import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RoutingPathConstant } from 'src/app/constants/routing/routing-path';
import { ValidationMessageConstant } from 'src/app/constants/validation/validation-message';
import { ValidationPattern } from 'src/app/constants/validation/validation-pattern';
import { ForgotPasswordService } from 'src/app/services/authentication/forgot-password.service';

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
  
  constructor(private service: ForgotPasswordService, private router:Router){  }

  ngOnInit(): void {
    
  }

  onSubmit(){
    this.forgotPasswordForm.markAllAsTouched();
    if(this.forgotPasswordForm.valid) {
      this.service.forgotPassword(<string>this.forgotPasswordForm.value.email).subscribe({
        next:(res) => this.router.navigate([RoutingPathConstant.loginUrl])

        
      })
    }
  }

}
