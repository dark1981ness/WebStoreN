using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers.Identity
{
    [Route(WebAPI.Identity.User)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> _userStore;

        public UsersController(WebStoreDB db)
        {
            _userStore = new UserStore<User, Role, WebStoreDB>(db);
            //_userStore.AutoSaveChanges = false;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAllUsers() => await _userStore.Users.ToArrayAsync();
    }
}
