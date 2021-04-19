using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using WebStore.Clients.Base;
using WebStore.Domain.Models;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient: BaseClient, IEmployeesData
    {
        private readonly ILogger<EmployeesClient> _logger;

        public EmployeesClient(IConfiguration configuration, ILogger<EmployeesClient> logger)
            : base(configuration, WebAPI.Employees) => _logger = logger;

        public int Add(Employee employee) => Post(Address, employee).Content.ReadAsAsync<int>().Result;


        public Employee Add(string lastName, string firstName, string patronymic, DateTime dOb, DateTime hireDate, decimal salary, string eMail) =>
            Post($"{Address}/employee?LastName={lastName}&FirstName={firstName}&Patronymic={patronymic}", "")
            .Content.ReadAsAsync<Employee>().Result;

        public bool Delete(int id)
        {
            _logger.LogInformation($"Удаление сотрудника id:{id}");
            var result = Delete($"{Address}/{id}").IsSuccessStatusCode;
            _logger.LogInformation($"Удаление сотрудника id:{id} - {(result ? "выполнено" : "не найден")} выполнено");
            return result;
        }

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Address);

        public Employee Get(int id) => Get<Employee>($"{Address}/{id}");

        public Employee GetByName(string lastName, string firstName, string patronymic) =>
            Get<Employee>($"{Address}/employee?LastName={lastName}&FirstName={firstName}&Patronymic={patronymic}");

        public void Update(Employee employee) => Put(Address, employee);
    }
}
