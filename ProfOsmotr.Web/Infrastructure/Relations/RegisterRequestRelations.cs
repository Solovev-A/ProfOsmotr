using ProfOsmotr.DAL;
using System;

namespace ProfOsmotr.Web.Infrastructure
{
    public class RegisterRequestShortNameRelation : PropertyRelation<ClinicRegisterRequest, string>
    {
        public RegisterRequestShortNameRelation()
            : base(x => x.ShortName)
        {
        }
    }

    public class RegisterRequestCreationTimeRelation : PropertyRelation<ClinicRegisterRequest, DateTime>
    {
        public RegisterRequestCreationTimeRelation()
            : base(x => x.CreationTime)
        {
        }
    }

    public class RegisterRequestSenderNameRelation : PropertyRelation<ClinicRegisterRequest, string>
    {
        public RegisterRequestSenderNameRelation()
            : base(x => x.Sender.UserProfile.Name)
        {
        }
    }

    public class RegisterRequestApprovedRelation : PropertyRelation<ClinicRegisterRequest, bool>
    {
        public RegisterRequestApprovedRelation()
            : base(x => x.Approved)
        {
        }
    }
}