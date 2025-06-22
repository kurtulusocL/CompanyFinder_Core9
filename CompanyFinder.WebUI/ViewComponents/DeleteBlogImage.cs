using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class DeleteBlogImage : ViewComponent
    {
        readonly IPictureService _pictureService;
        public DeleteBlogImage(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_pictureService.GetAllIncludingByBlogId(id));
        }
    }
}
