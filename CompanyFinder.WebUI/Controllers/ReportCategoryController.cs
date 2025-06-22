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
    public class ReportCategoryController : Controller
    {
        readonly IReportCategoryService _reportCategoryService;
        public ReportCategoryController(IReportCategoryService reportCategoryService)
        {
            _reportCategoryService = reportCategoryService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _reportCategoryService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ReportCategoryOps()
        {
            return View(await _reportCategoryService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> ReportCategoryDetail(int? id)
        {
            return View(await _reportCategoryService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReportCategory model)
        {
            var result = await _reportCategoryService.CreateAsync(model);
            if (result)
            {
                TempData["reportCategoryCreatedSuccessfull"] = "Report Category Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["reportCategoryCreatedError"] = "Report Category Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _reportCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReportCategory model)
        {
            var result = await _reportCategoryService.UpdateAsync(model);
            if (result)
            {
                TempData["reportCategoryUpdatedSuccessfull"] = "Report Category Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["reportCategoryUpdateError"] = "Report Category Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _reportCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteReportCategory(ReportCategory model, int id)
        {
            var result = await _reportCategoryService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(ReportCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _reportCategoryService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _reportCategoryService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _reportCategoryService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _reportCategoryService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReportCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
