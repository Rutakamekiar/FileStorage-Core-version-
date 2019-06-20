import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MyFile } from '../models/file';

@Injectable({
  providedIn: 'root'
})
export class TxtOperationsService {
  readonly baseUrl = 'http://localhost:51439/api/txt';
  constructor(private http: HttpClient) {}

  getHeader() {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('Authorization', 'Bearer ' + localStorage.getItem('access_token'));
    return myHeaders;
   }

  getTxtFileSymbolsCount(newFile: MyFile) {
    const myHeaders = this.getHeader();

    return this.http.get(this.baseUrl + '/' + newFile.id + '/symbols', {headers: myHeaders});
  }
  getTxtFile(newFile: MyFile) {
    const myHeaders = this.getHeader();

    return this.http.get(this.baseUrl + '/' + newFile.id, {headers: myHeaders});
  }
}
