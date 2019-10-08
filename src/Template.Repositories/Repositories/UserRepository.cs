using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Template.Entities.Database.Models;
using Template.Repositories.Base;
using Template.Repositories.Base.Contracts;
using Template.Repositories.Repositories.Contracts;

namespace Template.Repositories.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IServiceProvider services) : base(services) { }

        public async Task<User> Add(User entity, int? addedBy)
        {
            if (addedBy.HasValue)
                entity.AddedBy = addedBy;

            entity.AddedDate = DateTime.UtcNow;

            return await Store().Add(entity);
        }

        public async Task Update(User entity, int? modifiedBy)
        {
            if (modifiedBy.HasValue)
                entity.ModifiedBy = modifiedBy;

            entity.ModifiedDate = DateTime.UtcNow;

            await Store().Update(entity);
        }
    }
}
