using ProfOsmotr.DAL;
using System;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class PatientBase
    {
        public PatientBase(Patient patient)
        {
            if (patient is null) throw new ArgumentNullException(nameof(patient));

            FullName = $"{patient.LastName} {patient.FirstName} {patient.PatronymicName}".Trim();
        }

        public string FullName { get; set; }
    }
}