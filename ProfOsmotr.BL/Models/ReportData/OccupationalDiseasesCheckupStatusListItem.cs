using ProfOsmotr.DAL;
using System.Linq;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class OccupationalDiseasesCheckupStatusListItem : CheckupStatusDataBase
    {
        public string N { get; set; }

        public string Profession { get; set; }

        public string OrderItems { get; set; }

        public OccupationalDiseasesCheckupStatusListItem(CheckupStatus checkupStatus, string n) : base(checkupStatus)
        {
            N = n + ".";
            Profession = checkupStatus.Profession.Name;
            OrderItems = checkupStatus.Profession.OrderItems.Any()
                ? string.Join("; ", checkupStatus.Profession.OrderItems.Select(i => i.Key))
                : "отсутствуют";
        }

        protected OccupationalDiseasesCheckupStatusListItem()
        {
        }

        public static OccupationalDiseasesCheckupStatusListItem Empty()
        {
            string emptyValue = "-";
            return new OccupationalDiseasesCheckupStatusListItem()
            {
                DateOfBirth = emptyValue,
                EmployerDepartment = emptyValue,
                FullName = emptyValue,
                Gender = emptyValue,
                N = emptyValue,
                OrderItems = emptyValue,
                Profession = emptyValue
            };
        }
    }
}