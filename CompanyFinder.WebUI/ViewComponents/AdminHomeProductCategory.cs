using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeProductCategory:ViewComponent
    {
        readonly IProductCategoryService _productSubcategoryService;
        public AdminHomeProductCategory(IProductCategoryService productCategoryService)
        {
            _productSubcategoryService = productCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productSubcategoryService.GetAllProductCategoryForAdminHome());
        }
    }
}
