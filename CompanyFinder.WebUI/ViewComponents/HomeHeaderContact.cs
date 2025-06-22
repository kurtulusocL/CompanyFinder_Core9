using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeHeaderContact:ViewComponent
    {
        readonly IContactService _contactService;
        public HomeHeaderContact(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_contactService.GetAllContactForHomeHeader());
        }
    }
}
