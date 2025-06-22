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
    public class FeedbackController : Controller
    {
        readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _feedbackService.GetAllAsync());
        }
        public async Task<IActionResult> ByUser(string id)
        {
            return View(await _feedbackService.GetAllByUserIdAsync(id));
        }
        public async Task<IActionResult> FeedbackOps()
        {
            return View(await _feedbackService.GetAllForAdminAsync());
        }
        public async Task<IActionResult> FeedbackDetail(int? id)
        {
            return View(await _feedbackService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _feedbackService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteFeedback(Feedback model, int id)
        {
            var result = await _feedbackService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(FeedbackOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _feedbackService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FeedbackOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _feedbackService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FeedbackOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _feedbackService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FeedbackOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _feedbackService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FeedbackOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
