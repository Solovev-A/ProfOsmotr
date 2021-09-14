namespace ProfOsmotr.BL
{
    public class PatchEmployerDataQuery : PatchDtoBase
    {
        public int EmployeesTotal { get; set; }

        public int EmployeesWomen { get; set; }

        public int EmployeesUnder18 { get; set; }

        public int EmployeesPersistentlyDisabled { get; set; }

        public int WorkingWithHarmfulFactorsTotal { get; set; }

        public int WorkingWithHarmfulFactorsWomen { get; set; }

        public int WorkingWithHarmfulFactorsUnder18 { get; set; }

        public int WorkingWithHarmfulFactorsPersistentlyDisabled { get; set; }

        public int WorkingWithJobTypesTotal { get; set; }

        public int WorkingWithJobTypesWomen { get; set; }

        public int WorkingWithJobTypesUnder18 { get; set; }

        public int WorkingWithJobTypesPersistentlyDisabled { get; set; }
    }
}