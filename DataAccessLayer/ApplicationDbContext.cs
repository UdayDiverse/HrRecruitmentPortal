using DataAccessLayer.Domain.Masters.Department;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentEntity> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DepartmentEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}