import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-verify-otp',
  templateUrl: './verify-otp.component.html',
  styleUrls: ['./verify-otp.component.scss']
})
export class VerifyOtpComponent implements OnInit {

  userName:string = 'Sumit';
  verifyOtpForm = new FormGroup({
    otp: new FormControl('', Validators.compose([Validators.required,Validators.minLength(6),Validators.maxLength(6)]))
  })

  constructor() {

  }

  ngOnInit(): void {
    
  }

  onSubmit(){
    this.verifyOtpForm.markAllAsTouched();
    if(this.verifyOtpForm.valid){
       console.log(this.verifyOtpForm.value);
       
    }
  }

  resendOtp(){

  }
}
