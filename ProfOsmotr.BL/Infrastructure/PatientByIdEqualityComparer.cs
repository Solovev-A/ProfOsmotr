using ProfOsmotr.DAL;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ProfOsmotr.BL.Infrastructure
{
    internal class PatientByIdEqualityComparer : IEqualityComparer<Patient>
    {
        public bool Equals([AllowNull] Patient x, [AllowNull] Patient y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode([DisallowNull] Patient obj)
        {
            return obj.Id;
        }
    }
}