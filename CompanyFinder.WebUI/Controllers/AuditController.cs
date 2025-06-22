using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.Controllers
{
    [ExceptionHandler]
    [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
    public class AuditController : Controller
    {
        readonly IAuditService _auditService;
        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _auditService.GetAllAsync());
        }
        public async Task<IActionResult> VisitorAudit()
        {
            return View(await _auditService.GetAllVisitorAuditAsync());
        }
        public async Task<IActionResult> UserAudit()
        {
            return View(await _auditService.GetAllUserAuditAsync());
        }
        public async Task<IActionResult> AuditOps()
        {
            return View(await _auditService.GetAllForAdmin());
        }
        public async Task<IActionResult> AuditDetail(int? id)
        {
            return View(await _auditService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _auditService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteAudit(Audit model, int id)
        {
            var result = await _auditService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(AuditOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _auditService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AuditOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _auditService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AuditOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _auditService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AuditOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _auditService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AuditOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
