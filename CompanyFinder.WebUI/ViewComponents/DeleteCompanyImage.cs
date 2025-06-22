using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class DeleteCompanyImage : ViewComponent
    {
        readonly IPictureService _pictureService;
        public DeleteCompanyImage(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_pictureService.GetAllIncludingByCompanyId(id));
        }
    }
}
