using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeCompanyCategory : ViewComponent
    {
        readonly ICompanyCategoryService _companyCategoryService;
        public AdminHomeCompanyCategory(ICompanyCategoryService companyCategoryService)
        {
            _companyCategoryService = companyCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companyCategoryService.GetAllCompanyCategoriesForAdminHome());
        }
    }
}
