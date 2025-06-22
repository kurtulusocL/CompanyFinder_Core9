using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class DropInformationManager : IDropInformationService
    {
        readonly IDropInformationDal _dropInformationDal;
        public DropInformationManager(IDropInformationDal dropInformationDal)
        {
            _dropInformationDal = dropInformationDal;
        }

        public bool Create(DropInformation entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = _dropInformationDal.Add(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(DropInformation entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _dropInformationDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _dropInformationDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<DropInformation>> GetAllAsync()
        {
            try
            {
                var result = await _dropInformationDal.GetAllAsync(i => i.IsActive == true && i.IsDeleted == false);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<DropInformation>();
            }
        }

        public IEnumerable<DropInformation> GetAllDropInformationsForAdminHeader()
        {
            try
            {
                return _dropInformationDal.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i=>i.CreatedDate).Take(15).ToList();
            }
            catch (Exception)
            {
                return new List<DropInformation>();
            }
        }

        public async Task<IEnumerable<DropInformation>> GetAllForAdmin()
        {
            try
            {
                var result = await _dropInformationDal.GetAllAsync();
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<DropInformation>();
            }
        }

        public async Task<DropInformation> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _dropInformationDal.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _dropInformationDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _dropInformationDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _dropInformationDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _dropInformationDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
