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
    public class CancelMembershipController : Controller
    {
        readonly ICancelMembershipService _cancelMembershipService;
        public CancelMembershipController(ICancelMembershipService cancelMembershipService)
        {
            _cancelMembershipService = cancelMembershipService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _cancelMembershipService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> Cancelled()
        {
            return View(await _cancelMembershipService.GetAllIncludingCancelledAsync());
        }
        public async Task<IActionResult> NotCancelled()
        {
            return View(await _cancelMembershipService.GetAllIncludingNotCancelledAsync());
        }
        public async Task<IActionResult> ByReason(int id)
        {
            return View(await _cancelMembershipService.GetAllIncludingByReasonIdAsync(id));
        }
        public async Task<IActionResult> ByUser(string id)
        {
            return View(await _cancelMembershipService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> CancelledForAdmin()
        {
            return View(await _cancelMembershipService.GetAllIncludingCancelledForAdminAsync());
        }
        public async Task<IActionResult> ByReasonForAdmin(int id)
        {
            return View(await _cancelMembershipService.GetAllIncludingForAdminByReasonIdAsync(id));
        }
        public async Task<IActionResult> ByUserForAdmin(string id)
        {
            return View(await _cancelMembershipService.GetAllIncludingForAdminByUserIdAsync(id));
        }
        public async Task<IActionResult> CancelMembershipOps()
        {
            return View(await _cancelMembershipService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> CancelMembershipDetail(int? id)
        {
            return View(await _cancelMembershipService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _cancelMembershipService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCancelMembership(CancelMembership model, int id)
        {
            var result = await _cancelMembershipService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CancelMembershipOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }       
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _cancelMembershipService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CancelMembershipOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _cancelMembershipService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CancelMembershipOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _cancelMembershipService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CancelMembershipOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _cancelMembershipService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CancelMembershipOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
