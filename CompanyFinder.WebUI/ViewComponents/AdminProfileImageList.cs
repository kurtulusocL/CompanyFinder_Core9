using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminProfileImageList:ViewComponent
    {
        readonly IProfileImageService _profileImageService;
        public AdminProfileImageList(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }
        public IViewComponentResult Invoke(string id)
        {
            return View(_profileImageService.GetAllIncludinggProfileImageForAdminHomeByUserId(id));
        }
    }
}
