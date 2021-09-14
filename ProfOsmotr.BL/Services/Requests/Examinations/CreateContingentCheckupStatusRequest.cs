namespace ProfOsmotr.BL
{
    public class CreateContingentCheckupStatusRequest
    {
        public int PatientId { get; set; }

        public int? EmployerDepartmentId { get; set; }

        public int? ProfessionId { get; set; }

        public int ClinicId { get; set; }

        public int ExaminationId { get; set; }

        public int CreatorId { get; set; }
    }
}