﻿using ProfOsmotr.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    public interface IPeriodicMedicalExaminationRepository : IQueryAwareRepository<PeriodicMedicalExamination>
    {
        Task<PeriodicMedicalExamination> FindExaminationAsync(int id, bool noTracking = false);

        Task<ContingentCheckupStatus> FindCheckupStatus(int id, bool noTracking = false);

        Task<int> GetCheckupStatusClinicIdAsync(int checkupStatusId);

        void DeleteCheckupStatus(ContingentCheckupStatus checkupStatus);
        Task<IEnumerable<CountResult>> CountCheckupsByMonth(int clinicId);
    }
}