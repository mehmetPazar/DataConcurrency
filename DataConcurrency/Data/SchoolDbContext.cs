using DataConcurrency.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataConcurrency.Data;

public class SchoolDbContext : DbContext
{
    public virtual DbSet<Student> Students { get; set; }
    
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().Property(a => a.Version).IsRowVersion();
    }
}