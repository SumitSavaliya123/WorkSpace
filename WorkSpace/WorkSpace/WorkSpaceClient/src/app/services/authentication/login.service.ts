import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ApiCallConstants } from 'src/app/constants/api-call/api';
import { RoutingPathConstant } from 'src/app/constants/routing/routing-path';
import { ILoginInterface } from 'src/app/models/login.Interface';
import { StorageHelperService } from '../storage-helper.service';
import { StorageHelperConstant } from 'src/app/shared/storage-helper/storage-helper';
import { IMailInterface } from 'src/app/models/mail';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  loginApi = ApiCallConstants.LOGIN_URL;
  socialMediaLoginApi = ApiCallConstants.SOCIAL_MEDIA_LOGIN_URL;

  constructor(private http: HttpClient, private router: Router, private storageHelper:StorageHelperService) {}

  login(loginCredentials: ILoginInterface):Observable<ILoginInterface> {
    return this.http
      .post<ILoginInterface>(this.loginApi, loginCredentials, {
        withCredentials: true,
        headers: {
          credentials: 'include',
        },
      })  
  }

  socialMediaLogin(email:string){
    const headers= new HttpHeaders ({'content-type': 'application/json'});
    const email1 =  {headers:headers};
    return this.http.post(this.socialMediaLoginApi, JSON.stringify(email),email1);  
  }
}
