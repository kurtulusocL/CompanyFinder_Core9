using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeCompanySubcategory:ViewComponent
    {
        readonly ICompanySubcategoryService _companySubcategoryService;
        public AdminHomeCompanySubcategory(ICompanySubcategoryService companySubcategoryService)
        {
            _companySubcategoryService = companySubcategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companySubcategoryService.GetAllCompanySubcategoryForAdminHome());
        }
    }
}
