import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MyFile } from '../models/file';

@Injectable({
  providedIn: 'root'
})
export class ImageOperationsService {
  readonly baseUrl = 'http://localhost:5000/api/image';
  constructor(private http: HttpClient) {}

  getHeader() {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('Authorization', 'Bearer ' + localStorage.getItem('access_token'));
    return myHeaders;
   }

  blackout(newFile: MyFile) {
    const myHeaders = this.getHeader();

    return this.http.put(this.baseUrl + '/' + newFile.id + '/blackout', {headers: myHeaders});
  }
}
