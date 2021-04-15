using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Infrastructure.Services.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> Get();

        Employee Get(int id);

        Employee GetByName(string lastName, string firstName, string patronymic);

        int Add(Employee employee);

        Employee Add(string lastName, string firstName, string patronymic, DateTime dOb, DateTime hireDate, decimal salary, string eMail);

        void Update(Employee employee);

        bool Delete(int id);
    }
}
