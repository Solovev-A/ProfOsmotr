namespace ProfOsmotr.Web.Models
{
    public class CreateCalculationSourceQuery
    {
        public CreateProfessonQuery Profession { get; set; }

        public int NumberOfPersons { get; set; }

        public int NumberOfPersonsOver40 { get; set; }

        public int NumberOfWomen { get; set; }

        public int NumberOfWomenOver40 { get; set; }
    }
}