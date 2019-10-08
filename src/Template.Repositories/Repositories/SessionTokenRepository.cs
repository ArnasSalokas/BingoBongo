using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Entities.Database.Models;
using Template.Entities.Enums;
using Template.Repositories.Base;
using Template.Repositories.Repositories.Contracts;

namespace Template.Repositories.Repositories
{
    public class SessionTokenRepository : BaseRepository<SessionToken>, ISessionTokenRepository
    {
        public SessionTokenRepository(IServiceProvider services) : base(services) { }

        public async Task<IEnumerable<SessionToken>> GetMany(int userId, SessionTokenType type)
        {
            return await Store()
                .Filtered(nameof(SessionToken.UserId), userId)
                .Filtered(nameof(SessionToken.TokenType), type)
                .Get<SessionToken>();
        }

        public async Task<SessionToken> GetClientToken(int clientId) =>
            await Store()
            .Filtered(nameof(SessionToken.ClientId), clientId)
            .Filtered(nameof(SessionToken.TokenType), SessionTokenType.App)
            .FirstOrNull<SessionToken>();

        public async Task<IEnumerable<SessionToken>> GetClientTokens(int clientId) =>
            await Store()
            .Filtered(nameof(SessionToken.ClientId), clientId)
            .Filtered(nameof(SessionToken.TokenType), SessionTokenType.App)
            .Get<SessionToken>();

        public async Task<SessionToken> Get(int userId, string token, SessionTokenType type)
        {
            var store = Store()
                .Filtered(nameof(SessionToken.TokenType), type)
                .Filtered(nameof(SessionToken.Token), token);

            if (type == SessionTokenType.Admin)
                store = store.Filtered(nameof(SessionToken.UserId), userId);

            else if (type == SessionTokenType.App)
                store = store.Filtered(nameof(SessionToken.ClientId), userId);

            return await store.FirstOrNull<SessionToken>();
        }

        public async Task<SessionToken> GetLast(int userId)
        {
            return await Store()
                .Filtered(nameof(SessionToken.UserId), userId)
                .Sorted(nameof(SessionToken.Id), "DESC")
                .FirstOrNull<SessionToken>();
        }
    }
}
