using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Infrastructure.Services.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {

        private readonly List<Employee> _employees;
        private int _currentMaxId;

        public InMemoryEmployeesData()
        {
            _employees = TestData.Employees;
            _currentMaxId = _employees.DefaultIfEmpty().Max(e => e?.Id ?? 1);
        }

        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_employees.Contains(employee)) return employee.Id; // при реализации с использованием БД эта проверка не нужна

            employee.Id = ++_currentMaxId;
            _employees.Add(employee);

            return employee.Id;
        }

        public Employee Add(string lastName, string firstName, string patronymic, DateTime dOb, DateTime hireDate, decimal salary, string eMail)
        {
            var employee = new Employee()
            {
                LastName = lastName,
                FirstName = firstName,
                Patronymic = patronymic,
                Birthday = dOb,
                HireDate = hireDate,
                Salary = salary,
                EMail = eMail
            };

            Add(employee);
            return employee;
        }

        public bool Delete(int id)
        {
            var db_item = Get(id);

            if (db_item is null) return false;
            return _employees.Remove(db_item);
        }

        public IEnumerable<Employee> Get() => _employees;


        public Employee Get(int id) => _employees.FirstOrDefault(e => e.Id == id);

        public Employee GetByName(string lastName, string firstName, string patronymic) =>
            _employees.FirstOrDefault(e => e.LastName == lastName && e.FirstName == firstName && e.Patronymic == patronymic);

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_employees.Contains(employee)) return; // при реализации с использованием БД эта проверка не нужна

            var db_item = Get(employee.Id);

            if (db_item is null) return;

            db_item.FirstName = employee.FirstName;
            db_item.LastName = employee.LastName;
            db_item.Patronymic = employee.Patronymic;
            db_item.Birthday = employee.Birthday;
            db_item.HireDate = employee.HireDate;
            db_item.Salary = employee.Salary;
            db_item.EMail = employee.EMail;
        }
    }
}
