using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class UserTableProfileImage : ViewComponent
    {
        readonly IProfileImageService _profileImageService;
        public UserTableProfileImage(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_profileImageService.GetProfileImageByUserId(id));
        }
    }
}
