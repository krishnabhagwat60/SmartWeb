using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartWeb.Models;
using SmartWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SmartWeb.Controllers
{
    public class HomeController : Controller
    {
        Helper.Helper helper = new Helper.Helper();
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<TblApplicationUser> _userManager;
        private readonly SignInManager<TblApplicationUser> _signInManager;

        public HomeController(IUnitOfWork unitOfWork, UserManager<TblApplicationUser> userManager, SignInManager<TblApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index(string returnurl = null)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return LocalRedirect(Url.Content("~/Teacher"));
            }
            ViewData["ReturnUrl"] = returnurl;
            var loginModel = new LoginModel();
            return View(loginModel);
        }

        public IActionResult Authorized()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            if (ModelState.IsValid)
            {
                var currentUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(obj => obj.PhoneNumber == loginModel.PhoneNumber.Trim() && obj.Password == loginModel.Password && obj.UserVisible == "yes");
                if (currentUser == null)
                {
                    ViewBag.Error = "error";
                    ViewBag.Message = "تأكد من رقم الهاتف وكلمة المرور";
                }
                var user = await _signInManager.PasswordSignInAsync(currentUser, loginModel.Password, true, true);
                if (user.Succeeded)
                {
                    return LocalRedirect(Url.Content("~/Teacher"));
                }
                else
                {
                    ViewBag.Error = "error";
                    ViewBag.Message = "تأكد من تسجيل كلمة المرور بشكل صحيح";
                }
            }
            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" }, null);
        }

        public IActionResult Error(string id)
        {
            if (id == "404")
            {
                ViewBag.Message = "Not Found";
            }
            else if (id == "403")
            {
                ViewBag.Message = "Authorized";
            }
            else
            {
                ViewBag.Message = "Error";
            }
            return View();
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            return "";
        }
    }
}
