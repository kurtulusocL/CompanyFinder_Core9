using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeProductSubcategory:ViewComponent
    {
        readonly IProductSubcategoryService _productSubcategoryService;
        public AdminHomeProductSubcategory(IProductSubcategoryService productSubcategoryService)
        {
            _productSubcategoryService = productSubcategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productSubcategoryService.GetAllProductSubcategoriesForAdminHome());
        }
    }
}
