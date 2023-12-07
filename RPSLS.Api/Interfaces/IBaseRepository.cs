using RPSLS.Api.Data.DTO;

namespace RPSLS.Api.Interfaces
{
    public interface IBaseRepository<TModel> where TModel : BaseDTO
    {
        TModel GetById(Guid id);

        void Insert(TModel dto);

        TModel Update(TModel dto);

        IQueryable<TModel> Query();
    }
}
