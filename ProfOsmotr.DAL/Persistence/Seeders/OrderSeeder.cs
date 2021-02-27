using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    internal class OrderSeeder
    {
        private IProfUnitOfWork uow;

        internal OrderSeeder(IProfUnitOfWork uow)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Добавляет пункты приказа с обследованиями по приказу в хранилища
        /// </summary>
        /// <returns>Перечисление всех обследований</returns>
        internal async Task<IEnumerable<OrderExamination>> Seed()
        {
            await SeedAvailabilityGroups();
            await SeedTargetGroups();
            IEnumerable<OrderExamination> examinations = await SeedOrderAsync();
            return examinations;
        }

        private async Task SeedAvailabilityGroups()
        {
            var groups = Enum.GetValues(typeof(ServiceAvailabilityGroupId))
                .Cast<ServiceAvailabilityGroupId>()
                .Select(e => new ServiceAvailabilityGroup() { Id = e, Name = e.Description() });

            await uow.ServiceAvailabilityGroups.AddRangeAsync(groups);
        }

        private async Task SeedTargetGroups()
        {
            var groups = Enum.GetValues(typeof(TargetGroupId))
                .Cast<TargetGroupId>()
                .Select(e => new TargetGroup() { Id = e, Name = e.Description() });

            await uow.TargetGroups.AddRangeAsync(groups);
        }

        private async Task<IEnumerable<OrderExamination>> SeedOrderAsync()
        {
            var orderData = await GetJsonData<OrderData>(OrderDataConfiguration.OrderDataJsonPath);
            var examinationsData = orderData.Doctors.Values
                .Concat(orderData.Examinations.Values)
                .Concat(orderData.General.Doctors)
                .Concat(orderData.General.Examinations);

            var orderExaminations = GetOrderExaminations(examinationsData).ToList();
            var orderItems = GetOrderItems(orderData, orderExaminations).ToList();

            await uow.OrderItems.AddRangeAsync(orderItems);

            return orderExaminations;
        }

        private async Task<T> GetJsonData<T>(string path)
        {
            string json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<T>(json);
        }

        private IEnumerable<OrderExamination> GetOrderExaminations(IEnumerable<ExaminationData> examinationsData)
        {
            return examinationsData.Select(data => {
                var defaultIndex = new ExaminationResultIndex() { Title = "Результат", UnitOfMeasure = "" };

                var result = new OrderExamination()
                {
                    IsMandatory = data.IsMandatory,
                    Name = data.Name,
                    TargetGroupId = data.TargetGroupId,
                    DefaultServiceDetails = new ServiceDetails()
                    {
                        Code = data.ServiceCode,
                        FullName = data.ServiceName
                    }                    
                };

                if (data.Indexes != null && data.Indexes.Any())
                {
                    foreach(var indexData in data.Indexes)
                    {
                        var index = GetExaminationResultIndex(indexData);
                        result.ExaminationResultIndexes.Add(index);
                    }
                }
                else
                {
                    result.ExaminationResultIndexes.Add(defaultIndex);
                }

                return result;
            });
        }

        private ExaminationResultIndex GetExaminationResultIndex(ExaminationResultIndexData data)
        {
            return new ExaminationResultIndex()
            {
                Title = data.Title,
                UnitOfMeasure = data.UnitOfMeasure
            };
        }

        private IEnumerable<OrderItem> GetOrderItems(OrderData orderData, List<OrderExamination> orderExaminations)
        {
            return orderData.Factors.Select(factorData =>
            {
                var result = new OrderItem()
                {
                    Key = factorData.Key,
                    Name = factorData.Value.Name
                };
                var itemDoctorsData = factorData.Value.Doctors
                    .Select(docId => orderData.Doctors[docId.ToString()]);
                var itemExaminationsData = factorData.Value.Examinations
                    .Select(examId => orderData.Examinations[examId.ToString()]);

                IEnumerable<OrderExamination> itemExaminations = itemDoctorsData.Concat(itemExaminationsData)
                    .Select(examData => orderExaminations.Single(exam => exam.Name == examData.Name));

                foreach (var exam in itemExaminations)
                {
                    result.OrderExaminations.Add(exam);
                }
                return result;
            });
        }

        // для десериализации

        private class OrderData
        {
            public Dictionary<string, ExaminationData> Doctors { get; set; }

            public Dictionary<string, ExaminationData> Examinations { get; set; }

            public Dictionary<string, FactorData> Factors { get; set; }

            public GeneralData General { get; set; }
        }

        private class FactorData
        {
            public string Name { get; set; }

            public IEnumerable<int> Doctors { get; set; }

            public IEnumerable<int> Examinations { get; set; }
        }

        private class GeneralData
        {
            public IEnumerable<ExaminationData> Doctors { get; set; }

            public IEnumerable<ExaminationData> Examinations { get; set; }
        }

        private class ExaminationData
        {
            public string Name { get; set; }

            public TargetGroupId TargetGroupId { get; set; }

            public string ServiceName { get; set; }

            public string ServiceCode { get; set; }

            public bool IsMandatory { get; set; }

            public IEnumerable<ExaminationResultIndexData> Indexes { get; set; }
        }

        private class ExaminationResultIndexData
        {
            public string Title { get; set; }

            public string UnitOfMeasure { get; set; }
        }
    }
}