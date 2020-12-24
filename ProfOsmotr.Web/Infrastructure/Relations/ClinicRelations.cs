using ProfOsmotr.DAL;

namespace ProfOsmotr.Web.Infrastructure
{
    public class ClinicShortNameRelation : PropertyRelation<Clinic, string>
    {
        public ClinicShortNameRelation()
            : base(x => x.ClinicDetails.ShortName)
        {
        }
    }

    public class ClinicIdRelation : PropertyRelation<Clinic, int>
    {
        public ClinicIdRelation()
            : base(x => x.Id)
        {
        }
    }
}