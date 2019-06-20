import { GetFolderContentComponent } from './../get-folder-content/get-folder-content.component';
import { Folder } from 'src/app/models/folder';
import { FolderService } from './../../services/folder.service';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-create-folder',
  templateUrl: './create-folder.component.html',
  styleUrls: ['./create-folder.component.css']
})
export class CreateFolderComponent {

  constructor(private service: FolderService,
              @Inject(GetFolderContentComponent) private parent: GetFolderContentComponent) {}
  creating = false;
  parentId: number;
  name: string;

  onClickCreate() {
    const folder: Folder = new Folder();
    folder.name = this.name;
    folder.parentFolderId = this.parent.myFolder.id;
    this.service.createFolder(folder).subscribe(() =>
      this.parent.openFolder(this.parent.myFolder.id)
    );
    this.creating = false;
  }
}
