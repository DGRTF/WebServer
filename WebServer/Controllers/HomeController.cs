using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using WebServer.Models;

namespace EmptyApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(EmployeesContext context, IWebHostEnvironment appEnvironment)
        {
            employeesContext = context;
            AppEnvironment = appEnvironment;
        }

        EmployeesContext employeesContext { get; set; }

        IWebHostEnvironment AppEnvironment {get;set;}

        public JsonResult Index(int quantity = 10)
        {
            var employees = employeesContext.Employees.AsNoTracking().ToList();
            return Json(employees);
        }

        public JsonResult SortByName(bool reverse = false)
        {
            IQueryable<Employee> employees;
            if (reverse)
                employees = employeesContext.Employees.AsNoTracking().OrderByDescending(p => p.Name);
            else
                employees = employeesContext.Employees.AsNoTracking().OrderBy(p => p.Name);

            return Json(employees);
        }

        public JsonResult SortBySurname(bool reverse = false)
        {
            IQueryable<Employee> employees;
            if (reverse)
                employees = employeesContext.Employees.AsNoTracking().OrderByDescending(p => p.Surname);
            else
                employees = employeesContext.Employees.AsNoTracking().OrderBy(p => p.Surname);

            return Json(employees);
        }

        public JsonResult SortByPosition(bool reverse = false)
        {
            IQueryable<Employee> employees;
            if (reverse)
                employees = employeesContext.Employees.AsNoTracking().OrderByDescending(p => p.Position);
            else
                employees = employeesContext.Employees.AsNoTracking().OrderBy(p => p.Position);

            return Json(employees);
        }

        public JsonResult SortByAddress(bool reverse = false)
        {
            IQueryable<Employee> employees;
            if (reverse)
                employees = employeesContext.Employees.AsNoTracking().OrderByDescending(p => p.Address);
            else
                employees = employeesContext.Employees.AsNoTracking().OrderBy(p => p.Address);

            return Json(employees);
        }

        public JsonResult DeleteEmployee(int id)
        {
            Employee employee = employeesContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null && employee.Id == id)
            {
                employeesContext.Employees.Remove(employee);
                employeesContext.SaveChanges();
            }
            var employees = employeesContext.Employees.AsNoTracking().ToList();
            return Json(employees);
        }

        [HttpPost]
        public JsonResult SortByRemoteWork(bool reverse = false)
        {
            IQueryable<Employee> employees;
            if (reverse)
                employees = employeesContext.Employees.AsNoTracking().OrderByDescending(p => p.RemoteWork);
            else
                employees = employeesContext.Employees.AsNoTracking().OrderBy(p => p.RemoteWork);

            return Json(employees);
        }

        [HttpPost]
        public JsonResult AddEmployee(
            string name,
            string surname,
            string address,
            bool remoteWork,
            string position,
            string birthDate
            )
        {
            string type;
            string path = "/fileImg/";

            if (Request.Form.Files.Count != 0)
            {
                type = Request.Form.Files[0].ContentType;
                IFormFile uploadedFile = Request.Form.Files[0];
                if (type == "image/jpeg")
                {
                    // путь к папке Files
                    path += uploadedFile.FileName.Replace(" ", string.Empty);
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(AppEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        uploadedFile.CopyTo(fileStream);
                    }
                }
            }


            string header = Request.Headers["Content-Type"];

            Employee employee = new Employee
            {
                Name = name,
                Preview = path,
                Surname = surname,
                BirthDate = birthDate,
                Position = position,
                RemoteWork = remoteWork,
                Address = address
            };

            employeesContext.Employees.Add(employee);

            var employees = employeesContext.Employees.AsNoTracking().ToList();
            employeesContext.SaveChanges();
            return Json(employees);
        }

        public JsonResult EditEmployee(
            int id,
            string name,
            string surname,
            string address,
            bool remoteWork,
            string preview,
            string position,
            string birthDate
            )
        {
            Employee employee = employeesContext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null && employee.Id == id)
            {
                employee.Name = name;
                employee.Surname = surname;
                employee.Address = address;
                employee.RemoteWork = remoteWork;
                employee.Preview = preview;
                employee.Position = position;
                employee.BirthDate = birthDate;

                employeesContext.SaveChanges();
            }
            var employees = employeesContext.Employees.AsNoTracking().ToList();
            return Json(employees);
        }
    }

}