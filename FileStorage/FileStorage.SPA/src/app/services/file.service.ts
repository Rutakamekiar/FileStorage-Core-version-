import { Injectable } from '@angular/core';
import { baseUrl } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MyFile } from '../models/responses/my-file';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  path: string = baseUrl + '/files';
  constructor(private http: HttpClient) { }

  uploadFile(file: File, accessLevel: boolean, folderId: string) {
    const url = this.path + `?accessLevel=${accessLevel}&folderId=${folderId}`;
    const myHeaders = this.getHeaders();
    const data = new FormData();
    data.append('File', file , file.name);
    return this.http.post(url, data, {headers: myHeaders});
  }
  deleteFile(id: string) {
    const url = this.path + '/' + id;
    const myHeaders = this.getHeaders();
    return this.http.delete(url, {headers: myHeaders});
  }
  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('Authorization', 'bearer ' + token);
    return myHeaders;
  }
}
