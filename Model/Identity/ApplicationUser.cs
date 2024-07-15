using Microsoft.AspNetCore.Identity;

namespace Model.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FullName { get; set; }
}