﻿using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Abstract
{
    public interface IUserAgreementService
    {
        Task<IEnumerable<UserAgreement>> GetAllAsync();
        Task<IEnumerable<UserAgreement>> GetAllForAdmin();
        Task<UserAgreement> GetByIdAsync(int? id);
        Task<bool> CreateAsync(UserAgreement entity);
        Task<bool> UpdateAsync(UserAgreement entity);
        Task<bool> DeleteAsync(UserAgreement entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
