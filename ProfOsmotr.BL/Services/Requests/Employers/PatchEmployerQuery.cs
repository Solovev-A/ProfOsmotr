using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.BL
{
    public class PatchEmployerQuery : PatchDtoBase
    {
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(70)]
        public string HeadLastName { get; set; }

        [StringLength(70)]
        public string HeadFirstName { get; set; }

        [StringLength(70)]
        public string HeadPatronymicName { get; set; }

        [StringLength(70)]
        public string HeadPosition { get; set; }
    }
}