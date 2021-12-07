using ProfOsmotr.Web.Infrastructure;
using System;

namespace ProfOsmotr.Web.Models
{
    public class RegisterRequestResource
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        [RelatedProperty(typeof(RegisterRequestShortNameRelation))]
        public string ShortName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public RequestSenderResource Sender { get; set; }

        [RelatedProperty(typeof(RegisterRequestCreationTimeRelation))]
        public DateTime CreationTime { get; set; }

        public bool Processed { get; set; }

        [RelatedProperty(typeof(RegisterRequestApprovedRelation))]
        public bool Approved { get; set; }
    }
}