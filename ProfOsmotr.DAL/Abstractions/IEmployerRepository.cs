﻿using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    public interface IEmployerRepository : IQueryAwareRepository<Employer>
    {
        Task<Employer> FindEmployerAsync(int id);
    }
}