import { Component, Inject, Input } from '@angular/core';
import { FolderService } from 'src/app/services/folder.service';
import { GetFolderContentComponent } from '../get-folder-content/get-folder-content.component';

@Component({
  selector: 'app-delete-folder',
  templateUrl: './delete-folder.component.html',
  styleUrls: ['./delete-folder.component.css']
})
export class DeleteFolderComponent {
  @Input() folderId: number;

  constructor(private service: FolderService,
              @Inject(GetFolderContentComponent) private parent: GetFolderContentComponent) {}

  deleteFolder() {
    this.service.deleteFolderById(this.folderId).subscribe(f => {
      console.log('folder deleted');
      this.parent.openFolder(this.parent.myFolder.id);
     });
  }
}
