using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.Controllers
{
    [AuditLog]
    [ExceptionHandler]
    [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
    public class ReportController : Controller
    {
        readonly IReportService _reportService;
        readonly IReportAnswerService _reportAnswerService;
        public ReportController(IReportService reportService, IReportAnswerService reportAnswerService)
        {
            _reportService = reportService;
            _reportAnswerService = reportAnswerService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _reportService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> AnsweredReport()
        {
            return View(await _reportService.GetAllIncludingAnsweredAsync());
        }
        public async Task<IActionResult> NotAnsweredReport()
        {
            return View(await _reportService.GetAllIncludingNotAnsweredAsync());
        }
        public async Task<IActionResult> FixedReport()
        {
            return View(await _reportService.GetAllIncludingFixedReportsAsync());
        }
        public async Task<IActionResult> NotFixedReport()
        {
            return View(await _reportService.GetAllIncludingNotFixedReportsAsync());
        }
        public async Task<IActionResult> ByCategory(int id)
        {
            return View(await _reportService.GetAllIncludingReportByCategoryIdAsync(id));
        }
        public async Task<IActionResult> ByBlog(int? id)
        {
            return View(await _reportService.GetAllIncludingReportByBlogIdAsync(id));
        }
        public async Task<IActionResult> ByComment(int? id)
        {
            return View(await _reportService.GetAllIncludingReportByCommentIdAsync(id));
        }
        public async Task<IActionResult> ByCommentAnswer(int? id)
        {
            return View(await _reportService.GetAllIncludingReportByCommentAnswerIdAsync(id));
        }
        public async Task<IActionResult> ByCompany(int? id)
        {
            return View(await _reportService.GetAllIncludingReportByCompanyIdAsync(id));
        }
        public async Task<IActionResult> ByCompanyForm(int? id)
        {
            return View(await _reportService.GetAllIncludingReportByCompanyFormIdAsync(id));
        }
        public async Task<IActionResult> ByCompanyFormAnswer(int? id)
        {
            return View(await _reportService.GetAllIncludingReportByCompanyFormAnswerIdAsync(id));
        }
        public async Task<IActionResult> ByPartnership(int? id)
        {
            return View(await _reportService.GetAllIncludingReportByCompanyPartnershipIdAsync(id));
        }
        public async Task<IActionResult> ByProduct(int? id)
        {
            return View(await _reportService.GetAllIncludingReportByProductIdAsync(id));
        }
        public async Task<IActionResult> ByUser(string id)
        {
            return View(await _reportService.GetAllIncludingReportByUserIdAsync(id));
        }
        public async Task<IActionResult> ByUserForAdmin(string id)
        {
            return View(await _reportService.GetAllIncludingReportForAdminByUserIdAsync(id));
        }
        public async Task<IActionResult> ReportOps()
        {
            return View(await _reportService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> ReportDetail(int? id)
        {
            return View(await _reportService.GetByIdAsync(id));
        }
        public async Task<IActionResult> CreateAnswer(int? id)
        {
            ViewBag.ReportId = await _reportService.GetByIdAsync(id);
            ReportAnswer model = new ReportAnswer
            {
                ReportId = id
            };
            return View("CreateAnswer", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnswer(string title, string desc, int? reportId)
        {
            var result = await _reportAnswerService.CreateAsync(title, desc, reportId);
            if (result)
            {
                TempData["reportAnswerSentSuccessfull"] = "Report Answer Sent Successfull";
                return RedirectToAction(nameof(CreateAnswer));
            }
            TempData["reportAnswerSentError"] = "Report Answer Sent Error";
            return RedirectToAction(nameof(CreateAnswer));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _reportService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteReport(Report model, int id)
        {
            var result = await _reportService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(ReportOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetFixed(int id, Report model)
        {
            var result = await _reportService.SetFixedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportDetail), new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotFixed(int id, Report model)
        {
            var result = await _reportService.SetNotFixedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportDetail), new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _reportService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _reportService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _reportService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _reportService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
