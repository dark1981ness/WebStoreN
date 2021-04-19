using Microsoft.Extensions.Configuration;
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
        public EmployeesClient(IConfiguration configuration) : base(configuration, WebAPI.Employees)
        {

        }

        public int Add(Employee employee) => Post(Address, employee).Content.ReadAsAsync<int>().Result;


        public Employee Add(string lastName, string firstName, string patronymic, DateTime dOb, DateTime hireDate, decimal salary, string eMail) =>
            Post($"{Address}/employee?LastName={lastName}&FirstName={firstName}&Patronymic={patronymic}", "")
            .Content.ReadAsAsync<Employee>().Result;

        public bool Delete(int id) => Delete($"{Address}/{id}").IsSuccessStatusCode;

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(Address);

        public Employee Get(int id) => Get<Employee>($"{Address}/{id}");

        public Employee GetByName(string lastName, string firstName, string patronymic) =>
            Get<Employee>($"{Address}/employee?LastName={lastName}&FirstName={firstName}&Patronymic={patronymic}");

        public void Update(Employee employee) => Put(Address, employee);
    }
}
