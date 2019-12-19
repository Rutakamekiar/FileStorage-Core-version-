import { Component, OnInit } from '@angular/core';
import { FolderService } from 'src/app/services/folder.service';
import { UserService } from 'src/app/services/user.service';

import { Router } from '@angular/router';
import { AccountDetails } from 'src/app/models/responses/account-details';
import { Folder } from 'src/app/models/responses/folder';
import { CreateFolderRequest } from 'src/app/models/requests/create-folder-request';
import { FileService } from 'src/app/services/file.service';
import { MyFile } from 'src/app/models/responses/my-file';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private folderService: FolderService,
              private userService: UserService,
              private fileService: FileService,
              private router: Router) { }

  accountDetails: AccountDetails = new AccountDetails();
  folder: Folder = new Folder();
  createFolderName: string;
  fileToUpload: File;
  ngOnInit() {
    this.userService.getAccountDetails().subscribe(r => {
      this.accountDetails = r;
    });

    this.folderService.getRootFolder('').subscribe(r => {
      console.log(r);
      this.folder = r;
    });
  }

  deleteFolder(id: string) {
    this.folderService.deleteFolder(id).subscribe(() => {
      this.getFolder(this.folder.id);
    });
  }

  createFolder() {
    const model = new CreateFolderRequest();
    model.name = this.createFolderName;
    model.parentId = this.folder.id;
    this.folderService.createFolder(model).subscribe(() => {
      this.getFolder(this.folder.id);
    });
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
  }

  uploadFile() {
    this.fileService.uploadFile(this.fileToUpload, true, this.folder.id).subscribe(() => {
      this.getFolder(this.folder.id);
    });
  }

  deleteFile(id: string) {
    this.fileService.deleteFile(id).subscribe(() => {
      this.getFolder(this.folder.id);
    });
  }

  private getFolder(id: string) {
    this.folderService.getRootFolder(id).subscribe(r => {
      console.log(r);
      this.folder = r;
    });
  }
}
