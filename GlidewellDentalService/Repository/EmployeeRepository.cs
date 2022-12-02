using GlidewellDentalService.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GlidewellDentalService.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public List<Employee> GetAllEmployee()
        {
            var rng = new Random();
            var employesData = Enumerable.Range(1, 5).Select(index => new Employee
            {
                //EmployeeId = rng.Next(-20, 55),
                //Email = "Test" + rng.Next(-20, 55) + "@test.com",
                //EmployeeName = "Test Name" + rng.Next(-20, 55),
                //Skill = "Test Skill" + rng.Next(-20, 55)
            })
            .ToList();

            return employesData;
        }
    }
}
