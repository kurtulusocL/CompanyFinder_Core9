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
    public class CompanyFormAnswerController : Controller
    {
        readonly ICompanyFormAnswerService _companyFormAnswerService;
        public CompanyFormAnswerController(ICompanyFormAnswerService companyFormAnswerService)
        {
            _companyFormAnswerService = companyFormAnswerService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _companyFormAnswerService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ByCompanyForm(int? id)
        {
            return View(await _companyFormAnswerService.GetAllIncludingByCompanyFormIdAsync(id));
        }
        public async Task<IActionResult> ByUser(string id)
        {
            return View(await _companyFormAnswerService.GetAllIncludingUserIdAsync(id));
        }
        public async Task<IActionResult> ByUserForAdmin(string id)
        {
            return View(await _companyFormAnswerService.GetAllIncludingForAdminByUserIdAsync(id));
        }
        public async Task<IActionResult> CompanyFormAnswerOps()
        {
            return View(await _companyFormAnswerService.GetAllIncludingForAdminAsync());
        }
        public async Task<IActionResult> CompanyFormAnswerDetail(int? id)
        {
            return View(await _companyFormAnswerService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _companyFormAnswerService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCompanyFormAnswer(CompanyFormAnswer model, int id)
        {
            var result = await _companyFormAnswerService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyFormAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _companyFormAnswerService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyFormAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _companyFormAnswerService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyFormAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _companyFormAnswerService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyFormAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _companyFormAnswerService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyFormAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
