using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeAllOurServices:ViewComponent
    {
        readonly IOurServicesService _ourServicesService;
        public HomeAllOurServices(IOurServicesService ourServicesService)
        {
            _ourServicesService = ourServicesService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_ourServicesService.GetAllOurServicesForHome());
        }
    }
}
