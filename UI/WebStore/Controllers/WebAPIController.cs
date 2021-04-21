using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.TestAPI;

namespace WebStore.Controllers
{
    public class WebAPIController : Controller
    {
        private readonly IValuesService _valuesService;

        public WebAPIController(IValuesService valuesService) => _valuesService = valuesService;

        public IActionResult Index()
        {
            var value = _valuesService.Get();
            return View(value);
        }
    }
}
