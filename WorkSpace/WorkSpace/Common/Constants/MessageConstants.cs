using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Constants
{
    public class MessageConstants
    {
        #region Success Messages
        public const string GlobalSuccess = "Success";

        public const string GlobalCreated = "Resource created successfully.";

        public static readonly string InvalidLoginCredential = "Invalid email/username or password.";

        public static readonly string LoginSuccess = "You are logged in !!";

        public static readonly string MailSent = "Mail has been sent to your registered email.";

        public static readonly string PasswordReset = "Your Password reset successfully.";

        #endregion

        #region Exception Messages
        public static readonly string DEFAULT_MODELSTATE = "Model state is invalid!";

        public static readonly string VALIDATION_ERROR = "One or more validation failures have occured!";

        public static readonly string Invalidotp = "Invalid OTP";

        public const string INVALID_ATTEMPT = "Invalid Attempt!";

        public const string TOKEN_EXPIRE = "Your session has been expired!";

        public const string INVALID_TOKEN = "Invalid Token!";
        #endregion


        public static readonly string EmailAlreadyExists = "User already exists. Please try with other email.";

    }
}
