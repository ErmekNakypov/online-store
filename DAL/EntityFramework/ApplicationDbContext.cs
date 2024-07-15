using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Identity;

namespace DAL.EntityFramework;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    #region MyRegion
    
    public DbSet<User?> Users { get; set; }
    
    #endregion
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var applicationContextAssembly = typeof(ApplicationDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(applicationContextAssembly);
    }
   
}