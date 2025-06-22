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
    public class CompanyCategoryController : Controller
    {
        readonly ICompanyCategoryService _companyCategoryService;
        public CompanyCategoryController(ICompanyCategoryService companyCategoryService)
        {
            _companyCategoryService = companyCategoryService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _companyCategoryService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> CompanyCategoryOps()
        {
            return View(await _companyCategoryService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> CompanyCategoryDetail(int? id)
        {
            return View(await _companyCategoryService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyCategory model)
        {
            var result = await _companyCategoryService.CreateAsync(model);
            if (result)
            {
                TempData["companyCategoryCreatedSuccessfull"] = "Company Category Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["companyCategoryCreatedError"] = "Company Category Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _companyCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyCategory model)
        {
            var result = await _companyCategoryService.UpdateAsync(model);
            if (result)
            {
                TempData["companyCategoryUpdatedSuccessfull"] = "Company Category Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["companyCategoryUpdateError"] = "Company Category Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _companyCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCompanyCategory(CompanyCategory model, int id)
        {
            var result = await _companyCategoryService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _companyCategoryService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _companyCategoryService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _companyCategoryService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _companyCategoryService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
