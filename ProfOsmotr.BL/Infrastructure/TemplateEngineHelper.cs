using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TemplateEngine.Docx;

namespace ProfOsmotr.BL.Infrastructure
{
    public class TemplateEngineHelper
    {
        protected readonly string reportsDirectoryPath;

        public TemplateEngineHelper(string reportsDirectoryPath)
        {
            this.reportsDirectoryPath = reportsDirectoryPath ?? throw new ArgumentNullException(nameof(reportsDirectoryPath));
        }

        public string CreateReport<TModel>(string templatePath, TModel data)
        {
            string reportPath = PrepareNewReport(templatePath, reportsDirectoryPath);
            Content content = ToContent(data);
            SaveReport(reportPath, content);

            return reportPath;
        }

        /// <summary>
        /// Подготавливает новый отчет
        /// </summary>
        /// <param name="templatePath">Путь к файлу шаблона отчета</param>
        /// <param name="reportsDirectoryPath">Путь к директории, в которую будет создан новый отчет</param>
        /// <returns>Путь к файлу нового отчета</returns>
        protected string PrepareNewReport(string templatePath, string reportsDirectoryPath)
        {
            string newFileName = Guid.NewGuid().ToString();
            string targetPath = reportsDirectoryPath + newFileName + ".docx";

            File.Delete(targetPath);
            File.Copy(templatePath, targetPath);

            return targetPath;
        }

        /// <summary>
        /// Заполняет отчет по пути <paramref name="reportPath"/> содержимым <paramref name="content"/> и сохраняет изменения
        /// </summary>
        /// <param name="reportPath">Путь к подготовленному файлу отчета</param>
        /// <param name="content">Значения текущего отчета</param>
        protected void SaveReport(string reportPath, Content content)
        {
            using (var outputDocument = new TemplateProcessor(reportPath)
                .SetRemoveContentControls(true))
            {
                outputDocument.FillContent(content);
                outputDocument.SaveChanges();
            }
        }

        protected Content ToContent<TModel>(TModel model)
        {
            ICollection<IContentItem> contentItems = new List<IContentItem>();
            string targetPropName = string.Empty;
            object value = null;
            ProcessObject(model, contentItems);
            return new Content(contentItems.ToArray());

            void ProcessObject<Tobj>(Tobj obj, ICollection<IContentItem> target)
            {
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    string initialPropName = targetPropName;

                    targetPropName += prop.Name;
                    value = prop.GetValue(obj, null);
                    Type propType = prop.PropertyType;

                    // если тип свойства - класс, обрабатываем и его свойства,
                    // сохраняя вложенность свойств в названии результирующего свойства
                    if (IsClassAndNotString(propType))
                    {
                        targetPropName += ".";
                        ProcessObject(value, target);
                    }
                    // если тип свойства - перечисление объектов,
                    // обрабатываем его как таблица, а содержимое - как строки таблицы
                    else if (propType.IsGenericType
                        && propType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                        && IsClassAndNotString(propType.GetGenericArguments()[0]))
                    {
                        TableContent table = new TableContent(targetPropName);
                        targetPropName += ".";

                        foreach (var item in (IEnumerable)value)
                        {
                            ICollection<IContentItem> row = new List<IContentItem>();
                            ProcessObject(item, row);
                            table.AddRow(row.ToArray());
                        }
                    }
                    // если свойство - строка, обрабатываем как обычное поле
                    else if (propType == typeof(string))
                    {
                        var valueString = value is null ? string.Empty : value.ToString();
                        target.Add(new FieldContent(targetPropName, valueString));
                    }
                    else
                    {
                        // прочие схемы шаблонов и объектов возможны,
                        // но не поддерживаются в этой реализации
                        throw new NotSupportedException();
                    }

                    targetPropName = initialPropName;
                }
            }
        }

        protected bool IsClassAndNotString(Type type)
        {
            return type.IsClass && type != typeof(string);
        }
    }
}