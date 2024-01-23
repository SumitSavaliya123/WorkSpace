import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ApiCallConstants } from 'src/app/constants/api-call/api';
import { RoutingPathConstant } from 'src/app/constants/routing/routing-path';
import { ILoginInterface } from 'src/app/models/login.Interface';
import { StorageHelperService } from '../storage-helper.service';
import { StorageHelperConstant } from 'src/app/shared/storage-helper/storage-helper';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  loginApi = ApiCallConstants.LOGIN_URL;

  constructor(private http: HttpClient, private router: Router, private storageHelper:StorageHelperService) {}

  login(loginCredentials: ILoginInterface) {
    this.storageHelper.setAsSession(StorageHelperConstant.email, loginCredentials.email);
    this.http
      .post(this.loginApi, loginCredentials, {
        withCredentials: true,
        headers: {
          credentials: 'include',
        },
      })
      .subscribe({
        next: (res: any) => {
          this.router.navigate([RoutingPathConstant.verifyOtpUrl]);
        },
      });
  }
}
