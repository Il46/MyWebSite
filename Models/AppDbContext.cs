using Microsoft.EntityFrameworkCore;
using MyWebSite.Models;

namespace MyWebSite;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<StudentRecord> Students => Set<StudentRecord>();
    public DbSet<TeacherRecord> Teachers => Set<TeacherRecord>();
    public DbSet<GroupRecord> Groups => Set<GroupRecord>();
    public DbSet<GradeRecord> Grades => Set<GradeRecord>();
    public DbSet<AttendanceRecord> Attendance => Set<AttendanceRecord>();
    public DbSet<CollegeProfileRecord> CollegeProfiles => Set<CollegeProfileRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentRecord>(entity =>
        {
            entity.HasKey(student => student.StudentId);
            entity.Property(student => student.StudentId).HasMaxLength(32);
            entity.Property(student => student.Name).HasMaxLength(80).IsRequired();
            entity.Property(student => student.Surname).HasMaxLength(80).IsRequired();
            entity.Property(student => student.Group).HasMaxLength(30).IsRequired();
        });

        modelBuilder.Entity<TeacherRecord>(entity =>
        {
            entity.HasKey(teacher => teacher.Id);
            entity.Property(teacher => teacher.Id).HasMaxLength(32);
            entity.Property(teacher => teacher.Name).HasMaxLength(80).IsRequired();
            entity.Property(teacher => teacher.Surname).HasMaxLength(80).IsRequired();
            entity.Property(teacher => teacher.Subject).HasMaxLength(80).IsRequired();
        });

        modelBuilder.Entity<GroupRecord>(entity =>
        {
            entity.HasKey(group => group.Title);
            entity.Property(group => group.Title).HasMaxLength(30);
        });

        modelBuilder.Entity<GradeRecord>(entity =>
        {
            entity.HasKey(grade => grade.Id);
            entity.Property(grade => grade.Id).HasMaxLength(32);
            entity.Property(grade => grade.Subject).HasMaxLength(80).IsRequired();
            entity.HasOne<StudentRecord>()
                .WithMany()
                .HasForeignKey(grade => grade.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AttendanceRecord>(entity =>
        {
            entity.HasKey(record => new { record.StudentId, record.Date });
            entity.HasOne<StudentRecord>()
                .WithMany()
                .HasForeignKey(record => record.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CollegeProfileRecord>().HasKey(profile => profile.Id);
    }
}
