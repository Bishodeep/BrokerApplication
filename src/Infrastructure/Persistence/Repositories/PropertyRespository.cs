using clean.Application.Contracts.Persistance;
using clean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace clean.Infrastructure.Persistence.Repositories
{
    public class PropertyRespository : Repository<Property>, IPropertyRepository
    {
        public PropertyRespository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
        public async Task<IEnumerable<Property>> GetAllPropertyAsync()
        {
            return await GetAllAsync()
               .ToListAsync();
        }
    }
}
