import { UserService } from './../../services/user.service';
import {Component, OnInit} from '@angular/core';
import { FolderService } from 'src/app/services/folder.service';
import { Folder } from 'src/app/models/folder';
import { ActivatedRoute, Params } from '@angular/router';
import { FileService } from 'src/app/services/file.service';

@Component({
  selector: 'app-get-folder-content',
  templateUrl: './get-folder-content.component.html',
  styleUrls: ['./get-folder-content.component.css']
})
export class GetFolderContentComponent implements OnInit {

  constructor(private folderService: FolderService,
              private userService: UserService,
              private activatedRoute: ActivatedRoute) { }

  public myFolder: Folder = new Folder();
  public memorySize: number;
  public rootFolderSize: number;
  ngOnInit() {
    let folderId: number;
    this.activatedRoute.params.subscribe((params: Params) => {
// tslint:disable-next-line: no-string-literal
      folderId = params['id'];
    });
    if (folderId === undefined) {
      this.folderService.getRootFolder().subscribe(f => {
        this.myFolder = f;
        console.log(this.myFolder);
        this.folderService.getRootFolderSize().subscribe(m => this.rootFolderSize = m);
        this.userService.getMemorySize().subscribe(m => this.memorySize = m);
      });
    } else {
      this.openFolder(folderId);
    }
  }
  openFolder(folderId: number) {
    this.folderService.getFolderById(folderId).subscribe(f => {
      this.myFolder = f;
      console.log(f);
     });
    this.folderService.getRootFolderSize().subscribe(m => this.rootFolderSize = m);
    this.userService.getMemorySize().subscribe(m => this.memorySize = m);
  }
}
