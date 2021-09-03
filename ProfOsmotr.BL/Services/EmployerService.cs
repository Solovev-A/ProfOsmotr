using AutoMapper;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class EmployerService : IEmployerService
    {
        private readonly IProfUnitOfWork uow;
        private readonly IClinicService clinicService;
        private readonly IMapper mapper;

        public EmployerService(IProfUnitOfWork uow, IClinicService clinicService, IMapper mapper)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.clinicService = clinicService ?? throw new ArgumentNullException(nameof(clinicService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<EmployerResponse> GetEmployerAsync(int id)
        {
            var employer = await uow.Employers.FindEmployerAsync(id);

            if (employer is null)
            {
                return new EmployerResponse("Организация не найдена");
            }

            return new EmployerResponse(employer);
        }

        public async Task<QueryResponse<Employer>> ListEmployersAsync(ExecuteQueryBaseRequest request)
        {
            try
            {
                var employers = await uow.Employers.ExecuteQuery(
                    employer => employer.Name,
                    false,
                    request.Start,
                    request.ItemsPerPage,
                    request.Search,
                    employer => employer.ClinicId == request.ClinicId);

                return new QueryResponse<Employer>(employers);
            }
            catch (Exception ex)
            {
                return new QueryResponse<Employer>(ex.Message);
            }
        }

        public async Task<EmployerResponse> CreateEmployerAsync(CreateEmployerRequest request)
        {
            var clinicResponse = await clinicService.GetClinic(request.ClinicId);
            if (!clinicResponse.Succeed)
            {
                return new EmployerResponse(clinicResponse.Message);
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return new EmployerResponse("Название организации не может быть пустым");
            }

            Employer employer = mapper.Map<Employer>(request);

            try
            {
                await uow.Employers.AddAsync(employer);
                await uow.SaveAsync();
                return new EmployerResponse(employer);
            }
            catch (Exception ex)
            {
                return new EmployerResponse(ex.Message);
            }
        }

        public async Task<EmployerResponse> UpdateEmployerAsync(UpdateEmployerRequest request)
        {
            var employer = await uow.Employers.FindAsync(request.EmployerId);

            if (employer is null)
            {
                return new EmployerResponse("Организация не найдена");
            }

            mapper.Map(request.Query, employer);

            try
            {
                await uow.SaveAsync();
                return new EmployerResponse(employer);
            }
            catch (Exception ex)
            {
                return new EmployerResponse(ex.Message);
            }
        }

        public async Task<QueryResponse<Employer>> ListActualEmployersAsync(int clinicId)
        {
            try
            {
                var result = await uow.Employers.ExecuteQuery(
                    orderingSelector: employer => employer.Id,
                    descending: true,
                    length: 20,
                    customFilter: employer => employer.ClinicId == clinicId);
                return new QueryResponse<Employer>(result);
            }
            catch (Exception ex)
            {
                return new QueryResponse<Employer>(ex.Message);
            }
        }

        public async Task<EmployerDepartmentResponse> FindEmployerDepartmentAsync(int id, bool noTracking = true)
        {
            var employerDepartment = await uow.Employers.FindEmployerDepartmentAsync(id, noTracking);

            if (employerDepartment is null)
            {
                return new EmployerDepartmentResponse("Структурное подразделение не найдено");
            }

            return new EmployerDepartmentResponse(employerDepartment);
        }

        public async Task<EmployerDepartmentResponse> CreateEmployerDepartmentAsync(CreateEmployerDepartmentRequest request)
        {
            var employerResponse = await GetEmployerAsync(request.ParentId);

            if (!employerResponse.Succeed)
            {
                return new EmployerDepartmentResponse(employerResponse.Message);
            }

            var department = mapper.Map<EmployerDepartment>(request);

            try
            {
                await uow.EmployerDepartments.AddAsync(department);
                await uow.SaveAsync();
                return new EmployerDepartmentResponse(department);
            }
            catch (Exception ex)
            {
                return new EmployerDepartmentResponse(ex.Message);
            }
        }

        public async Task<EmployerDepartmentResponse> UpdateEmployerDepartmentAsync(UpdateEmployerDepartmentRequest request)
        {
            var departmentResponse = await FindEmployerDepartmentAsync(request.EmployerDepartmentId, false);

            if (!departmentResponse.Succeed)
            {
                return departmentResponse;
            }

            mapper.Map(request.Query, departmentResponse.Result);

            try
            {
                await uow.SaveAsync();
                return new EmployerDepartmentResponse(departmentResponse.Result);
            }
            catch (Exception ex)
            {
                return new EmployerDepartmentResponse(ex.Message);
            }
        }
    }
}