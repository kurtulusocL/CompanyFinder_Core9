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
    public class ExceptionLoggerController : Controller
    {
        readonly IExceptionLoggerService _exceptionLoggerService;
        public ExceptionLoggerController(IExceptionLoggerService exceptionLoggerService)
        {
            _exceptionLoggerService = exceptionLoggerService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _exceptionLoggerService.GetAllAsync());
        }
        public async Task<IActionResult> ExceptionLoggerOps()
        {
            return View(await _exceptionLoggerService.GetAllForAdmin());
        }
        public async Task<IActionResult> ExceptionLoggerDetail(int? id)
        {
            return View(await _exceptionLoggerService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _exceptionLoggerService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteExceptionLogger(ExceptionLogger model, int id)
        {
            var result = await _exceptionLoggerService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(ExceptionLoggerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _exceptionLoggerService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ExceptionLoggerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _exceptionLoggerService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ExceptionLoggerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _exceptionLoggerService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ExceptionLoggerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _exceptionLoggerService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ExceptionLoggerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
