import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { baseUrl } from 'src/environments/environment';
import { Folder } from '../models/responses/folder';
import { Observable } from 'rxjs';
import { CreateFolderRequest } from '../models/requests/create-folder-request';

@Injectable({
  providedIn: 'root'
})
export class FolderService {

  path: string = baseUrl + '/folders';
  constructor(private http: HttpClient) { }

  getRootFolder(id: string): Observable<Folder> {
    const url = this.path + '/' + id;
    const myHeaders = this.getHeaders();
    return this.http.get<Folder>(url, {headers: myHeaders});
  }

  deleteFolder(id: string) {
    const url = this.path + '/' + id;
    const myHeaders = this.getHeaders();
    return this.http.delete(url, {headers: myHeaders});
  }

  createFolder(model: CreateFolderRequest) {
    const myHeaders = this.getHeaders();
    return this.http.post(this.path, model, {headers: myHeaders});
  }

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('Authorization', 'bearer ' + token);
    return myHeaders;
  }
}
