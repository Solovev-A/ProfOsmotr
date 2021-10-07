namespace ProfOsmotr.BL.Models.ReportData
{
    public class CheckupStatusDataListItem : CheckupStatusData
    {
        /// <summary>
        /// Номер по порядку
        /// </summary>
        public string N { get; set; }

        public CheckupStatusDataListItem(DAL.CheckupStatus checkupStatus, string n) : base(checkupStatus)
        {
            N = n + ".";
        }

        protected CheckupStatusDataListItem()
        {
        }

        public static CheckupStatusDataListItem Empty()
        {
            string emptyValue = "-";
            return new CheckupStatusDataListItem()
            {
                CheckupResult = emptyValue,
                DateOfBirth = emptyValue,
                EmployerDepartment = emptyValue,
                FullName = emptyValue,
                Gender = emptyValue,
                MedicalReport = emptyValue,
                N = emptyValue
            };
        }
    }
}