import { Component, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RoutingPathConstant } from 'src/app/constants/routing/routing-path';
import { UserRole } from 'src/app/constants/user-role';
import { IVerifyOtpInterface } from 'src/app/models/verify-otp.inerface';
import { AuthService } from 'src/app/services/auth.service';
import { VerifyOtpService } from 'src/app/services/authentication/verify-otp.service';
import { StorageHelperService } from 'src/app/services/storage-helper.service';
import { MessageService } from 'src/app/shared/services/message.service';
import { StorageHelperConstant } from 'src/app/shared/storage-helper/storage-helper';

@Component({
  selector: 'app-verify-otp',
  templateUrl: './verify-otp.component.html',
  styleUrls: ['./verify-otp.component.scss'],
})
export class VerifyOtpComponent implements OnInit {
  userName: string = 'Sumit';
  verifyOtpForm = new FormGroup({
    otp: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(6),
      ])
    ),
  });

  constructor(private service: VerifyOtpService,private authService:AuthService,private router:Router, private storageHelper:StorageHelperService, private injector: Injector) { }

  ngOnInit(): void { }

  onSubmit() {
    this.verifyOtpForm.markAllAsTouched();
    if (this.verifyOtpForm.valid) {
      this.service
        .verifyOtp(0, <IVerifyOtpInterface>this.verifyOtpForm.value)
        .subscribe({
          next: (res: any) => {
            this.authService.decodeToken(res.data.accessToken);
        this.storageHelper.setAsLocal(
          StorageHelperConstant.refreshToken,
          res.data.refreshToken
        );
            if(this.authService.getUserRole() == UserRole.managerRoleId){
                this.router.navigate([RoutingPathConstant.managerDashboard]);
            }
            else{
              this.router.navigate([RoutingPathConstant.employeeDashboard]);
            }
          },
        });
    }
  }

  resendOtp() {
    this.service.resendOtp().subscribe({
      next: (response) => {
        console.log('Success:', response);
      },
      error: (error) => {
        console.error('Error:', error);

        // Check if the error has a 'error' property with details
        if (error.error) {
          console.error('Server error details:', error.error);
        }
      },
    });
  }
}
