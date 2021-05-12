using System;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Filters;

namespace WebStore.Controllers
{
    [ActionDescription("Главный контроллер")]
    
    public class HomeController : Controller
    {
        [ActionDescription("Главное действие")]
        [AddHeader("Test","Header value")]
        public IActionResult Index() => View();

        public IActionResult Throw() => throw new ApplicationException("Test error!");

        public IActionResult SecondAction(string id) => Content($"Action with value id:{id}");

        public IActionResult ContactUs() => View();

        public IActionResult PageNotFound() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Blog() => View();

        public IActionResult Cart() => View();

        public IActionResult Login() => View();

        public IActionResult ProductDetails() => View();

        public IActionResult Shop() => View();

        public IActionResult Checkout() => View();

        public IActionResult ErrorStatus(string code) => code switch
        {
            "404" => RedirectToAction(nameof(PageNotFound)),
            _ => Content($"Error code: {code}")
        };
        
    }
}
