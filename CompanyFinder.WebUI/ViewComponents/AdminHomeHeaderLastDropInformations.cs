using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeHeaderLastDropInformations:ViewComponent
    {
        readonly IDropInformationService _dropInformationService;
        public AdminHomeHeaderLastDropInformations(IDropInformationService dropInformationService)
        {
            _dropInformationService = dropInformationService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_dropInformationService.GetAllDropInformationsForAdminHeader());
        }
    }
}
