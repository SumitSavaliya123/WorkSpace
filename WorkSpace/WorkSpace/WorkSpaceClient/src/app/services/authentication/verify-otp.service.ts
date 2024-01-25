import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { StorageHelperService } from '../storage-helper.service';
import { IVerifyOtpInterface } from 'src/app/models/verify-otp.inerface';
import { StorageHelperConstant } from 'src/app/shared/storage-helper/storage-helper';
import { ApiCallConstants } from 'src/app/constants/api-call/api';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class VerifyOtpService {
  verifyOtpApi = ApiCallConstants.VERIFY_OTP_URL;
  resendOtpApi = ApiCallConstants.RESEND_OTP_URL;

  constructor(
    private router: Router,
    private http: HttpClient,
    private storageHelper: StorageHelperService
  ) {}

  verifyOtp(id: number, otpData: IVerifyOtpInterface): Observable<any> {
    let body = {
      email: this.storageHelper.getFromSession(StorageHelperConstant.email),
      otp: otpData.otp,
      id: id,
    };

    return this.http.post(this.verifyOtpApi, body, {
      withCredentials: true,
      headers: {
        credentials: 'include',
      },
    });
  }

  resendOtp() {
    debugger;
    let body = {
      email: this.storageHelper.getFromSession(StorageHelperConstant.email),
    };
    return this.http.post(this.resendOtpApi, body);
  }
}
