using System;
using Microsoft.AspNetCore.Mvc;

namespace SmartWeb.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class PrintController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                ViewBag.Error = "error";
                ViewBag.Message = "من فضلك تأكد من البيانات";
                return View();
            }
        }
    }
}
