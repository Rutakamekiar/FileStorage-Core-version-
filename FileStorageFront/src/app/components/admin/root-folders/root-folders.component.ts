import { UserService } from './../../../services/user.service';
import { FolderService } from 'src/app/services/folder.service';
import { AdminService } from './../../../services/admin.service';
import { Component, OnInit } from '@angular/core';
import { Folder } from 'src/app/models/folder';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-root-folders',
  templateUrl: './root-folders.component.html',
  styleUrls: ['./root-folders.component.css']
})
export class RootFoldersComponent implements OnInit {

  constructor(private service: AdminService,
              private folderService: FolderService,
              private userService: UserService,
              private activatedRoute: ActivatedRoute) {}
  myFolder: Folder;
  rootFolders: Folder[] = [];
  memorySize: number;
  ngOnInit() {
    let folderId: number;
    this.activatedRoute.params.subscribe((params: Params) => {
// tslint:disable-next-line: no-string-literal
      folderId = params['id'];
    });
    if (folderId === undefined) {
      this.service.getAllRootFolders().subscribe(f => {
        this.rootFolders = f;
      });
    } else {
      this.openFolder(folderId);
    }
  }
  openFolder(folderId: number) {
    this.service.getFolderById(folderId).subscribe(f => {
      this.myFolder = f;
      this.service.getMemorySize(f.userId).subscribe(m => this.memorySize = m);
      console.log(f);
     });
  }

}
