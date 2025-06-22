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
    public class ReportAnswerController : Controller
    {
        readonly IReportAnswerService _reportAnswerService;
        public ReportAnswerController(IReportAnswerService reportAnswerService)
        {
            _reportAnswerService = reportAnswerService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _reportAnswerService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> SatisfieldAnswer()
        {
            return View(await _reportAnswerService.GetAllIncludingSatisfieldsAsync());
        }
        public async Task<IActionResult> NotSatisfieldAnswer()
        {
            return View(await _reportAnswerService.GetAllIncludingNotSatisfieldsAsync());
        }
        public async Task<IActionResult> ByReport(int? id)
        {
            return View(await _reportAnswerService.GetAllIncludingByReportIdAsync(id));
        }
        public async Task<IActionResult> ByReportForAdmin(int? id)
        {
            return View(await _reportAnswerService.GetAllIncludingForAdminByReportIdAsync(id));
        }
        public async Task<IActionResult> ReportAnswerOps()
        {
            return View(await _reportAnswerService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> ReportAnswerDetail(int? id)
        {
            return View(await _reportAnswerService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _reportAnswerService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string title, string desc, int? reportId, int id)
        {
            var result = await _reportAnswerService.UpdateAsync(title, desc, reportId, id);
            if (result)
            {
                TempData["reportAnswerUpdatedSuccessfull"] = "Report Answer Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = id });
            }
            TempData["reportAnswerUpdateError"] = "Report Answer Update Error";
            return RedirectToAction(nameof(Edit), new { id = id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _reportAnswerService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteReportAnswer(ReportAnswer model, int id)
        {
            var result = await _reportAnswerService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(ReportAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _reportAnswerService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _reportAnswerService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _reportAnswerService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _reportAnswerService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
