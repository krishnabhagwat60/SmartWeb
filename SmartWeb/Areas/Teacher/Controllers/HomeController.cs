using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using SmartWeb.Models;
using SmartWeb.Repository.IRepository;

namespace SmartWeb.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize]

    public class HomeController : Controller
    {
        private readonly UserManager<TblApplicationUser> _userManager;
        private readonly SignInManager<TblApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(UserManager<TblApplicationUser> userManager, SignInManager<TblApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            var currentUser = _unitOfWork.ApplicationUser.Get(_userManager.GetUserId(User));
            if (currentUser == null || currentUser.UserVisible == "no")
            {
                _signInManager.SignOutAsync().Wait();
                return Json(new { isValid = false, code = "401", path = MyVariables.WebSitePath + "Home/index" });
            }
            return Json(new { isValid = true, category = currentUser.Category, firstname = currentUser.FirstName, fullname = currentUser.FullName, imageurl = currentUser.ImageUrl, jobtitle = currentUser.JobTitle });
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Home", new { area = "" }, null);
        }
    }
}
