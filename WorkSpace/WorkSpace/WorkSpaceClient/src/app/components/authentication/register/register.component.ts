import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RoutingPathConstant } from 'src/app/constants/routing/routing-path';
import { VALIDATION_CONSTANTS } from 'src/app/constants/validation/validation-constants';
import { ValidationMessageConstant } from 'src/app/constants/validation/validation-message';
import { ValidationPattern } from 'src/app/constants/validation/validation-pattern';
import { RegisterService } from 'src/app/services/authentication/register.service';
import { MessageService } from 'src/app/shared/services/message.service';
import { passwordMatchValidator } from 'src/app/validators/password-match';

@Component({
  selector: 'app-regiter',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  emailValidationMsg: string = ValidationMessageConstant.email;
  passwordValidationMsg: string = ValidationMessageConstant.password;

  constructor(private registerService:RegisterService,private router:Router,private messageService:MessageService){

  }

  registerForm = new FormGroup({
    firstName: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(VALIDATION_CONSTANTS.MIN_NAME_LENGTH),
        Validators.maxLength(VALIDATION_CONSTANTS.MAX_NAME_LENGTH),
        Validators.pattern(ValidationPattern.names),
      ])
    ),
    lastName: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(VALIDATION_CONSTANTS.MIN_NAME_LENGTH),
        Validators.maxLength(VALIDATION_CONSTANTS.MAX_NAME_LENGTH),
        Validators.pattern(ValidationPattern.names),
      ])
    ),
    email: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.email),
      ])
    ),
    password: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.password),
      ])
    ),
    repeatPassword: new FormControl(
      '',
      [Validators.required],
      [passwordMatchValidator('password')]
    ),
  });

  onSubmit() {
    this.registerForm.markAllAsTouched();        
    if(this.registerForm.valid){
      this.registerService.register(<any>this.registerForm.value).subscribe({
        next: (res:any) =>{
          this.router.navigate([RoutingPathConstant.login]);
        },
        error: (err:any) => {
          this.messageService.error('User already exist, please try with another email.');
        }
      });
    }
  }
}
