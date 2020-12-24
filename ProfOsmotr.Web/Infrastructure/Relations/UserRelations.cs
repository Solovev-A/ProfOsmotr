using ProfOsmotr.DAL;

namespace ProfOsmotr.Web.Infrastructure
{
    public class UserIdRelation : PropertyRelation<User, int>
    {
        public UserIdRelation()
            : base(x => x.Id)
        {
        }
    }

    public class UserNameRelation : PropertyRelation<User, string>
    {
        public UserNameRelation()
            : base(x => x.UserProfile.Name)
        {
        }
    }

    public class UserRoleNameRelation : PropertyRelation<User, string>
    {
        public UserRoleNameRelation()
            : base(x => x.Role.Name)
        {
        }
    }

    public class UserClinicShortNameRelation : PropertyRelation<User, string>
    {
        public UserClinicShortNameRelation()
            : base(x => x.Clinic.ClinicDetails.ShortName)
        {
        }
    }
}