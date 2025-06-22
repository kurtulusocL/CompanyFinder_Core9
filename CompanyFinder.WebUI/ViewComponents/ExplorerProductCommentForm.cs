using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerProductCommentForm:ViewComponent
    {
        readonly IProductService _productService;
        public ExplorerProductCommentForm(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            ViewBag.ProductId = _productService.GetIncludingProductById(id);
            Comment model = new Comment
            {
                ProductId = id
            };
            return View(model);
        }
    }
}
