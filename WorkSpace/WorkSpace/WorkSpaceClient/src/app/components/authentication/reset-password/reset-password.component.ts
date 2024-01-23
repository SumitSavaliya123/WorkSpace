import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { passwordMatchValidator } from 'src/app/common/validators/password-match';
import { ValidationMessageConstant } from 'src/app/constants/validation/validation-message';
import { ValidationPattern } from 'src/app/constants/validation/validation-pattern';

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

  cunsructor() {}

  ngOnInit(): void {}

  onSubmit() {
    this.resetPasswordForm.markAllAsTouched();
    if (this.resetPasswordForm.valid) {
      console.log(this.resetPasswordForm.value);
    }
  }
}
