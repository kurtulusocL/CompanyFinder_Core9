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
    public class CustomerController : Controller
    {
        readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _customerService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ByCompany(int? id)
        {
            return View(await _customerService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> ByStatus(int id)
        {
            return View(await _customerService.GetAllIncludingByStatusIdAsync(id));
        }
        public async Task<IActionResult> CustomerOps()
        {
            return View(await _customerService.GetAllIncludingForAdminAsync());
        }
        public async Task<IActionResult> CustomerDetail(int? id)
        {
            return View(await _customerService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _customerService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCustomer(Customer model, int id)
        {
            var result = await _customerService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CustomerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _customerService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CustomerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _customerService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CustomerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _customerService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CustomerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _customerService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CustomerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
