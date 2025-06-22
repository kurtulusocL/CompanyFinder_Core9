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
    public class CancelMembershipReasonController : Controller
    {
        readonly ICancelMembershipReasonService _reasonService;
        public CancelMembershipReasonController(ICancelMembershipReasonService reasonService)
        {
            _reasonService = reasonService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _reasonService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ReasonOps()
        {
            return View(await _reasonService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> ReasonDetail(int? id)
        {
            return View(await _reasonService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CancelMembershipReason model)
        {
            var result = await _reasonService.CreateAsync(model);
            if (result)
            {
                TempData["reasonCreatedSuccessfull"] = "Cancel Membership Reason Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["reasonCreatedError"] = "Cancel Membership Reason Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _reasonService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CancelMembershipReason model)
        {
            var result = await _reasonService.UpdateAsync(model);
            if (result)
            {
                TempData["reasonUpdatedSuccessfull"] = "Cancel Membership Reason Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["reasonUpdateError"] = "Cancel Membership Reason Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _reasonService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteReason(CancelMembershipReason model, int id)
        {
            var result = await _reasonService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(ReasonOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _reasonService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReasonOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _reasonService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReasonOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _reasonService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReasonOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _reasonService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ReasonOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
