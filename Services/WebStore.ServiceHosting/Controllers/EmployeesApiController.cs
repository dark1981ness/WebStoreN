using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesApiController(IEmployeesData employeesData) => _employeesData = employeesData;


        [HttpPost]
        public int Add(Employee employee) => _employeesData.Add(employee);

        [HttpPost("employee")]
        public Employee Add(string lastName, string firstName, string patronymic, DateTime dOb, DateTime hireDate, decimal salary, string eMail) => _employeesData.Add(lastName, firstName, patronymic, dOb, hireDate, salary, eMail);

        [HttpDelete("{id}")]
        public bool Delete(int id) => _employeesData.Delete(id);

        [HttpGet]
        public IEnumerable<Employee> Get() => _employeesData.Get();

        [HttpGet("{id}")]
        public Employee Get(int id) => _employeesData.Get(id);

        [HttpGet("employee")]
        public Employee GetByName(string lastName, string firstName, string patronymic) => _employeesData.GetByName(lastName, firstName, patronymic);

        [HttpPut]
        public void Update(Employee employee) => _employeesData.Update(employee);
    }
}
