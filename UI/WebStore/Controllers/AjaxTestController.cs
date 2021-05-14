using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AjaxTestController : Controller
    {
        public IActionResult Index() => View();

        public async Task<IActionResult> GetJSON(int? id, string msg, int delay = 2000)
        {
            await Task.Delay(delay);

            return Json(new
            {
                Message = $"Response (id:{id ?? -1}): {msg ?? "<null>"}",
                ServerTime = DateTime.Now
            });
        }

        public async Task<IActionResult> GetHTML(int? id, string msg, int delay = 2000)
        {
            await Task.Delay(delay);

            return PartialView("Partial/_DataView", new AjaxTestDataViewModel(id, msg, DateTime.Now));
        }
    }
}
