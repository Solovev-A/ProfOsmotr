using ProfOsmotr.Web.Infrastructure;

namespace ProfOsmotr.Web.Models
{
    public class UsersListItem
    {
        [RelatedProperty(typeof(UserIdRelation))]
        public int Id { get; set; }

        [RelatedProperty(typeof(UserNameRelation))]
        public string Name { get; set; }

        public string Position { get; set; }

        public string Username { get; set; }

        public RoleResource Role { get; set; }
    }

    public class UsersListGlobalItem : UsersListItem
    {
        [RelatedProperty(typeof(UserClinicShortNameRelation))]
        public string ClinicShortName { get; set; }
    }
}