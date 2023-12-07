using Microsoft.EntityFrameworkCore;
using RPSLS.Api.Data;
using RPSLS.Api.Data.DTO;
using System.Reflection;

namespace RPSLS.Api.Repositories
{
    public class BaseRepository<TModel>(RPSLSDbContext context) where TModel : BaseDTO
    {
        public RPSLSDbContext DbContext { get; set; } = context;

        public virtual TModel GetById(Guid id)
        {
            var item = Query().FirstOrDefault(e => e.Id == id);

            return item ?? throw new KeyNotFoundException();
        }

        public virtual void Insert(TModel model)
        {
            DbContext.Set<TModel>().Add(model);
        }

        public virtual TModel Update(TModel dto)
        {
            TModel item = GetById(dto.Id);

            if(item == null)
                throw new KeyNotFoundException();
            else
                UpdateValues(dto, item);

            return item;
        }

        private TModel UpdateValues(TModel modelBase, TModel modelDestiny)
        {
            var properties = modelBase.GetType().GetProperties().Where(p => p.Name != "id").ToList();

            properties.ForEach(property => {
                var value = property.GetValue(modelBase);
                PropertyInfo? destinyProperty = modelDestiny.GetType().GetProperty(property.Name);
                destinyProperty?.SetValue(modelDestiny, value);
            });

            return modelDestiny;
        }

        public virtual IQueryable<TModel> Query()
        {
            return DbContext.Set<TModel>();
        }
    }
}
