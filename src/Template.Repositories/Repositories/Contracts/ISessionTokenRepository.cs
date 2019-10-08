using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Entities.Database.Models;
using Template.Entities.Enums;
using Template.Repositories.Base.Contracts;

namespace Template.Repositories.Repositories.Contracts
{
    public interface ISessionTokenRepository : IBaseRepository<SessionToken>
    {
        /// <summary>
        /// Returns the last session token of a given user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<SessionToken> GetLast(int userId);

        /// <summary>
        /// Returns a session token, checked against user ownership.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<SessionToken> Get(int userId, string token, SessionTokenType type);

        /// <summary>
        /// Gets client token
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<SessionToken> GetClientToken(int clientId);

        /// <summary>
        /// Gets client tokens
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<IEnumerable<SessionToken>> GetClientTokens(int clientId);

        /// <summary>
        /// Returns all session tokens of a user according to the provided type.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<IEnumerable<SessionToken>> GetMany(int userId, SessionTokenType type);
    }
}
