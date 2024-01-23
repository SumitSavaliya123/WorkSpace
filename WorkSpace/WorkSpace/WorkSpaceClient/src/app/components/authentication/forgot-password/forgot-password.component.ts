import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RoutingPathConstant } from 'src/app/constants/routing/routing-path';
import { ValidationMessageConstant } from 'src/app/constants/validation/validation-message';
import { ValidationPattern } from 'src/app/constants/validation/validation-pattern';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})

export class ForgotPasswordComponent implements OnInit{

  emailValidationMsg:string = ValidationMessageConstant.email;
  loginUrl:string = RoutingPathConstant.loginUrl;
  forgotPasswordForm = new FormGroup({
    email : new FormControl("",Validators.compose([Validators.required,Validators.pattern(ValidationPattern.email)]))
  })
  cunstructor(){

  }

  ngOnInit(): void {
    
  }

  onSubmit(){
    this.forgotPasswordForm.markAllAsTouched();
    if(this.forgotPasswordForm.valid) {
      console.log(this.forgotPasswordForm.value);
    }
  }

}
