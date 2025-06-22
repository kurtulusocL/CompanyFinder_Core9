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
    public class AppointmentAnswerController : Controller
    {
        readonly IAppointmentAnswerService _appointmentAnswerService;
        public AppointmentAnswerController(IAppointmentAnswerService appointmentAnswerService)
        {
            _appointmentAnswerService = appointmentAnswerService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _appointmentAnswerService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ApprovedAnswer()
        {
            return View(await _appointmentAnswerService.GetAllIncludingByApprovedAsync());
        }
        public async Task<IActionResult> NotApprovedAnswer()
        {
            return View(await _appointmentAnswerService.GetAllIncludingByNotApprovedAsync());
        }
        public async Task<IActionResult> AnswerByAppointment(int? id)
        {
            return View(await _appointmentAnswerService.GetAllIncludingByAppointmentIdAsync(id));
        }
        public async Task<IActionResult> AnswerByAppointmentForAdmin(int? id)
        {
            return View(await _appointmentAnswerService.GetAllIncludingByAppointmentIdForAdminAsync(id));
        }
        public async Task<IActionResult> AnswerOps()
        {
            return View(await _appointmentAnswerService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> AnswerDetail(int? id)
        {
            return View(await _appointmentAnswerService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _appointmentAnswerService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteAnswer(AppointmentAnswer model, int id)
        {
            var result = await _appointmentAnswerService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(AnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _appointmentAnswerService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _appointmentAnswerService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _appointmentAnswerService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _appointmentAnswerService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AnswerOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
