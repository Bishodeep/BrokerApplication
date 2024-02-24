using Microsoft.AspNetCore.Identity;

namespace clean.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
}
