using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAllRoles() => await _roleStore.Roles.ToArrayAsync();

        [HttpPost]
        public async Task<bool> CreateAsync(Role role)
        {
            var creation_result = await _roleStore.CreateAsync(role);
            // Добавить логирование в случае Succeeded == false
            return creation_result.Succeeded;
        }

        [HttpPut]
        public async Task<bool> UpdateAsync(Role role)
        {
            var uprate_result = await _roleStore.UpdateAsync(role);
            return uprate_result.Succeeded;
        }

        [HttpPost("Delete")]
        public async Task<bool> DeleteAsync(Role role)
        {
            var delete_result = await _roleStore.DeleteAsync(role);
            return delete_result.Succeeded;
        }

        [HttpPost("GetRoleId")]
        public async Task<string> GetRoleIdAsync([FromBody] Role role) => await _roleStore.GetRoleIdAsync(role);

        [HttpPost("GetRoleName")]
        public async Task<string> GetRoleNameAsync([FromBody] Role role) => await _roleStore.GetRoleNameAsync(role);

        [HttpPost("SetRoleName/{name}")]
        public async Task<string> SetRoleNameAsync(Role role, string name)
        {
            await _roleStore.SetRoleNameAsync(role, name);
            await _roleStore.UpdateAsync(role);
            return role.Name;
        }

        [HttpPost("GetNormalizedRoleName")]
        public async Task<string> GetNormalizedRoleNameAsync(Role role) => await _roleStore.GetNormalizedRoleNameAsync(role);

        [HttpPost("SetNormalizedRoleName/{name}")]
        public async Task<string> SetNormalizedRoleNameAsync(Role role, string name)
        {
            await _roleStore.SetNormalizedRoleNameAsync(role, name);
            await _roleStore.UpdateAsync(role);
            return role.NormalizedName;
        }

        [HttpGet("FindById/{id}")]
        public async Task<Role> FindByIdAsync(string id) => await _roleStore.FindByIdAsync(id);

        [HttpGet("FindByName/{name}")]
        public async Task<Role> FindByNameAsync(string name) => await _roleStore.FindByNameAsync(name);
    }
}
