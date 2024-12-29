using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Bases
{
    public interface IService<TEntity, TModel> where TEntity : class, new() where TModel : class, new()
    {
        public IQueryable<TModel> Query();
        public ServiceBase Create(TEntity Record);
        public ServiceBase Update(TEntity Record);
        public ServiceBase Delete(int Id);

    }
}
