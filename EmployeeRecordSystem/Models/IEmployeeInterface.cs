using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRecordSystem.Models
{
    public interface IEmployeeInterface
    {
        void AddEmployee(Employee employee);

        IEnumerable<Employee> GetAllEmployees();

        void UpdateEmployee(Employee employee);

        Employee GetEmployeeDetails(int? id);

        void DeleteEmployeeDetails(int? id);

        string GetLogin(Login login);
    }
}
