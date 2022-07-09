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
    public class AlertController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        string Title = "التنبيهات";

        public AlertController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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

        [HttpGet]
        public IActionResult GetAlert(int StageID)
        {
            var alert = _unitOfWork.TeacherAlert.GetAll(obj => obj.StageID == StageID && obj.AlertVisible == "yes");
            return Json(new { data = alert });
        }

        [HttpGet]
        public IActionResult Create()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblAlert tblAlert)
        {
            TblAlert alert = new TblAlert
            {
                StageID = tblAlert.StageID,
                AlertName = tblAlert.AlertName.Trim(),
                AlertVisible = "yes"
            };
            _unitOfWork.TeacherAlert.Add(alert);
            _unitOfWork.Save();
            return Json(new { isValid = true, title = Title, message = "تم الحفظ بنجاح" });
        }
    }
}
