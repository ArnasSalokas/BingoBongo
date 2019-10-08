using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Entities.API.WebParams.Base;
using Template.Entities.Database.Models;
using Template.Repositories.Base;
using Template.Repositories.Repositories.Contracts;

namespace Template.Repositories.Repositories
{
    public class UserClaimRepository : BaseRepository<UserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(IServiceProvider services) : base(services) { }

        public async Task<UserClaim> Add(UserClaim entity, int? addedBy)
        {
            if (addedBy.HasValue)
                entity.AddedBy = addedBy;

            entity.AddedDate = DateTime.UtcNow;

            return await Store().Add(entity);
        }

        public async Task Update(UserClaim entity, int? modifiedBy)
        {
            if (modifiedBy.HasValue)
                entity.ModifiedBy = modifiedBy;

            entity.ModifiedDate = DateTime.UtcNow;

            await Store().Update(entity);
        }

        /// <summary>
        /// Gets all user claims of a single user. By default filters out only active claims.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserClaim>> Get(int userId, bool onlyActive = true)
        {
            var store = Store()
                .Filtered(nameof(UserClaim.UserId), userId);

            if (onlyActive)
                store.Filtered(nameof(UserClaim.Value), bool.TrueString, ComparisonOperator.Equals);

            return await store.Get<UserClaim>();
        }
    }
}
