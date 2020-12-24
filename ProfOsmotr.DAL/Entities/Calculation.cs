using System;
using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет расчет медицинского осмотра
    /// </summary>
    public class Calculation
    {
        /// <summary>
        /// Идентификатор расчета
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Идентификатор медицинской организации, производившей расчет
        /// </summary>
        public int ClinicId { get; set; }

        /// <summary>
        /// Медицинская организация, производившая расчет
        /// </summary>
        public virtual Clinic Clinic { get; set; }

        /// <summary>
        /// Идентификатор пользователя, производившего расчет
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// Пользователь, производивший расчет
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// Название расчета
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата и время создание расчета
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Сигнализирует о том, что расчет подвергался ручным изменениям
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// Первичные исходные данные расчета
        /// </summary>
        public virtual ICollection<CalculationSource> CalculationSources { get; set; } = new List<CalculationSource>();

        /// <summary>
        /// Результаты расчета
        /// </summary>
        public virtual ICollection<CalculationResultItem> CalculationResultItems { get; set; } = new List<CalculationResultItem>();

        /// <summary>
        /// Используется для отметки расчета как удаленного без физического удаления
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}