using DemoProc.Models;

namespace DemoProc.Services
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployeeList();
        public string InsertEmployee(Employee employee);
        public string UpdateEmployee(Employee employee);
        public string DeleteEmployee(int Id);
     
    }
}
 