using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartWeb.Models;
using SmartWeb.Repository.IRepository;
using System;
using System.Linq;

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
