using ProfOsmotr.Web.Infrastructure;

namespace ProfOsmotr.Web.Models
{
    public class ClinicDetailsResource
    {
        public string FullName { get; set; }

        [RelatedProperty(typeof(ClinicShortNameRelation))]
        public string ShortName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}