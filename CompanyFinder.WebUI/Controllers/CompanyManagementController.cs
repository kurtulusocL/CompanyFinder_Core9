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
    public class CompanyManagementController : Controller
    {
        readonly ICompanyService _companyService;
        readonly ICompanyMessageService _companyMessageService;
        public CompanyManagementController(ICompanyService companyService, ICompanyMessageService companyMessageService)
        {
            _companyService = companyService;
            _companyMessageService = companyMessageService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _companyService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> CommentableCompanies()
        {
            return View(await _companyService.GetAllIncludingByCommentablesAsync());
        }
        public async Task<IActionResult> NotCommentableCompanies()
        {
            return View(await _companyService.GetAllIncludingByNotCommentablesAsync());
        }
        public async Task<IActionResult> CompanyByFoundationDate()
        {
            return View(await _companyService.GetAllIncludingByFoundationDateAsync());
        }
        public async Task<IActionResult> WeeklyMostPopularCompanies() 
        {
            return View(await _companyService.GetAllIncludingWeeklyPopularCompaniesFortAdminAsync());
        }
        public async Task<IActionResult> Country(int id)
        {
            return View(await _companyService.GetAllIncludingByCountryIdAsync(id));
        }
        public async Task<IActionResult> Category(int id)
        {
            return View(await _companyService.GetAllIncludingByCategoryIdAsync(id));
        }
        public async Task<IActionResult> Subcategory(int? id)
        {
            return View(await _companyService.GetAllIncludingBySubcategoryIdAsync(id));
        }
        public async Task<IActionResult> City(int? id)
        {
            return View(await _companyService.GetAllIncludingByCityIdAsync(id));
        }
        public async Task<IActionResult> CompanyUser(string id)
        {
            return View(await _companyService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> CompanyUserForAdmin(string id)
        {
            return View(await _companyService.GetAllIncludingByUserIdForAdminAsync(id));
        }
        public async Task<IActionResult> CompanyOps()
        {
            return View(await _companyService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> CompanyDetail(int? id)
        {
            return View(await _companyService.GetByIdAsync(id));
        }
        public async Task<IActionResult>SendMessage(int? id)
        {
            ViewBag.CompanyId=await _companyService.GetByIdAsync(id);
            CompanyMessage model = new CompanyMessage
            {
                CompanyId = id
            };
            return View("SendMessage", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(string? title, string subject, string desc, int? companyId, string userId)
        {
            var result = await _companyMessageService.CreateAsync(title, subject, desc, companyId, userId);
            if (result)
            {
                TempData["companyMessageSendSuccessfull"] = "Company Message Sent Successfull";
                return RedirectToAction(nameof(SendMessage), new { id = companyId });
            }
            TempData["companyMessageSendError"] = "Company Message Send Error";
            return RedirectToAction(nameof(SendMessage), new { id = companyId });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _companyService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCompany(Company model, int id)
        {
            var result = await _companyService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _companyService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _companyService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _companyService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _companyService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
