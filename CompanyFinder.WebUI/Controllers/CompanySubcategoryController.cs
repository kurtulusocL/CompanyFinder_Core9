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
    public class CompanySubcategoryController : Controller
    {
        readonly ICompanySubcategoryService _companySubcategoryService;
        readonly ICompanyCategoryService _companyCategoryService;
        public CompanySubcategoryController(ICompanySubcategoryService companySubcategoryService, ICompanyCategoryService companyCategoryService)
        {
            _companySubcategoryService = companySubcategoryService;
            _companyCategoryService = companyCategoryService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _companySubcategoryService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> Category(int? id)
        {
            return View(await _companySubcategoryService.GetAllIncludingByCategoryIdAsync(id));
        }
        public async Task<IActionResult> CompanySubcategoryOps()
        {
            return View(await _companySubcategoryService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> CompanySubcategoryDetail(int? id)
        {
            return View(await _companySubcategoryService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.CompanyCategories = await _companyCategoryService.GetAllIncludingForAddSubcategoryAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanySubcategory model)
        {
            var result = await _companySubcategoryService.CreateAsync(model);
            if (result)
            {
                TempData["companySubcategoryCreate"] = "Company Subcategory Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["companySubcategoryCreateError"] = "Company Subcategory Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.CompanyCategories = await _companyCategoryService.GetAllIncludingForAddSubcategoryAsync();
            var data = await _companySubcategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanySubcategory model)
        {
            var result = await _companySubcategoryService.UpdateAsync(model);
            if (result)
            {
                TempData["companySubcategoryUpdate"] = "Company Subcategory Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["companySubcategoryUpdateError"] = "Company Subcategory Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _companySubcategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCompanySubcategory(CompanySubcategory model, int id)
        {
            var result = await _companySubcategoryService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CompanySubcategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _companySubcategoryService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanySubcategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _companySubcategoryService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanySubcategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _companySubcategoryService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanySubcategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _companySubcategoryService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanySubcategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
