using DataAccessLayer.Domain.Masters.Department;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<DepartmentEntity> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DepartmentEntity>(entity =>
        {
            entity.ToTable("Departments");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.DepartmentName)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.DepartmentCode)
                  .HasMaxLength(20);

            entity.Property(e => e.IsActive)
                  .HasDefaultValue(true);
        });
    }
}