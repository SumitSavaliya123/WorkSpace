import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { passwordMatchValidator } from 'src/app/common/validators/password-match';
import { RoutingPathConstant } from 'src/app/constants/routing/routing-path';
import { ValidationMessageConstant } from 'src/app/constants/validation/validation-message';
import { ValidationPattern } from 'src/app/constants/validation/validation-pattern';
import { IResetPasswordInterface } from 'src/app/models/reset-password.interface';
import { ResetPasswordService } from 'src/app/services/authentication/reset-password.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss'],
})
export class ResetPasswordComponent implements OnInit {
  passwordValidationMsg: string = ValidationMessageConstant.password;
  resetPasswordForm = new FormGroup({
    password: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.password),
      ])
    ),
    confirmPassword: new FormControl(
      '',
      [Validators.required],
      [passwordMatchValidator()]
    ),
  });

  constructor(private service:ResetPasswordService ,  private router:Router) {}

  ngOnInit(): void {}

  onSubmit() {
    this.resetPasswordForm.markAllAsTouched();
    if (this.resetPasswordForm.valid) {
      console.log(this.resetPasswordForm.value);
        this.service.resetPassword(<IResetPasswordInterface>this.resetPasswordForm.value).subscribe({
          next: ()=> this.router.navigate([RoutingPathConstant.loginUrl])
        })
    }
  }
}
