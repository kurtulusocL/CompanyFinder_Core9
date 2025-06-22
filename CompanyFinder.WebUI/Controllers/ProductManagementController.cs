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
    public class ProductManagementController : Controller
    {
        readonly IProductService _productService;
        public ProductManagementController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _productService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> AvailableProduct()
        {
            return View(await _productService.GetAllIncludingAvailableProductsAsync());
        }
        public async Task<IActionResult> NotAvailableProduct()
        {
            return View(await _productService.GetAllIncludingNotAvailebleProductsAsync());
        }
        public async Task<IActionResult> CommentableProduct()
        {
            return View(await _productService.GetAllIncludingCommentableProductsAsync());
        }
        public async Task<IActionResult> NotCommentableProduct()
        {
            return View(await _productService.GetAllIncludingNotCommentableProductsAsync());
        }
        public async Task<IActionResult> QuestionableProduct()
        {
            return View(await _productService.GetAllIncludingQuestionableProductsAsync());
        }
        public async Task<IActionResult> NotQuestionableProduct()
        {
            return View(await _productService.GetAllIncludingNotQuestionableProductsAsync());
        }
        public async Task<IActionResult> WeeklyMostPopularProducts()
        {
            return View(await _productService.GetAllIncludingWeeklyPopularProductsForAdminAsync());
        }
        public async Task<IActionResult> ProductCategory(int id)
        {
            return View(await _productService.GetAllIncludingByCategoryIdAsync(id));
        }
        public async Task<IActionResult> ProductSubcategory(int? id)
        {
            return View(await _productService.GetAllIncludingBySubcategoryIdAsync(id));
        }
        public async Task<IActionResult> ProductCompany(int? id)
        {
            return View(await _productService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> UserProduct(string id)
        {
            return View(await _productService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> UserProductForAdmin(string id)
        {
            return View(await _productService.GetAllIncludingByUserIdForAdminAsync(id));
        }
        public async Task<IActionResult> ProductCompanyForAdmin(int? id)
        {
            return View(await _productService.GetAllIncludingByCompanyIdForAdminAsync(id));
        }
        public async Task<IActionResult> ProductOps()
        {
            return View(await _productService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> ProductDetail(int? id)
        {
            return View(await _productService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _productService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteProduct(Product model, int id)
        {
            var result = await _productService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(ProductOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _productService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _productService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _productService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _productService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProductOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
