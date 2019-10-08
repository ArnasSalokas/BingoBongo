using System.Threading.Tasks;
using Template.Entities.Database.Models;
using Template.Entities.Enums;

namespace Template.Services.Services.Contracts
{
    public interface ISessionTokenService
    {
        /// <summary>
        /// Parses the given token. Extracts and returns the user ID found within the parsed string.
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        int ParseUserId(string tokenString);

        /// <summary>
        /// Checks and returns a bool value whether the token provided is formed in a valid format.
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        bool IsValidFormat(string tokenString);

        /// <summary>
        /// Parses the given token. Extracts and returns session token's type found within the parsed string.
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        SessionTokenType ParseUserType(string tokenString);

        /// <summary>
        /// Removes any session tokens that belong to the given user ID and are of the provided session token type.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task LogoutUser(int userId, SessionTokenType type);

        /// <summary>
        /// Removes single client session token.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task LogoutClient(int clientId);

        /// <summary>
        /// Generates and returns a session token for mobile applications. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notificationToken"></param>
        /// <returns></returns>
        Task<SessionToken> GenerateAppToken(int userId, string notificationToken);

        /// <summary>
        /// Generates and returns a session token for the web application of administrator user type.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<SessionToken> GenerateAdminToken(int userId);
    }
}
