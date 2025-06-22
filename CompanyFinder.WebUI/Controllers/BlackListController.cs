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
    public class BlackListController : Controller
    {
        readonly IBlackListService _blackListService;
        readonly IAuditService _auditService;
        public BlackListController(IBlackListService blackListService, IAuditService auditService)
        {
            _blackListService = blackListService;
            _auditService = auditService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _blackListService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ByAudit(int? id)
        {
            return View(await _blackListService.GetAllIncludingByAuditIdAsync(id));
        }
        public async Task<IActionResult> ByAuditForAdmin(int? id)
        {
            return View(await _blackListService.GetAllIncludingForAdminByAuditIdAsync(id));
        }
        public async Task<IActionResult> BlackListOps()
        {
            return View(await _blackListService.GetAllIncludingForAdminAsync());
        }
        public async Task<IActionResult> BlackListDetail(int? id)
        {
            return View(await _blackListService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Create(int? id)
        {
            ViewBag.AuditId = await _auditService.GetByIdAsync(id);
            BlackList model = new BlackList
            {
                AuditId = id
            };
            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string macAddress, string ipAddress, string localIpAddress, string remoteIpAddress, string ipAddressVPN, DateTime expirationDate, int? auditId)
        {
            var result = await _blackListService.CreateAsync(macAddress, ipAddress, localIpAddress, remoteIpAddress, ipAddressVPN, expirationDate, auditId);
            if (result)
            {
                TempData["successBlackListCreated"] = "Add to BlackList Successfully";
                return RedirectToAction(nameof(Create), new { id = auditId });
            }
            TempData["errorBlackListCreated"] = "There was an error";
            return RedirectToAction(nameof(Create), new { id = auditId });
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _blackListService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string macAddress, string ipAddress, string localIpAddress, string remoteIpAddress, string ipAddressVPN, DateTime expirationDate, int? auditId, int id)
        {
            var result = await _blackListService.UpdateAsync(macAddress, ipAddress, localIpAddress, remoteIpAddress, ipAddressVPN, expirationDate, auditId, id);
            if (result)
            {
                TempData["successBlackListUpdated"] = "Update BlackList Successfully";
                return RedirectToAction(nameof(Edit), new { id = id });
            }
            TempData["errorBlackListUpdated"] = "There was an error";
            return RedirectToAction(nameof(Edit), new { id = id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _blackListService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteBlackList(BlackList model, int id)
        {
            var result = await _blackListService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(BlackListOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _blackListService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlackListOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _blackListService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlackListOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _blackListService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlackListOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _blackListService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlackListOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
