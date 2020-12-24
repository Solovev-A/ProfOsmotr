using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfOsmotr.BL
{
    public class Calculator302n : IProfCalculator
    {
        public IEnumerable<CalculationResultItem> CalculateResult(IEnumerable<CalculationSource> sources)
        {
            if (sources == null)
                throw new ArgumentNullException(nameof(sources));
            if (!sources.Any())
                throw new InvalidOperationException("Расчет результата невозможен, в связи с отсутствием исходных данных для расчета");
            var amountByService = new Dictionary<Service, int>();
            foreach (var source in sources)
            {
                var servicesForCurrentSourceProfession = GetNecessaryServices(source);
                foreach (var service in servicesForCurrentSourceProfession)
                {
                    if (service.OrderExamination.TargetGroupId == TargetGroupId.ForPersonsOver40Only)
                    {
                        AddAmount(service, source.NumberOfPersonsOver40);
                    }
                    else if (service.OrderExamination.TargetGroupId == TargetGroupId.ForWomenOnly)
                    {
                        AddAmount(service, source.NumberOfWomen);
                    }
                    else if (service.OrderExamination.TargetGroupId == TargetGroupId.ForWomenOver40Only)
                    {
                        AddAmount(service, source.NumberOfWomenOver40);
                    }
                    else
                    {
                        AddAmount(service, source.NumberOfPersons);
                    }
                }
            }
            return amountByService
                .Where(pair => pair.Value != 0)
                .Select(pair => new CalculationResultItem() { Service = pair.Key, Amount = pair.Value });

            void AddAmount(Service service, int amount)
            {
                if (amount < 0)
                    throw new InvalidOperationException("Численность работников не может быть отрицательной");

                if (!amountByService.ContainsKey(service))
                    amountByService[service] = amount;
                else
                    amountByService[service] += amount;
            }
        }

        private IEnumerable<Service> GetNecessaryServices(CalculationSource source)
        {
            var sourceExaminations = source.Profession.ProfessionOrderItems
                    .SelectMany(item => item.OrderItem.OrderItemOrderExaminations.Select(i => i.OrderExamination));

            return sourceExaminations
                    .Select(examination => examination.ActualClinicServices.Single().Service)
                    .Distinct();
        }
    }
}