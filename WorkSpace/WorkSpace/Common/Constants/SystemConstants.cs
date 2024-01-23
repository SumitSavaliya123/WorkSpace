
namespace Common.Constants
{
    public class SystemConstants
    {
        public static readonly string CorsPolicy = "WorkSpaceCors";

        public const string DEFAULT_DATETIME = "(getutcdate())";

        public const string CONNECTION_STRING_NAME = "DefaultConnection";

        public static readonly int MaxPageSizeResponse = 50;


        #region Session Constant

        public const string LoggedUser = "LoggedUser";

        public const string Bearer = "Bearer ";

        public const string RememeberMeCookieKey = "rememberMe";

        public const string TrueString = "True";

        #endregion Session Constant


        #region Claim Type

        public const string UserIdClaim = "UserId";

        #endregion Claim Type


        #region Otp

        public const string AuthenticationOtp = "AuthenticationOtp";

        public const string ProfileUpdateOtp = "ProfileUpdateOtp";

        #endregion
    }
}
