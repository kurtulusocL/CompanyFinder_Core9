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
    public class UserAgreementController : Controller
    {
        readonly IUserAgreementService _userAgreementService;
        public UserAgreementController(IUserAgreementService userAgreementService)
        {
            _userAgreementService = userAgreementService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _userAgreementService.GetAllAsync());
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _userAgreementService.GetAllAsync());
        }
        public async Task<IActionResult> UserAgreementOps()
        {
            return View(await _userAgreementService.GetAllForAdmin());
        }
        public async Task<IActionResult> UserAgreementDetail(int? id)
        {
            return View(await _userAgreementService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserAgreement model)
        {
            var result = await _userAgreementService.CreateAsync(model);
            if (result)
            {
                TempData["userAgreementCreatedSuccessfull"] = "User Agreement Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["userAgreementCreateError"] = "User Agreement Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _userAgreementService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserAgreement model)
        {
            var result = await _userAgreementService.UpdateAsync(model);
            if (result)
            {
                TempData["userAgreementUpdatedSuccessfull"] = "User Agreement Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["userAgreementUpdateError"] = "User Agreement Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _userAgreementService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteUserAgreement(UserAgreement model, int id)
        {
            var result = await _userAgreementService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(UserAgreementOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _userAgreementService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserAgreementOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _userAgreementService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserAgreementOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _userAgreementService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserAgreementOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _userAgreementService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserAgreementOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
