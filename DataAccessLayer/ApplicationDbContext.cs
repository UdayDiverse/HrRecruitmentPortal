using DataAccess.Domain.Masters.LookUpMst;
using DataAccess.Domain.Masters.LookUpType;
using DataAccessLayer.Domain.Common.Notes;
using DataAccessLayer.Domain.Masters.Department;
using DataAccessLayer.Domain.Masters.Job;
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
    public virtual DbSet<JobEntity> JobEntity { get; set; }
    public virtual DbSet<JobMembersEntity> JobMembersEntity { get; set; }
    public virtual DbSet<UserEntity> UserEntity { get; set; }
    public virtual DbSet<LookupTypeMstEntity> LookupTypeMstEntities { get; set; }
    public virtual DbSet<LookupMstEntity> LookupMstEntities { get; set; }
    public virtual DbSet<NoteEntity> NoteEntity { get; set; }

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

        modelBuilder.Entity<JobEntity>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasMany(d => d.JobMembers)
                .WithOne(d => d.Job)
                .HasForeignKey(m => m.JobId);
        });

        modelBuilder.Entity<NoteEntity>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Header).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Status).HasMaxLength(50).IsRequired();
            entity.Property(e => e.ReferenceType).IsRequired();
            entity.Property(e => e.ReferenceId).IsRequired();
        });
    }
}
