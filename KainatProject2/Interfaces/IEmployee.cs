using KainatProject2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KainatProject2.Interfaces
{
    public interface IEmployee
    {
        IEnumerable<Employee> GetAllEmployees();
        int AddEmployee(Employee employee);
        int UpdateEmployee(Employee employee);
        Employee GetEmployeeData(int id);
        int DeleteEmployee(int id);

        Admin Logincheck(string username, string password);
    }
}
