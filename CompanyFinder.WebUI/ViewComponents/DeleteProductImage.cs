using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class DeleteProductImage : ViewComponent
    {
        readonly IPictureService _pictureService;
        public DeleteProductImage(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_pictureService.GetAllIncludingByProductId(id));
        }
    }
}
