using GlidewellDentalService.Model;
using System.Collections.Generic;

namespace GlidewellDentalService.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployee();
    }
}