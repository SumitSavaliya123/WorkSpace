export class RoutingPathConstant {
 //authentication routing path
  public static register = 'register';
  public static registerUrl = '/register';

  public static login = 'login';
  public static loginUrl = '/login';

  public static verifyOtp = 'verify-otp';
  public static verifyOtpUrl = '/verify-otp';

  public static forgotPassword = 'forgot-password';
  public static forgotPasswordUrl = '/forgot-password';

  public static resetPassword = 'reset-password';
  public static resetPasswordUrl = '/reset-password';

  //after signin dashboard routing path

  public static dashboard = 'dashboard';
  public static dashboardUrl = '/dashboard';

  public static managerDashboard = 'manager/dashboard';
  public static managerDashboardUrl = '/manager/dashboard';

  public static employeeDashboard = 'employee/dashboard';
  public static employeeDashboardUrl = '/employee/dashboard';

  //manage authentication routing path
  
  public static unauthorize = 'unauthorize';
  public static unauthorizeUrl = '/unauthorize';

  public static sessionExpire = 'session-expired';
  public static sessionExpireUrl = '/session-expired';

  public static notFound = 'not-found';
  public static notFoundUrl = '/not-found';
}
