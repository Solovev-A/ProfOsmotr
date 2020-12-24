using ProfOsmotr.Web.Infrastructure;

namespace ProfOsmotr.Web.Models
{
    public class ClinicResource
    {
        [RelatedProperty(typeof(ClinicIdRelation))]
        public int Id { get; set; }

        public bool IsBlocked { get; set; }

        public ClinicDetailsResource ClinicDetails { get; set; }
    }
}