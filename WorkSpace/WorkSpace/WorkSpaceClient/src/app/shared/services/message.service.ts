import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  message : string = '';
  constructor(private toastr : ToastrService) { }

  clear(){
    this.message = '';
  }

  success(message:string,action?:string){
    this.toastr.success(message,action,{
      positionClass:'toast-top-right',
      timeOut:3000
    })
  }

  error(message:string,action?:string){
    this.toastr.error(message,action,{
      positionClass:'toast-top-right',
      timeOut:3000
    })
  }

  info(message:string,action?:string){
    this.toastr.info(message,action,{
      positionClass:'toast-top-right',
      timeOut:3000
    })
  }

  warning(message:string,action?:string){
    this.toastr.warning(message,action,{
      positionClass:'toast-top-right',
      timeOut:3000
    })
  }
}
