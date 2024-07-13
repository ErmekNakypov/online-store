using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace DAL.EntityFramework;

public class ApplicationDbContext :  DbContext
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