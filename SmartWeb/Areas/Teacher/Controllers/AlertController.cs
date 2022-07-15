using iText.Html2pdf;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartWeb.Models;
using SmartWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            var alert = GetAlertsByStageId(StageID);
            return Json(new { data = alert });
        }
        public IEnumerable<TblAlert> GetAlertsByStageId(int stageId)
        {
            return _unitOfWork.TeacherAlert.GetAll(obj => obj.StageID == stageId && obj.AlertVisible == "yes");
        }
        [HttpPost]
        public IActionResult Export(string GridHtml)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                HtmlConverter.ConvertToPdf(GridHtml, stream);
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
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
        public async Task<IActionResult> OnGet()
        {
            MemoryStream ms = new MemoryStream();

            PdfWriter writer = new PdfWriter(ms);
            PdfDocument pdfDoc = new PdfDocument(writer);
            Document document = new Document(pdfDoc, PageSize.A4, false);
            writer.SetCloseStream(false);


            // empty line
            document.Add(new Paragraph(""));

            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            // empty line
            document.Add(new Paragraph(""));

            // Add table containing data
            document.Add(await GetPdfTable());

            // Page Numbers
            int n = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                  .Format("Page " + i + " of " + n)),
                  559, 806, i, TextAlignment.RIGHT,
                  VerticalAlignment.TOP, 0);
            }

            document.Close();
            byte[] byteInfo = ms.ToArray();
            ms.Write(byteInfo, 0, byteInfo.Length);
            ms.Position = 0;

            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");

            //Uncomment this to return the file as a download
            //fileStreamResult.FileDownloadName = "NorthwindProducts.pdf";

            return fileStreamResult;
        }
        private async Task<Table> GetPdfTable()
        {
            // Table
            Table table = new Table(4, false);

            // Headings
            Cell cellProductId = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("م"));

            Cell cellProductName = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.JUSTIFIED)
               .Add(new Paragraph("التنبيه"));

            table.AddCell(cellProductId);
            table.AddCell(cellProductName);

            int i = 1;
            foreach (var item in GetAlertsByStageId(2))
            {
                Cell cId = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(i.ToString()));

                Cell cName = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .Add(new Paragraph(item.AlertName));

                table.AddCell(cId);
                table.AddCell(cName);
                i++;
            }

            return table;
        }

    }
}
