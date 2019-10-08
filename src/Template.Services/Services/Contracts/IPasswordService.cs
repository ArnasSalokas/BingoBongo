namespace Template.Services.Services.Contracts
{
    public interface IPasswordService
    {
        /// <summary>
        /// Returns a hash string from the provided password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string HashPassword(string password);

        /// <summary>
        /// Validates if the provided password matches with the correct hash. Returns true if it matches.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="correctHash"></param>
        /// <returns></returns>
        bool ValidatePassword(string password, string correctHash);
    }
}
