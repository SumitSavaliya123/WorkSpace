import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiCallConstants } from 'src/app/constants/api-call/api';
import { RegisterForm } from 'src/app/models/register';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  registerApi = ApiCallConstants.REGISTER_URL;

  constructor(private http:HttpClient) { }

  register(registerForm:RegisterForm):Observable<any>{
    return this.http.post<any>(this.registerApi,registerForm);
  }
}
