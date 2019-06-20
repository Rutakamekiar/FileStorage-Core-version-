import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Folder } from '../models/folder';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  readonly baseUrl = 'http://localhost:51439/api/admin';

  constructor(private http: HttpClient) {}

  private getHeader() {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('Authorization', 'Bearer ' + localStorage.getItem('access_token'));
    return myHeaders;
   }
   getAllRootFolders(): Observable<Folder[]> {
    const myHeaders = this.getHeader();
    return this.http.get<Folder[]>(this.baseUrl + '/folders', {headers: myHeaders});
  }
  getFolderById(id: number) {
    const myHeaders = this.getHeader();
    return this.http.get<Folder>(this.baseUrl + '/folders/' + id, {headers: myHeaders});
  }
  blockFile(id: number) {
    const myHeaders = this.getHeader();
    return this.http.put(this.baseUrl + '/files/' + id, {headers: myHeaders});
  }
  getMemorySize(name: string): Observable<number> {
    const myHeaders = this.getHeader();
    return this.http.get<number>(this.baseUrl + '/memorySize/' + name, {headers: myHeaders});
  }
  changeMemorySize(name: string, size: number) {
    const data = new FormData();
    data.append('memorySize', size.toString());
    const myHeaders = this.getHeader();
    return this.http.put(this.baseUrl + '/users/' + name, data, {headers: myHeaders});
  }
}
