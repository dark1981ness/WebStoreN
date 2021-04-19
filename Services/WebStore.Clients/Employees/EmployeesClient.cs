using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Interfaces;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient: BaseClient
    {
        public EmployeesClient(IConfiguration configuration) : base(configuration, WebAPI.Employees)
        {

        }
    }
}
