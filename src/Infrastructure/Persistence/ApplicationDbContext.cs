using clean.Domain.Entities;
using clean.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace clean.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
            ApplicationRole,
            string,
            IdentityUserClaim<string>,
            IdentityUserRole<string>,
            IdentityUserLogin<string>,
            IdentityRoleClaim<string>,
            IdentityUserToken<string>>
{
    

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet <Property> Properties { get; set; }



    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        

        var result = await base.SaveChangesAsync(cancellationToken);


        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
