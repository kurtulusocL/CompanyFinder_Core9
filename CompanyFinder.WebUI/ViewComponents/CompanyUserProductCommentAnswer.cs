using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserProductCommentAnswer:ViewComponent
    {
        readonly ICommentAnswerService _commentAnswerService;
        public CompanyUserProductCommentAnswer(ICommentAnswerService commentAnswerService)
        {
            _commentAnswerService = commentAnswerService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentAnswerService.GetAllIncludingCommentAnswerForCompanyByCommentId(id));
        }
    }
}
