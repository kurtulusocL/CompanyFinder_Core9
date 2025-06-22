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
    public class StockController : Controller
    {
        readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _stockService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ByQuantity()
        {
            return View(await _stockService.GetAllIncludingByQuantityAsync());
        }
        public async Task<IActionResult> ByProduct(int? id)
        {
            return View(await _stockService.GetAllIncludingByProductIdAsync(id));
        }
        public async Task<IActionResult> ByCompany(int? id)
        {
            return View(await _stockService.GetAllIncludingByCompanyIdIdAsync(id));
        }
        public async Task<IActionResult> StockOps()
        {
            return View(await _stockService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> StockDetail(int? id)
        {
            return View(await _stockService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _stockService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteStock(Stock model, int id)
        {
            var result = await _stockService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(StockOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _stockService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(StockOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _stockService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(StockOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _stockService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(StockOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _stockService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(StockOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
