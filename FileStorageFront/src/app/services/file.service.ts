import { MyFile } from './../models/file';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  readonly baseUrl = 'http://localhost:5000/api/files';
  constructor(private http: HttpClient) {}

  uploadFile(file: File, folderId: number, accessLevel: boolean) {
    console.log('create file');
    const data = new FormData();
    data.append('File', file , file.name);
    data.append('AccessLevel', accessLevel.toString());
    data.append('FolderId', folderId.toString());
    const myHeaders = this.getHeader();
    return this.http.post(this.baseUrl, data, {headers: myHeaders});
  }
  getHeader() {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('Authorization', 'Bearer ' + localStorage.getItem('access_token'));
    return myHeaders;
   }
  deleteFileById(fileId: number) {
    const myHeaders = this.getHeader();
    return this.http.delete(this.baseUrl + '/' + fileId, {headers: myHeaders});
  }
  updateFile(newFile: MyFile) {
    let myHeaders = this.getHeader();
    myHeaders = myHeaders.set('Content-Type', 'application/json');
    const json = JSON.stringify(newFile);

    return this.http.put(this.baseUrl + '/' + newFile.id, json, {headers: myHeaders});
  }
  downloadFile(fileId: number): Observable<Blob> {
    const myHeaders = this.getHeader();
    return this.http.get<Blob>(this.baseUrl + '/' + fileId, {headers: myHeaders, responseType: 'blob' as 'json'});
  }
}
