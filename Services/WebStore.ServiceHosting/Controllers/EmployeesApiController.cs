using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WebStore.Domain.Models;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/employees")]
    [Route(WebAPI.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;
        private readonly ILogger<EmployeesApiController> _logger;

        public EmployeesApiController(IEmployeesData employeesData, ILogger<EmployeesApiController> logger)
        {
            _employeesData = employeesData;
            _logger = logger;
        }

        [HttpPost]
        public int Add(Employee employee)
        {
            _logger.LogInformation($"Добавление сотрудника {employee}");
            return _employeesData.Add(employee);
        }

        [HttpPost("employee")]
        public Employee Add(string lastName, string firstName, string patronymic, DateTime dOb, DateTime hireDate, decimal salary, string eMail)
        {
            _logger.LogInformation($"Добавление сотрудника {lastName} {firstName} {patronymic} {dOb} {hireDate} {salary} {eMail}");
            return _employeesData.Add(lastName, firstName, patronymic, dOb, hireDate, salary, eMail);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _logger.LogInformation($"Удаление сотрудника id:{id}");
            var result = _employeesData.Delete(id);
            _logger.LogInformation($"Удаление сотрудника id:{id} - {(result ? "выполнено" : "не найден")} выполнено");
            return result;
        }

        [HttpGet]
        public IEnumerable<Employee> Get() => _employeesData.Get();

        [HttpGet("{id}")]
        public Employee Get(int id) => _employeesData.Get(id);

        [HttpGet("employee")]
        public Employee GetByName(string lastName, string firstName, string patronymic) => _employeesData.GetByName(lastName, firstName, patronymic);

        [HttpPut]
        public void Update(Employee employee)
        {
            _logger.LogInformation($"Редактирование сотрудника {employee}");
            _employeesData.Update(employee);
        }
    }
}
