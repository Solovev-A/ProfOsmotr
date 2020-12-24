using ProfOsmotr.Web.Infrastructure;

namespace ProfOsmotr.Web.Models
{
    public class RoleResource
    {
        public int Id { get; set; }

        [RelatedProperty(typeof(UserRoleNameRelation))]
        public string Name { get; set; }
    }
}