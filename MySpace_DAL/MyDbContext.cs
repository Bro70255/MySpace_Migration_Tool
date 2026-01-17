using Microsoft.EntityFrameworkCore;
using MySpace_Common;
using MySpace_Common.EntityModels;

namespace MySpace_DAL
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }
        public DbSet<ExtractedFileDetails> ExtractedFileDetails { get; set; }
        public DbSet<FileChildDetail> FileChildDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectMaster> ProjectMaster { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();
        }

    }
}
