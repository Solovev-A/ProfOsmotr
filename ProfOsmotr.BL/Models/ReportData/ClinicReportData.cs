using ProfOsmotr.DAL;
using System;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class ClinicReportData
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public ClinicReportData(Clinic clinic)
        {
            if (clinic is null) throw new ArgumentNullException(nameof(clinic));

            Address = clinic.ClinicDetails.Address;
            Name = clinic.ClinicDetails.FullName;
            Phone = clinic.ClinicDetails.Phone;
        }
    }
}