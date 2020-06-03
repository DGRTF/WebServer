using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmptyApp.Controllers
{
    public class HomeController : Controller
    {

        private List<Employee> Employees { get; set; }

        public JsonResult Index(int quantity = 1)
        {
            Staff staff = new Staff();
            Employees = staff.Emloyees;
            return Json(Employees);
        }

        public string SortByName(int count, int quantity)
        {

            return "gena";
        }
    }

    class Employee
    {
        public Employee(
            string preview,
            string name,
            string surname,
            string birthDate,
            //int age,
            string position,
            bool remoteWork,
            string address
            )
        {
            Preview = preview;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            //Age = age;
            Position = position;
            RemoteWork = remoteWork;
            Address = address;
        }



        public string Preview { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string BirthDate { get; set; }

        //public int Age { get; set; }

        public string Position { get; set; }

        public bool RemoteWork { get; set; }

        public string Address { get; set; }

    }


    class Staff
    {
        public Staff()
        {
            Emloyees = new List<Employee>(){
                new Employee(
                "./img/Gena.jpeg",
                "Гена",
                "Попов",
                "05/04/1996",
                "слесарь",
                true,
                "г. СПб, ул. Конюшенная, д. 25, кв. 47"
                ),
            new Employee(
                "./img/Gena.jpeg",
                "Петя",
                "Рогов",
                "12/08/1999",
                "сантехник",
                true,
                "г. СПб, ул. Пенная, д. 56, кв. 16"
                )
            };
        }

        public List<Employee> Emloyees { get; private set; }
    }

}