using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomePictureCounter : ViewComponent
    {
        readonly IPictureService _pictureService;
        public AdminHomePictureCounter(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var pictureCount = _pictureService.PictureCounter();
                ViewData["pictureCount"] = pictureCount >= 0 ? pictureCount : 0;
            }
            catch (Exception)
            {
                ViewData["pictureCount"] = 0;
            }
            return View();
        }
    }
}
