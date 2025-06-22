using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyDetaillmage : ViewComponent
    {
        readonly IPictureService _pictureService;
        public ExplorerCompanyDetaillmage(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_pictureService.GetAllIncludingExplorerCompanyDetailImageByCompanyId(id));
        }
    }
}
