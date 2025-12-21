using Microsoft.EntityFrameworkCore;
using MySpace_Common;


namespace MySpace_DAL
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Registration> Registrations { get; set; }
    }
}
