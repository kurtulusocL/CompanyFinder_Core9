using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyCategoryList : ViewComponent
    {
        readonly ICompanyCategoryService _companyCategoryService;
        public ExplorerCompanyCategoryList(ICompanyCategoryService companyCategoryService)
        {
            _companyCategoryService = companyCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_companyCategoryService.GetAllIncludingCompanyCategoriesForCompany());
        }
    }
}
