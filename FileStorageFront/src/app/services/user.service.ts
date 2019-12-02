import { Injectable } from '@angular/core';
import {Observable, Subscription, empty} from 'rxjs';
import { HttpClient, HttpHeaders , HttpErrorResponse} from '@angular/common/http';
import { UserLogin } from '../models/user-login';
import { UserLoginResponse } from '../models/user-login-response';
import {UserRegister} from '../models/user-register';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  readonly baseUrl = 'http://localhost:5000/api/account';

  constructor(private http: HttpClient) { }

  loginUser(user: UserLogin): Observable<UserLoginResponse> {
    const url = `/signIn`;
    return this.http.post<UserLoginResponse>(this.baseUrl + url, user);
  }

  registerUser(user: UserRegister) {
    console.log(user);
    const url = this.baseUrl + '/register';
    return this.http.post(url, user);
  }
  logout() {
    localStorage.removeItem('access_token');
  }
  getMemorySize(): Observable<number> {
    const myHeaders = this.getHeader();
    return this.http.get<number>(this.baseUrl + '/memorySize', {headers: myHeaders});
  }
  private getHeader() {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('Authorization', 'Bearer ' + localStorage.getItem('access_token'));
    return myHeaders;
   }
}
