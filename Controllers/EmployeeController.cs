using DemoProc.Models;
using DemoProc.Services;
using Microsoft.AspNetCore.Mvc;


namespace DemoProc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeService;
        //Get Employees
        public EmployeeController(IConfiguration configuration, IEmployeeService employeeService)
        {
            _configuration = configuration;
            _employeeService = employeeService;
        }

        public ActionResult Index()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            model.EmployeesList = _employeeService.GetEmployeeList().ToList();
            return View(model);

        }

        [HttpPost]
        public IActionResult AddUpdateEmployee(Employee employee)
        {
            EmployeeViewModel model = new EmployeeViewModel();
                   //   employee.Id = 1;
            string url = Request.Headers["Referer"].ToString();

            string result = string.Empty;
            if (employee.Id > 0)
            {
                result = _employeeService.UpdateEmployee(employee);
            }
            else
            {
                result = _employeeService.InsertEmployee(employee);
            }

            if (result == "added successfully")
            {
                TempData["SuccessMsg"] = "Employee added successfully";
                return Redirect(url);
            }

            else
            {
                TempData["ErrorMsg"] = result;
                return Redirect(url);
            }
        }
        [HttpPost]
        public IActionResult DeleteEmployee(int Id)
        {
            string url = Request.Headers["Referer"].ToString();
            string result = _employeeService.DeleteEmployee(Id);
            if (result == "deleted successfully")
            {
                // return new JsonResult("deleted successfully");
                return Json(new { message = "deleted successfully" });
                //TempData["SuccessMsg"] = "Employee deleted successfully";
                //return Redirect(url);
            }
            else
            {
                return Json(new { message = "Error Occured" });

            }
        }


    }
}

