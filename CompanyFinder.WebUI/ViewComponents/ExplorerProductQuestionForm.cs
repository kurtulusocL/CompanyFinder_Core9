using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerProductQuestionForm : ViewComponent
    {
        readonly IProductService _productService;
        public ExplorerProductQuestionForm(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            ViewBag.ProductId = _productService.GetIncludingProductById(id);
            Question model = new Question
            {
                ProductId = id
            };
            return View(model);
        }
    }
}
