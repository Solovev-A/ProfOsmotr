using ProfOsmotr.DAL;
using System.Collections.Generic;

namespace ProfOsmotr.BL.Models
{
    public class ProfessionSearchResult
    {
        public IEnumerable<Profession> Items { get; set; }

        public IEnumerable<Profession> Suggestions { get; set; }
    }
}