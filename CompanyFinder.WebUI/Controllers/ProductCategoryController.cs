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
    public class ProductCategoryController : Controller
    {
        readonly IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _productCategoryService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ProductCategoryOps()
        {
            return View(await _productCategoryService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> ProductCategoryDetail(int? id)
        {
            return View(await _productCategoryService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategory model)
        {
            var result = await _productCategoryService.CreateAsync(model);
            if (result)
            {
                TempData["productCategoryCreatedSuccessfull"] = "Product Category Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["productCategoryCreatedError"] = "Product Category Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _productCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductCategory model)
        {
            var result = await _productCategoryService.UpdateAsync(model);
            if (result)
            {
                TempData["productCategoryUpdatedSuccessfull"] = "Product Category Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["productCategoryUpdateError"] = "Product Category Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _productCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteProductCategory(ProductCategory model, int id)
        {
            var result = await _productCategoryService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(ProductCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _productCategoryService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _productCategoryService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _productCategoryService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _productCategoryService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
