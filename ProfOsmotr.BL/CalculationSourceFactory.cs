using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class CalculationSourceFactory : ICalculationSourceFactory
    {
        private readonly IProfessionService professionService;

        public CalculationSourceFactory(IProfessionService professionService)
        {
            this.professionService = professionService ?? throw new ArgumentNullException(nameof(professionService));
        }

        public async Task<CalculationSourcesResponse> CreateCalculationSources(CreateCalculationRequest request)
        {
            if (request == null || !request.CreateProfessionRequests.Any())
            {
                return new CalculationSourcesResponse("Невозможно создать исходные данные расчета без сведений о профессиях");
            }

            List<CalculationSource> sources = new List<CalculationSource>();

            foreach (var profRequest in request.CreateProfessionRequests)
            {
                if (!CheckProfessionRequestNumbers(profRequest))
                {
                    return new CalculationSourcesResponse($"Профессия '{profRequest.Name}' содержит некорректную численность персонала");
                }

                var professionResponse = await professionService.CreateProfessionForCalculation(profRequest, request.ClinicId);
                if (!professionResponse.Succeed)
                {
                    return new CalculationSourcesResponse($"Профессия '{profRequest.Name}' содержит некорректные данные");
                }
                sources.Add(GetCalculationSource(profRequest, professionResponse));
            }
            return new CalculationSourcesResponse(sources);
        }

        private bool CheckProfessionRequestNumbers(CreateProfessionRequest request)
        {
            return request.NumberOfPersons > 0 &&
                request.NumberOfPersons >= request.NumberOfPersonsOver40 &&
                request.NumberOfPersons >= request.NumberOfWomen &&
                request.NumberOfWomen >= request.NumberOfWomenOver40 &&
                request.NumberOfPersonsOver40 >= request.NumberOfWomenOver40 &&
                request.NumberOfWomenOver40 >= 0;
        }

        private CalculationSource GetCalculationSource(CreateProfessionRequest request, ProfessionResponse response)
        {
            return new CalculationSource()
            {
                NumberOfPersons = request.NumberOfPersons,
                NumberOfPersonsOver40 = request.NumberOfPersonsOver40,
                NumberOfWomen = request.NumberOfWomen,
                NumberOfWomenOver40 = request.NumberOfWomenOver40,
                Profession = response.Result
            };
        }
    }
}