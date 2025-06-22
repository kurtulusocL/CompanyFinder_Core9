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
    public class ProductSubcategoryController : Controller
    {
        readonly IProductCategoryService _productCategoryService;
        readonly IProductSubcategoryService _productSubcategoryService;
        public ProductSubcategoryController(IProductCategoryService productCategoryService, IProductSubcategoryService productSubcategoryService)
        {
            _productCategoryService = productCategoryService;
            _productSubcategoryService = productSubcategoryService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _productSubcategoryService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> Category(int? id)
        {
            return View(await _productSubcategoryService.GetAllIncludingByCategoryIdAsync(id));
        }
        public async Task<IActionResult> ProductSubcategoryOps()
        {
            return View(await _productSubcategoryService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> ProductSubcategoryDetail(int? id)
        {
            return View(await _productSubcategoryService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.ProductCategories = await _productCategoryService.GetAllIncludingForAddSubcategoryAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductSubcategory model)
        {
            var result = await _productSubcategoryService.CreateAsync(model);
            if (result)
            {
                TempData["productSubcategoryCreate"] = "Product Subcategory Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["productSubcategoryCreateError"] = "Product Subcategory Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ProductCategories = await _productCategoryService.GetAllIncludingForAddSubcategoryAsync();
            var data = await _productSubcategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductSubcategory model)
        {
            var result = await _productSubcategoryService.UpdateAsync(model);
            if (result)
            {
                TempData["productSubcategoryUpdate"] = "Product Subcategory Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["productSubcategoryUpdateError"] = "Product Subcategory Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _productSubcategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteProductSubcategory(ProductSubcategory model, int id)
        {
            var result = await _productSubcategoryService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(ProductSubcategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _productSubcategoryService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductSubcategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _productSubcategoryService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductSubcategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _productSubcategoryService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductSubcategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _productSubcategoryService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductSubcategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
