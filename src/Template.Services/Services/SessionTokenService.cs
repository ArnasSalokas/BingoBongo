using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Template.Entities.Database.Models;
using Template.Entities.Enums;
using Template.Repositories.Repositories.Contracts;
using Template.Services.Base;
using Template.Services.Services.Contracts;
using Wiry.Base32;

namespace Template.Services.Services
{
    public class SessionTokenService : BaseService, ISessionTokenService
    {
        private readonly ISessionTokenRepository _sessionTokenRepository;

        public SessionTokenService(IServiceProvider services) : base(services)
        {
            _sessionTokenRepository = services.GetSessionTokenRepository();
        }

        public bool IsValidFormat(string tokenString) => tokenString.Count(v => v == '.') == 3;

        public async Task LogoutUser(int userId, SessionTokenType type)
        {
            var dbTokens = await _sessionTokenRepository.GetMany(userId, type);

            foreach (var token in dbTokens)
                await _sessionTokenRepository.Delete(token);
        }

        public async Task LogoutClient(int clientId)
        {
            var tokens = await _sessionTokenRepository.GetClientTokens(clientId);

            foreach (var token in tokens)
                await _sessionTokenRepository.Delete(token);
        }

        public async Task<SessionToken> GenerateAppToken(int clientId, string phoneNumber)
        {
            var token = SessionToken.FormAppToken(clientId, phoneNumber, _config.SessionToken.LifetimeMinutes);
            token.Token = GenerateTokenString(SessionTokenType.App, clientId);

            var tr = _services.GetTransactionStore();
            tr.BeginTransaction();

            await LogoutClient(clientId);
            var newToken = await _sessionTokenRepository.Add(token);

            tr.CommitTransaction();

            return newToken;
        }

        public async Task<SessionToken> GenerateAdminToken(int userId)
        {
            var token = SessionToken.FormAdminToken(userId, _config.SessionToken.LifetimeMinutes);
            token.Token = GenerateTokenString(SessionTokenType.Admin, userId);

            return await _sessionTokenRepository.Add(token);
        }

        private string GenerateTokenString(SessionTokenType type, int userId, string phoneNumber = "")
        {
            byte[] randomBytes = _services.GetRngService().Bytes(_config.SessionToken.ByteCount);

            var tokenString = Base32Encoding.Standard.GetString(randomBytes);

            var typeEncoded = Base32Encoding.Standard.GetString(BitConverter.GetBytes((int)type));

            var userIdEncoded = Base32Encoding.Standard.GetString(BitConverter.GetBytes(userId));

            var phoneNumberEncoded = Base32Encoding.Standard.GetString(Encoding.Unicode.GetBytes(phoneNumber));

            return $"{tokenString}.{typeEncoded}.{userIdEncoded}.{phoneNumberEncoded}";
        }

        public int ParseUserId(string tokenString)
            => BitConverter.ToInt32(Base32Encoding.Standard.ToBytes(tokenString.Split('.')[2]), 0);

        public SessionTokenType ParseUserType(string tokenString)
            => (SessionTokenType)BitConverter.ToInt32(Base32Encoding.Standard.ToBytes(tokenString.Split('.')[1]), 0);

        public string ParsePhone(string tokenString)
            => BitConverter.ToString(Encoding.Unicode.GetBytes(tokenString.Split('.')[3]), 0);
    }
}
