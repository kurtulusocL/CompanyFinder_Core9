using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyFormSidebarCategories:ViewComponent
    {
        readonly IFormCategoryService _formCategoryService;
        public ExplorerCompanyFormSidebarCategories(IFormCategoryService formCategoryService)
        {
            _formCategoryService = formCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_formCategoryService.GetAllIncludingFormCategoriesForSidebar());
        }
    }
}
