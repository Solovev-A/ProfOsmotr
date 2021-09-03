using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.BL
{
    public class PatchEmployerDepartmentQuery : PatchDtoBase
    {
        [StringLength(500)]
        public string Name { get; set; }
    }
}