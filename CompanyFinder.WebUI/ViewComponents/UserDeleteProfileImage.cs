using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class UserDeleteProfileImage:ViewComponent
    {
        readonly IProfileImageService _profileImageService;
        public UserDeleteProfileImage(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_profileImageService.GetProfileImageByUserId(id));
        }
    }
}
