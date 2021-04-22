using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers.Identity
{
    [Route(WebAPI.Identity.Role)]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleStore<Role> _roleStore;

        public RolesController(WebStoreDB db)
        {
            _roleStore = new RoleStore<Role>(db);
        }
    }
}
