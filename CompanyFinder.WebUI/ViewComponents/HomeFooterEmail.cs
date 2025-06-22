using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeFooterEmail : ViewComponent
    {
        readonly IContactService _contactService;
        public HomeFooterEmail(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_contactService.GetAllContactForHomeHeader());
        }
    }
}
