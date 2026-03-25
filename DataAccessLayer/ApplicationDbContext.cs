using DataAccess.Domain.Masters.LookUpMst;
using DataAccess.Domain.Masters.LookUpType;
using DataAccessLayer.Domain.Masters.Department;
using DataAccessLayer.Domain.Masters.User;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentEntity> DepartmentEntity { get; set; }
    public virtual DbSet<DepartmentMembersEntity> DepartmentMembersEntity { get; set; }
    public virtual DbSet<UserEntity> UserEntity { get; set; }
    public virtual DbSet<LookupTypeMstEntity> LookupTypeMstEntities { get; set; }
    public virtual DbSet<LookupMstEntity> LookupMstEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DepartmentEntity>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasMany(d => d.DepartmentMembers)
                  .WithOne(d => d.Department)
                  .HasForeignKey(m => m.DeptId);
        });

        modelBuilder.Entity<LookupMstEntity>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(b => b.LookupTypeDetails)
             .WithMany(a => a.Lookups)
             .HasForeignKey(b => b.TypeId);
        });
    }
}