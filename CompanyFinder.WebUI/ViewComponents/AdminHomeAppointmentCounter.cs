using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeAppointmentCounter:ViewComponent
    {
        readonly IAppointmentService _appointmentService;
        public AdminHomeAppointmentCounter(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var appointmentCount = _appointmentService.AppointmentCounter();
                ViewData["appointmentCount"] = appointmentCount >= 0 ? appointmentCount : 0;
            }
            catch (Exception)
            {
                ViewData["appointmentCount"] = 0;
            }
            return View();
        }
    }
}
