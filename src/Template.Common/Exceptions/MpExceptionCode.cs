using System.ComponentModel;

namespace Template.Common.Exceptions
{
    public static class MpExceptionCode
    {
        public enum General
        {
            [Description("Unknown error.")]
            UnknownError = 10000,

            [Description("Invalid request.")]
            InvalidRequest = 10001,

            [Description("Invalid request.")]
            NotImplemented = 10002,

            [Description("Invalid response.")]
            InvalidResponse = 10003,

            [Description("Username and/or password incorrect. Try again.")]
            UserPasswordIncorrect = 10102,

            [Description("Passwords do not match.")]
            PasswordsDoNotMatch = 10110,

            [Description("Password must be at least 8 characters long and contain both letters and numbers.")]
            PasswordDoesNotMeetRequirements = 10111,

            [Description("New password must be different from previous password.")]
            PasswordSameAsOld = 10112,

            [Description("Data validation failed.")]
            DataValidationFailed = 10199,

            [Description("Unauthorized.")]
            Unauthorized = 10401,

            [Description("Invalid authentication strategy.")]
            InvalidAuthStrategy = 10511,

            [Description("Token is invalid or expired.")]
            ResetTokenExpired = 10612,

            [Description("Failed to update the access token for an API.")]
            AccessTokenUpdateFailed = 10700,

            [Description("Random generator has been corrupted!")]
            RandomGeneratorIsCorrupted = 10704
        }
    }
}
