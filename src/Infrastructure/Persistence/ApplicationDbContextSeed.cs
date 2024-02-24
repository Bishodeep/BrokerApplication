
using clean.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace clean.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<Identity.ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        var roles =new List<ApplicationRole> { new ApplicationRole() { Name = "Administrator" }, new ApplicationRole() { Name = "Broker" }, new ApplicationRole() { Name = "User" } };
        foreach (var role in roles)
        {
            if (roleManager.Roles.All(r => r.Name != role.Name))
            {
                await roleManager.CreateAsync(role);
            }
        }
        

        var administrator = new Identity.ApplicationUser { UserName = "superadmin@localhost", Email = "superadmin@localhost" };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Administrator1!");
            await userManager.AddToRolesAsync(administrator, new[] { roles.FirstOrDefault(x => x.Name == "Administrator").Name });
        }
    }
}
