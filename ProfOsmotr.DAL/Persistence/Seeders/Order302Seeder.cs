﻿using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    internal class Order302Seeder
    {
        #region Fields

        private IProfUnitOfWork uow;

        #endregion Fields

        #region Constructors

        internal Order302Seeder(IProfUnitOfWork uow)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Добавляет пункты приказа с обследованиями по приказу в хранилища
        /// </summary>
        /// <returns>Перечисление всех обследований</returns>
        internal async Task<IEnumerable<OrderExamination>> Seed()
        {
            await SeedAnnexes();
            await SeedAvailabilityGroups();
            await SeedTargetGroups();
            IEnumerable<OrderExamination> examinations = await SeedOrderAsync();
            return examinations;
        }

        private async Task AddExaminations(IList<OrderExamination> examinations)
        {
            foreach (var item in examinations)
            {
                await uow.OrderExaminations.AddAsync(item);
            }
        }

        private async Task AddOrderItems(IList<OrderItem> orderItems)
        {
            foreach (var item in orderItems)
            {
                await uow.OrderItems.AddAsync(item);
            }
        }

        private OrderAnnexId GetAnnexId(string line)
        {
            if (line.StartsWith("#"))
                return OrderAnnexId.General;
            if (line.StartsWith("2-"))
                return OrderAnnexId.JobTypes;
            return OrderAnnexId.HarmfulFactors;
        }

        private IList<ServiceDetails> GetDefaultDetails(string[] examinationsData)
        {
            List<ServiceDetails> result = new List<ServiceDetails>();
            foreach (var line in examinationsData)
            {
                var data = SplitExaminationLine(line);
                if (result.Any(item => item.FullName == data[2]))
                    continue;
                var detail = new ServiceDetails()
                {
                    FullName = data[2],
                    Code = data[1]
                };
                result.Add(detail);
            }
            return result;
        }

        private IList<OrderExamination> GetDefaultExaminations(string[] examinationsData,
                                                               IEnumerable<ServiceDetails> serviceDetails)
        {
            List<OrderExamination> result = new List<OrderExamination>();
            foreach (var line in examinationsData)
            {
                var data = SplitExaminationLine(line);
                var targetGroup = GetTargetGroup(data);
                var examination = new OrderExamination()
                {
                    Name = data[0],
                    TargetGroupId = targetGroup,
                    DefaultServiceDetails = serviceDetails.Single(det => det.FullName == data[2])
                };
                result.Add(examination);
            }
            return result;
        }

        private string GetItemKey(string line)
        {
            if (line.StartsWith("#"))
            {
                return line.Substring(1);
            }

            if (line.StartsWith("2-"))
            {
                return line.Substring(2);
            }

            return line;
        }

        private IList<OrderItem> GetOrderItems(string[] orderData, IList<OrderExamination> examinations)
        {
            var result = new List<OrderItem>();
            OrderItem currentItem = null;
            foreach (var line in orderData)
            {
                if (char.IsDigit(line[0]) || line[0] == '#')
                {
                    if (currentItem != null)
                    {
                        result.Add(currentItem);
                    }
                    OrderAnnexId annexId = GetAnnexId(line);
                    string key = GetItemKey(line);
                    currentItem = new OrderItem() { Key = key, OrderAnnexId = annexId };
                    continue;
                }
                var examination = examinations.Single(ex => ex.Name == line);
                var orderItemOrderExamination = new OrderItemOrderExamination()
                {
                    OrderExamination = examination,
                };
                currentItem.OrderItemOrderExaminations.Add(orderItemOrderExamination);
            }
            result.Add(currentItem);
            return result;
        }

        private TargetGroupId GetTargetGroup(string[] data)
        {
            return data[3] switch
            {
                "1" => TargetGroupId.ForAll,
                "2" => TargetGroupId.ForWomenOnly,
                "3" => TargetGroupId.ForWomenOver40Only,
                "4" => TargetGroupId.ForPersonsOver40Only,

                _ => throw new ArgumentException("Недопустимая целевая группа для обследования")
            };
        }

        private async Task SeedAnnexes()
        {
            var data = Enum.GetValues(typeof(OrderAnnexId))
                .Cast<OrderAnnexId>()
                .Select(e => new OrderAnnex() { Id = e, Name = e.Description() });

            await uow.OrderAnnexes.AddRangeAsync(data);
        }

        private async Task SeedAvailabilityGroups()
        {
            var groups = Enum.GetValues(typeof(ServiceAvailabilityGroupId))
                .Cast<ServiceAvailabilityGroupId>()
                .Select(e => new ServiceAvailabilityGroup() { Id = e, Name = e.Description() });

            await uow.ServiceAvailabilityGroups.AddRangeAsync(groups);
        }

        private async Task<IEnumerable<OrderExamination>> SeedOrderAsync()
        {
            string[] orderData = File.ReadAllLines(OrderDataConfiguration.OrderDataPath);
            string[] examinationsData = File.ReadAllLines(OrderDataConfiguration.ExaminationsDataPath);
            IList<ServiceDetails> details = GetDefaultDetails(examinationsData);
            IList<OrderExamination> examinations = GetDefaultExaminations(examinationsData, details);
            await AddExaminations(examinations);
            IList<OrderItem> orderItems = GetOrderItems(orderData, examinations);
            await AddOrderItems(orderItems);

            return examinations;
        }

        private async Task SeedTargetGroups()
        {
            var groups = Enum.GetValues(typeof(TargetGroupId))
                .Cast<TargetGroupId>()
                .Select(e => new TargetGroup() { Id = e, Name = e.Description() });

            await uow.TargetGroups.AddRangeAsync(groups);
        }

        private string[] SplitExaminationLine(string line)
        {
            return line.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion Methods
    }
}