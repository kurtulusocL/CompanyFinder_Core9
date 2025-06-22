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
    public class PricingCategoryController : Controller
    {
        readonly IPricingCategoryService _pricingCategoryService;
        public PricingCategoryController(IPricingCategoryService pricingCategoryService)
        {
            _pricingCategoryService = pricingCategoryService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _pricingCategoryService.GettAllIncludingAsync());
        }
        public async Task<IActionResult> PricingCategoryOps()
        {
            return View(await _pricingCategoryService.GettAllIncludingForAdminAsync());
        }
        public async Task<IActionResult>PricingCategoryDetail(int? id)
        {
            return View(await _pricingCategoryService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PricingCategory model)
        {
            var result = await _pricingCategoryService.CreateAsync(model);
            if (result)
            {
                TempData["pricingCategoryCreate"] = "Pricing Category Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["pricingCategoryCreateError"] = "Pricing Category Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _pricingCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PricingCategory model)
        {
            var result = await _pricingCategoryService.UpdateAsync(model);
            if (result)
            {
                TempData["pricingCategoryUpdate"] = "Pricing Category Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["pricingCategoryUpdateError"] = "Pricing Category Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _pricingCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeletePricingCategory(PricingCategory model, int id)
        {
            var result = await _pricingCategoryService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(PricingCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _pricingCategoryService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _pricingCategoryService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _pricingCategoryService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _pricingCategoryService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
