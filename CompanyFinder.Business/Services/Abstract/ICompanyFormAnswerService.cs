using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface ICompanyFormAnswerService
    {
        Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingAsync();
        Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingUserIdAsync(string userId);
        Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingByCompanyFormIdAsync(int? companyFormId);
        Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingForAdminAsync();
        Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingForAdminByUserIdAsync(string userId);
        Task<CompanyFormAnswer> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string? title, string desc, int? companyFormId, string userId);
        Task<bool> DeleteAsync(CompanyFormAnswer entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingCompanyUserFormAnswerByUserIdAsync(string userId);
        Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingCompanyUserYourFormAnswerByCompanyFormIdAsync(int? companyFormId);
        IEnumerable<CompanyFormAnswer> GetAllIncludingCompanyUserYourFormAnswerByCompanyFormId(int? companyFormId);
        IEnumerable<CompanyFormAnswer> GetAllIncludingCompanyFormAnswersForExplorer(int? companyFormId);
    }
}
