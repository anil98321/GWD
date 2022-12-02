using GlidewellDentalService.Model;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GlidewellDentalService.Contexts
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasData(
                new Employee()
                {
                    Id = "1",
                    FirstName = "Max",
                    LastName = "Jones",
                    Email = "mj@test.com",
                    MonthlySalary = 2000
                },
                 new Employee()
                 {
                     Id = "2",
                     FirstName = "Julie",
                     LastName = "Jones",
                     Email = "jj@test.com",
                     MonthlySalary = 2500
                 });


            base.OnModelCreating(modelBuilder);
        }
    }
}
