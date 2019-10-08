using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Template.Entities.Database.Base;
using Template.Repositories.Base.Contracts;

namespace Template.Repositories.Base
{
    public abstract class BaseRepository<T> where T : class, IEntity, new()
    {
        protected readonly IServiceProvider _services;

        public BaseRepository(IServiceProvider services) => _services = services;

        public virtual IDataStore Store() => _services.GetService<IDataStore>().As<T>();

        public async virtual Task<T> Add(T entity) => await Store().Add(entity);

        public async virtual Task Update(T entity) => await Store().Update(entity);

        public virtual async Task Delete(T entity)
        {
            if (entity == null)
                return;

            await Store().Delete<T>(entity.Id);
        }

        public virtual async Task Delete(IEnumerable<T> entities) => await Store().Delete<T>(entities.Select(e => (object)e.Id));
    }
}
