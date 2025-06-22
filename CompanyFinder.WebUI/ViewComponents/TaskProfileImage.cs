using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class TaskProfileImage:ViewComponent
    {
        readonly IProfileImageService _profileImageService;
        public TaskProfileImage(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_profileImageService.GetProfileImageByUserId(id));
        }
    }
}
