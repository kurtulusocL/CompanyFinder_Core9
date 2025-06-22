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
    public class AppointmentController : Controller
    {
        readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _appointmentService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ApprovedAppointment()
        {
            return View(await _appointmentService.GetAllIncludingByApprovedAsync());
        }
        public async Task<IActionResult> NotApprovedAppointment()
        {
            return View(await _appointmentService.GetAllIncludingByNotApprovedAsync());
        }
        public async Task<IActionResult> AppointmentByCompany(int? id)
        {
            return View(await _appointmentService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> AppointmentByUser(string id)
        {
            return View(await _appointmentService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> AppointmentByCompanyForAdmin(int? id)
        {
            return View(await _appointmentService.GetAllIncludingByCompanyIdForAdminAsync(id));
        }
        public async Task<IActionResult> AppointmentByUserForAdmin(string id)
        {
            return View(await _appointmentService.GetAllIncludingByUserIdForAdminAsync(id));
        }
        public async Task<IActionResult> AppointmentOps()
        {
            return View(await _appointmentService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> AppointmentDetail(int? id)
        {
            return View(await _appointmentService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _appointmentService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteAppointment(Appointment model, int id)
        {
            var result = await _appointmentService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(AppointmentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _appointmentService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AppointmentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _appointmentService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AppointmentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _appointmentService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AppointmentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _appointmentService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AppointmentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}