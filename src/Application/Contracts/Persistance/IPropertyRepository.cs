using clean.Application.Common.Models;
using clean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clean.Application.Contracts.Persistance
{
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<IEnumerable<Property>> GetAllPropertyAsync();
    }
}
