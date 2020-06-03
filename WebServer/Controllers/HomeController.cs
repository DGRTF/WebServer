using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebServer.Models;

namespace EmptyApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(EmployeesContext context)
        {
            employeesContext = context;
        }

        EmployeesContext employeesContext { get; set; }

        public JsonResult Index(int quantity = 10)
        {
            var employees = employeesContext.Employees.ToList();
            return Json(employees);
        }

        public JsonResult SortByName(bool reverse = false)
        {
            IOrderedQueryable<Employee> employees;
            if (reverse)
                employees = employeesContext.Employees.OrderByDescending(p => p.Name);
            else
                employees = employeesContext.Employees.OrderBy(p => p.Name);

            return Json(employees);
        }

        public JsonResult SortBySurname(bool reverse = false)
        {
            IOrderedQueryable<Employee> employees;
            if (reverse)
                employees = employeesContext.Employees.OrderByDescending(p => p.Surname);
            else
                employees = employeesContext.Employees.OrderBy(p => p.Surname);

            return Json(employees);
        }

        public JsonResult SortByPosition(bool reverse = false)
        {
            IOrderedQueryable<Employee> employees;
            if (reverse)
                employees = employeesContext.Employees.OrderByDescending(p => p.Position);
            else
                employees = employeesContext.Employees.OrderBy(p => p.Position);

            return Json(employees);
        }

        public JsonResult SortByAddress(bool reverse = false)
        {
            IOrderedQueryable<Employee> employees;
            if (reverse)
                employees = employeesContext.Employees.OrderByDescending(p => p.Address);
            else
                employees = employeesContext.Employees.OrderBy(p => p.Address);

            return Json(employees);
        }
    }

}