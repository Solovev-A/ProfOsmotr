using ProfOsmotr.Web.Infrastructure;

namespace ProfOsmotr.Web.Models
{
    public class RequestSenderResource
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        [RelatedProperty(typeof(RegisterRequestSenderNameRelation))]
        public string Name { get; set; }

        public string Position { get; set; }
    }
}