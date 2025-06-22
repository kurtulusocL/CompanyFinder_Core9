using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeFooterLocation : ViewComponent
    {
        readonly IContactService _contactService;
        public HomeFooterLocation(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_contactService.GetAllContactForHomeHeader());
        }
    }
}
