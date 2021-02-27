using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfOsmotr.BL
{
    public class ProfCalculator : IProfCalculator
    {
        private CalculateResultRequest request;

        public IEnumerable<CalculationResultItem> CalculateResult(CalculateResultRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (request.CalculationSources == null || !request.CalculationSources.Any())
                throw new InvalidOperationException("Расчет результата невозможен, в связи с отсутствием исходных данных для расчета");
            this.request = request;

            var amountByService = new Dictionary<Service, int>();
            foreach (var source in request.CalculationSources)
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
            var examinations = source.Profession.OrderItems
                    .SelectMany(item => item.OrderExaminations);

            if (request.MandatoryOrderExaminations != null)
            {
                examinations = examinations.Concat(request.MandatoryOrderExaminations);
            }

            return examinations
                    .Select(examination => examination.ActualClinicServices.Single().Service)
                    .Distinct();
        }
    }
}