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
    public class QuestionController : Controller
    {
        readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }       
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _questionService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> CompanyQuestion(int? id)
        {
            return View(await _questionService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> ProductQuestion(int? id)
        {
            return View(await _questionService.GetAllIncludingByProductIdAsync(id));
        }
        public async Task<IActionResult> UserQuestion(string id)
        {
            return View(await _questionService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> UserQuestionForAdmin(string id)
        {
            return View(await _questionService.GetAllIncludingByUserIdForAdminAsync(id));
        }
        public async Task<IActionResult> QuestionOps()
        {
            return View(await _questionService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> QuestionDetail(int? id)
        {
            return View(await _questionService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _questionService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteQuestion(Question model, int id)
        {
            var result = await _questionService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(QuestionOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _questionService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(QuestionOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _questionService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(QuestionOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _questionService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(QuestionOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _questionService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(QuestionOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
