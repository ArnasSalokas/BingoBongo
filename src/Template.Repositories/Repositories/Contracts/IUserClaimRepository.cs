using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Entities.Database.Models;
using Template.Repositories.Base.Contracts;

namespace Template.Repositories.Repositories.Contracts
{
    public interface IUserClaimRepository : IBaseRepository<UserClaim>
    {
        Task<UserClaim> Add(UserClaim entity, int? addedBy);
        Task Update(UserClaim entity, int? modifiedBy);

        /// <summary>
        /// Returns the given user's claims.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserClaim>> Get(int userId, bool onlyActive = true);
    }
}
