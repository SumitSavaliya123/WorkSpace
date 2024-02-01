import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { StorageHelperConstant } from '../shared/storage-helper/storage-helper';
import { StorageHelperService } from './storage-helper.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { RoutingPathConstant } from '../constants/routing/routing-path';


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private router: Router,
    private jwtService: JwtHelperService,
    private http: HttpClient,
    private storageHelper: StorageHelperService
  ) {}

  decodeToken(token: string) {
    const decodedToken = this.jwtService.decodeToken(token);
    const userRole =
      decodedToken[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ];
    const userId = decodedToken['UserId'];

    const userName =
      decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
      ];

    this.storageHelper.setAsLocal(StorageHelperConstant.authToken, token);
    this.storageHelper.setAsLocal(StorageHelperConstant.userRole, userRole);
    this.storageHelper.setAsLocal(StorageHelperConstant.userId, userId);
    this.storageHelper.setAsLocal(StorageHelperConstant.userName, userName);
  }

  isAuthenticate() {
    if (this.getJwtToken() === null || this.getJwtToken() === "") {
      return false;
    } else {
      return true;
    }
  }

  getJwtToken(){
    return this.storageHelper.getFromLocal(StorageHelperConstant.authToken);
  }

  getRefreshToken(){
    return this.storageHelper.getFromLocal(StorageHelperConstant.refreshToken);
  }

  getUserRole(){
    return this.storageHelper.getFromLocal(StorageHelperConstant.userRole);
  }

  getUserId(){
    return this.storageHelper.getFromLocal(StorageHelperConstant.userId);
  }

  getUserName(){
    return this.storageHelper.getFromLocal(StorageHelperConstant.userName);
  }

  logout(){
    this.storageHelper.removeFromLocal(StorageHelperConstant.authToken);
    this.storageHelper.removeFromLocal(StorageHelperConstant.refreshToken);
    this.storageHelper.removeFromLocal(StorageHelperConstant.userRole);
    this.storageHelper.removeFromLocal(StorageHelperConstant.userId);
    this.storageHelper.removeFromLocal(StorageHelperConstant.userName);
    this.storageHelper.removeFromLocal(StorageHelperConstant.rememberMe);   
    this.router.navigate([RoutingPathConstant.loginUrl]);
  }


}
