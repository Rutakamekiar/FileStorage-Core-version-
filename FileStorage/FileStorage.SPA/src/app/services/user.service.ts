import { Injectable } from '@angular/core';
import { RegisterBindingModel } from '../models/register-binding-model';
import { baseUrl } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  registerUser(model: RegisterBindingModel) {
    console.log(model);
    const url = baseUrl + '/register';
    return this.http.post(url, model);
  }
}
