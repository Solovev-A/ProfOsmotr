namespace ProfOsmotr.Web.Models
{
    public class PreliminaryExaminationWorkPlaceResource
    {
        public PreliminaryExaminationEmployerResource Employer { get; set; }

        public ExaminationProfessionResource Profession { get; set; }
    }
}