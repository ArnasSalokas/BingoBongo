using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template.Repositories.Base.Contracts
{
    public interface IBaseRepository<T> where T : class, new()
    {
        IDataStore Store();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task Delete(IEnumerable<T> entities);
    }
}
