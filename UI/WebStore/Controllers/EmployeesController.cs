using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    //[Route("Staff")]

    [Authorize]
    public class EmployeesController : Controller
    {
        //private readonly List<Employee> _Employees;
        private readonly IEmployeesData _employessData;

        public EmployeesController(IEmployeesData employessData)
        {
            //_Employees = TestData.Employees;
            _employessData = employessData;
        }

        //[Route("all")]
        public IActionResult Index() => View(_employessData.Get());

        //[Route("info-(id-{id})")]
        public IActionResult Details(int id)
        {
            Employee employee = _employessData.Get(id);
            if (employee is null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [Authorize(Roles = Role._administrators)]
        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        [Authorize(Roles = Role._administrators)]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return View(new EmployeeViewModel());
            }
            Employee employee = _employessData.Get((int)id);
            if (employee is null)
            {
                return NotFound();
            }
            return View(new EmployeeViewModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Birthday = employee.Birthday,
                HireDate = employee.HireDate,
                Salary = employee.Salary,
                EMail = employee.EMail
            });
        }

        [Authorize(Roles = Role._administrators)]
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Patronymic = model.Patronymic,
                    Birthday = model.Birthday,
                    HireDate = model.HireDate,
                    Salary = model.Salary,
                    EMail = model.EMail
                };

                if (employee.Id == 0)
                    _employessData.Add(employee);
                else
                    _employessData.Update(employee);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = Role._administrators)]
        public IActionResult Delete(int id)
        {
            //Employee employee = _Employees.SingleOrDefault(s => s.Id == id);
            //_Employees.Remove(employee);
            if (id <= 0) return BadRequest();

            var employee = _employessData.Get(id);

            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Birthday = employee.Birthday,
                HireDate = employee.HireDate,
                Salary = employee.Salary,
                EMail = employee.EMail
            });
        }

        [Authorize(Roles = Role._administrators)]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _employessData.Delete(id);
            return RedirectToAction("Index");
        }

        #region Edit w/o EmployeeViewModel
        //[HttpPost]
        //public IActionResult Edit(Employee employee, int id)

        //{
        //    _Employees.Where(s => s.Id == id)
        //        .Select(s =>
        //        {
        //            s.FirstName = employee.FirstName;
        //            s.LastName = employee.LastName;
        //            s.Patronymic = employee.Patronymic;
        //            s.Birthday = employee.Birthday;
        //            s.HireDate = employee.HireDate;
        //            s.Salary = employee.Salary;
        //            s.EMail = employee.EMail;
        //            return s;
        //        }).ToList();

        //    return RedirectToAction("Index");
        //}
        #endregion

    }
}
