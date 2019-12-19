import { Component, OnInit } from '@angular/core';
import { FolderService } from 'src/app/services/folder.service';
import { UserService } from 'src/app/services/user.service';

import { Router, Params, ActivatedRoute } from '@angular/router';
import { AccountDetails } from 'src/app/models/responses/account-details';
import { Folder } from 'src/app/models/responses/folder';
import { CreateFolderRequest } from 'src/app/models/requests/create-folder-request';
import { FileService } from 'src/app/services/file.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private folderService: FolderService,
              private userService: UserService,
              private fileService: FileService,
              private router: Router,
              private activatedRoute: ActivatedRoute) { }

  accountDetails: AccountDetails = new AccountDetails();
  folder: Folder = new Folder();
  createFolderName: string;
  fileToUpload: File;
  usedSpace: number;

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      let id = params.id;
      if (id === undefined) {
        id = '';
      }

      this.updatePage(id);
    });
  }

  deleteFolder(id: string) {
    this.folderService.deleteFolder(id).subscribe(() => {
      this.updatePage(this.folder.id);
    });
  }

  createFolder() {
    const model = new CreateFolderRequest();
    model.name = this.createFolderName;
    model.parentId = this.folder.id;
    this.folderService.createFolder(model).subscribe(() => {
      this.updatePage(this.folder.id);
    });
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
  }

  uploadFile() {
    this.fileService.uploadFile(this.fileToUpload, true, this.folder.id).subscribe(() => {
      this.updatePage(this.folder.id);
    });
  }

  deleteFile(id: string) {
    this.fileService.deleteFile(id).subscribe(() => {
      this.updatePage(this.folder.id);
    });
  }

  openFolder(id: string) {
    this.router.navigate([`/home/${id}`]);
    this.updatePage(id);
  }

  getBack() {
    this.router.navigate([`/home/${this.folder.parentFolderId}`]);
    this.updatePage(this.folder.parentFolderId);
  }

  private updatePage(id: string) {
    this.folderService.getFolderById(id).subscribe(r => {
      console.log(r);
      this.folder = r;
    });
    this.folderService.getSpaceUsedCount().subscribe(r => {
      this.usedSpace = r;
    });
    this.userService.getAccountDetails().subscribe(r => {
      this.accountDetails = r;
    });
  }
}
