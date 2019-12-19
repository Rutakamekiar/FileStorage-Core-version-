import { Injectable } from '@angular/core';
import { baseUrl } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginBindingModel } from '../models/requests/login-binding-model';
import { LoginResponse } from '../models/responses/login-response';
import { RegisterBindingModel } from '../models/requests/register-binding-model';
import { AccountDetails } from '../models/responses/account-details';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  path: string = baseUrl + '/account';
  constructor(private http: HttpClient) { }

  registerUser(model: RegisterBindingModel) {
    const url = this.path + '/register';
    return this.http.post(url, model);
    }

  login(model: LoginBindingModel): Observable<LoginResponse>   {
    const url = this.path + '/signIn';
    return this.http.post<LoginResponse>(url, model);
  }

  getAccountDetails(): Observable<AccountDetails>   {
    const url = this.path + '/accountDetails';
    const token = localStorage.getItem('token');
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('Authorization', 'bearer ' + token);
    return this.http.get<AccountDetails>(url, {headers: myHeaders});
  }
}
