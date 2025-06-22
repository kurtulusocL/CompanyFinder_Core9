using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeFooterSocialMedia:ViewComponent
    {
        readonly ISocialMediaService _socialMediaService;
        public HomeFooterSocialMedia(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_socialMediaService.GetAllSocialMediasForHomeFooter());
        }
    }
}
