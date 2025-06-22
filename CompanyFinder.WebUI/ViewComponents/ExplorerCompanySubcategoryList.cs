using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanySubcategoryList : ViewComponent
    {
        readonly ICompanySubcategoryService _companySubcategoryService;
        public ExplorerCompanySubcategoryList(ICompanySubcategoryService companySubcategoryService)
        {
            _companySubcategoryService = companySubcategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companySubcategoryService.GetAllIncludingCompanySubcategoriesForCompany());
        }
    }
}
