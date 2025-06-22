using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AboutOurServices:ViewComponent
    {
        readonly IOurServicesService _ourServicesService;
        public AboutOurServices(IOurServicesService ourServicesService)
        {
            _ourServicesService = ourServicesService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_ourServicesService.GetAllOurServicesRandomForAbout());
        }
    }
}
