using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminLayoutNavbarUserProfileImage:ViewComponent
    {
        readonly IProfileImageService _profileImageService;
        public AdminLayoutNavbarUserProfileImage(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_profileImageService.GetProfileImageByUserId(id));
        }
    }
}
