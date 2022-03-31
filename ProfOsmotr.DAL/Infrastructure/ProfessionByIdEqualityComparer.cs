using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ProfOsmotr.DAL.Infrastructure
{
    public class ProfessionByIdEqualityComparer : IEqualityComparer<Profession>
    {
        public bool Equals([AllowNull] Profession x, [AllowNull] Profession y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode([DisallowNull] Profession obj)
        {
            return obj.Id;
        }
    }
}