using ProfOsmotr.DAL;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ProfOsmotr.BL.Infrastructure
{
    internal class ProfessionByNameEqualityComparer : IEqualityComparer<Profession>
    {
        public bool Equals([AllowNull] Profession x, [AllowNull] Profession y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode([DisallowNull] Profession obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}