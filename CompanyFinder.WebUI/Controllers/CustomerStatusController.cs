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
    public class CustomerStatusController : Controller
    {
        readonly ICustomerStatusService _customerStatusService;
        public CustomerStatusController(ICustomerStatusService customerStatusService)
        {
            _customerStatusService = customerStatusService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _customerStatusService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> CustomerStatusOps()
        {
            return View(await _customerStatusService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> CustomerStatusDetail(int? id)
        {
            return View(await _customerStatusService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerStatus model)
        {
            var result = await _customerStatusService.CreateAsync(model);
            if (result)
            {
                TempData["customerStatusCreatedSuccessfull"] = "Customer Status Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["customerStatusCreatedError"] = "Customer Status Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _customerStatusService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerStatus model)
        {
            var result = await _customerStatusService.UpdateAsync(model);
            if (result)
            {
                TempData["customerStatusUpdatedSuccessfull"] = "Customer Status Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["customerStatusUpdateError"] = "Customer Status Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _customerStatusService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCustomerStatus(CustomerStatus model, int id)
        {
            var result = await _customerStatusService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CustomerStatusOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _customerStatusService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CustomerStatusOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _customerStatusService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CustomerStatusOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _customerStatusService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CustomerStatusOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _customerStatusService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CustomerStatusOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
