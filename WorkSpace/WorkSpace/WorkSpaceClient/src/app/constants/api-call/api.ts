export class ApiCallConstants{
    public static readonly BASE_URL= 'https://localhost:7130/api/';


    //Area name
     public static readonly AREA_AUTHENTICATION = this.BASE_URL + 'authentication';
     

    //Authentication
     public static readonly LOGIN_URL = this.AREA_AUTHENTICATION  + '/login';
     public static readonly SOCIAL_MEDIA_LOGIN_URL = this.AREA_AUTHENTICATION  + '/socialmedialogin';
     public static readonly VERIFY_OTP_URL =this.AREA_AUTHENTICATION + '/verify-otp';
     public static readonly RESEND_OTP_URL =this.AREA_AUTHENTICATION + '/resend-otp';
     public static readonly FORGOT_PASSWORD_URL = this.AREA_AUTHENTICATION + '/forgot-password';
     public static readonly RESET_PASSWORD_URL = this.AREA_AUTHENTICATION + '/reset-password';
     public static readonly REFRESH_TOKEN_URL = this.AREA_AUTHENTICATION + '/refresh-jwttoken';

}