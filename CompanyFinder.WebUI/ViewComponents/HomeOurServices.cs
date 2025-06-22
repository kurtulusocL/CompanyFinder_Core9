using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeOurServices:ViewComponent
    {
        readonly IOurServicesService _ourServicesService;
        public HomeOurServices(IOurServicesService ourServicesService)
        {
            _ourServicesService = ourServicesService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_ourServicesService.GetAllOurServicesRandomForHome());
        }
    }
}
