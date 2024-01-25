import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RoutingPathConstant } from 'src/app/constants/routing/routing-path';
import { ValidationMessageConstant } from 'src/app/constants/validation/validation-message';
import { ValidationPattern } from 'src/app/constants/validation/validation-pattern';
import { ILoginInterface } from 'src/app/models/login.Interface';
import { LoginService } from 'src/app/services/authentication/login.service';

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

  constructor(private _loginService:LoginService){

  }

  ngOnInit(): void {
    
  }

  onSubmit(){
    this.loginForm.markAllAsTouched();
    if(this.loginForm.valid)
    this._loginService.login(<ILoginInterface>this.loginForm.value)
  }

  rememberMeClick(checkbox:any){
    this.loginForm.value.rememberMe = checkbox.target.checked;  
  }

}
