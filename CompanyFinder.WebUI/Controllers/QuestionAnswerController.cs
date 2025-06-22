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
    public class QuestionAnswerController : Controller
    {
        readonly IQuestionAnswerService _questionAnswerService;
        public QuestionAnswerController(IQuestionAnswerService questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _questionAnswerService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ByQuestion(int? id)
        {
            return View(await _questionAnswerService.GetAllIncludingByQuestionIdAsync(id));
        }
        public async Task<IActionResult> UserAnswer(string id)
        {
            return View(await _questionAnswerService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> ByQuestionForAdmin(int? id)
        {
            return View(await _questionAnswerService.GetAllIncludingForAdminByQuestionIdAsync(id));
        }
        public async Task<IActionResult> UserAnswerForAdmin(string id)
        {
            return View(await _questionAnswerService.GetAllIncludingForAdminByUserIdAsync(id));
        }
        public async Task<IActionResult> QuestionAnswerOps()
        {
            return View(await _questionAnswerService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> QuestionAnswerDetail(int? id)
        {
            return View(await _questionAnswerService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _questionAnswerService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteQuestionAnswer(QuestionAnswer model, int id)
        {
            var result = await _questionAnswerService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(QuestionAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _questionAnswerService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(QuestionAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _questionAnswerService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(QuestionAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _questionAnswerService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(QuestionAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _questionAnswerService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(QuestionAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
