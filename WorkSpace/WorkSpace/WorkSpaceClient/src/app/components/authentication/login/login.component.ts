import { GoogleLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { RoutingPathConstant } from 'src/app/constants/routing/routing-path';
import { ValidationMessageConstant } from 'src/app/constants/validation/validation-message';
import { ValidationPattern } from 'src/app/constants/validation/validation-pattern';
import { ILoginInterface } from 'src/app/models/login.Interface';
import { AuthService } from 'src/app/services/auth.service';
import { LoginService } from 'src/app/services/authentication/login.service';
import { StorageHelperService } from 'src/app/services/storage-helper.service';
import { StorageHelperConstant } from 'src/app/shared/storage-helper/storage-helper';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  emailValidationMsg : string = ValidationMessageConstant.email;
  passwordValidationMsg : string =ValidationMessageConstant.password;
  forgotPasswordUrl:string = RoutingPathConstant.forgotPasswordUrl;

  loginForm = new FormGroup({
    email : new FormControl('',Validators.compose([Validators.required,Validators.pattern(ValidationPattern.email)])),
    password: new FormControl('',Validators.compose([Validators.required,Validators.pattern(ValidationPattern.password)])),
    rememberMe : new FormControl(false)
  });

  constructor(private _loginService:LoginService,private authService: SocialAuthService, private router:Router, private storageHelper:StorageHelperService){  }

  ngOnInit(){
    this.socialMediaLogin();
   }

  onSubmit(){
    this.loginForm.markAllAsTouched();
    if(this.loginForm.valid)
    this._loginService.login(<ILoginInterface>this.loginForm.value).subscribe({
      next: (res: any) => {
        if(res.success){
          this.storageHelper.setAsSession(StorageHelperConstant.email, String(this.loginForm.value.email));
          this.router.navigate([RoutingPathConstant.verifyOtpUrl]);
        }   
      },
    });
  }

  rememberMeClick(checkbox:any){
    this.loginForm.value.rememberMe = checkbox.target.checked;  
  }

  socialMediaLogin(){
	  this.authService.authState.subscribe((userfromGoogle: SocialUser) => {
      this._loginService.socialMediaLogin(userfromGoogle.email).subscribe(
        (response:any) => {
          if(response.success){
            this.router.navigate([RoutingPathConstant.verifyOtpUrl]);
            this.storageHelper.setAsSession(StorageHelperConstant.email,userfromGoogle.email);
          }
        }
      )
    });
  }

}
