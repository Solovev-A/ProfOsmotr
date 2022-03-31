using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class ErrorResource
    {
        public bool Success => false;

        public List<string> Errors { get; private set; }

        public ErrorResource(List<string> messages)
        {
            Errors = messages ?? new List<string>();
        }

        public ErrorResource(string message)
        {
            Errors = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                Errors.Add(message);
            }
        }
    }
}