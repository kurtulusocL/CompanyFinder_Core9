using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyDetailAppointment : ViewComponent
    {
        readonly ICompanyService _companyService;
        public ExplorerCompanyDetailAppointment(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            ViewBag.CompanyId = _companyService.GetCompanyById(id);
            Appointment model = new Appointment
            {
                CompanyId = id
            };
            return View(model);
        }
    }
}
