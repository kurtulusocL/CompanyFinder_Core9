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
    public class CommentAnswerController : Controller
    {
        readonly ICommentAnswerService _commentAnswerService;
        public CommentAnswerController(ICommentAnswerService commentAnswerService)
        {
            _commentAnswerService = commentAnswerService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _commentAnswerService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ByComment(int? id)
        {
            return View(await _commentAnswerService.GetAllIncludingByCommentIdAsync(id));
        }
        public async Task<IActionResult> ByUser(string id)
        {
            return View(await _commentAnswerService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> ByCommentForAdmin(int? id)
        {
            return View(await _commentAnswerService.GetAllIncludingForAdminByCommentIdAsync(id));
        }
        public async Task<IActionResult> ByUserForAdmin(string id)
        {
            return View(await _commentAnswerService.GetAllIncludingForAdminByUserIdAsync(id));
        }
        public async Task<IActionResult> CommentAnswerOps()
        {
            return View(await _commentAnswerService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> CommentAnswerDetail(int? id)
        {
            return View(await _commentAnswerService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _commentAnswerService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCommentAnswer(CommentAnswer model, int id)
        {
            var result = await _commentAnswerService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CommentAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _commentAnswerService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CommentAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _commentAnswerService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CommentAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _commentAnswerService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CommentAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _commentAnswerService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CommentAnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
