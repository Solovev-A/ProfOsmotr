using ProfOsmotr.DAL;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class PatientBaseListItem : PatientBase
    {
        public string N { get; set; }

        public PatientBaseListItem(Patient patient, string n) : base(patient)
        {
            N = n + ".";
        }

        protected PatientBaseListItem()
        {
        }

        public static PatientBaseListItem Empty()
        {
            string emptyValue = "-";
            return new PatientBaseListItem()
            {
                FullName = emptyValue,
                N = emptyValue
            };
        }
    }
}