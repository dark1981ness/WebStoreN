using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        #region Register

        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new User
            {
                UserName = model.UserName
            };

            _logger.LogInformation("Регистрация пользователя {0}", user.UserName);

            var registration_result = await _userManager.CreateAsync(user, model.Password);

            if (registration_result.Succeeded)
            {
                _logger.LogInformation("Пользователь {0} успешно зарегистрирован", user.UserName);

                await _userManager.AddToRoleAsync(user, Role._users);

                _logger.LogInformation("Пользователь {0} наделен ролью {1}", user.UserName, Role._users);

                await _signInManager.SignInAsync(user, false);

                _logger.LogInformation("Пользователь {0} вошел в систему сразу после регистрации", user.UserName);

                return RedirectToAction("Index", "Home");
            }

            _logger.LogWarning("Ошибка при регистрации пользователя {0} : {1}",
                user.UserName,
                string.Join(",", registration_result.Errors.Select(e => e.Description)));

            foreach (var error in registration_result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }
        #endregion

        #region Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var login_result = await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
#if DEBUG
                false
#else
                true
#endif

                );

            if (login_result.Succeeded)
            {
                _logger.LogInformation("Пользователь {0} вошёл в систему", model.UserName);
                //if (Url.IsLocalUrl(model.ReturnUrl))
                //    return Redirect(model.ReturnUrl);
                //return RedirectToAction("Index", "Home");
                return LocalRedirect(model.ReturnUrl ?? "/");
            }


            _logger.LogWarning("Ошибка при вводе имени пользователя {0}, либо пароля", model.UserName);

            ModelState.AddModelError("", "Неверное имя пользователя, или пароль!");
            return View(model);
        }
        #endregion

        public async Task<IActionResult> Logout()
        {
            var user_name = User.Identity!.Name;

            await _signInManager.SignOutAsync();

            _logger.LogInformation("Пользователь {0} вышел из системы", user_name);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}
