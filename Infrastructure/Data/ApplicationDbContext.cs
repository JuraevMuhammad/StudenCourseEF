using System.Collections.Immutable;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentGroups> StudentGroups { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasOne(x => x.Teacher).WithMany(x => x.Courses).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Course>().HasMany(x => x.Groups).WithOne(x => x.Course).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Group>().HasMany(x => x.StudentGroups).WithOne(x => x.Groups).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Student>().HasMany(x => x.StudentGroups).WithOne(x => x.Students).OnDelete(DeleteBehavior.Cascade);
    }
}