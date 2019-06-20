import { FileService } from './../../services/file.service';
import { Component, Inject, Input } from '@angular/core';
import { GetFolderContentComponent } from '../get-folder-content/get-folder-content.component';

@Component({
  selector: 'app-delete-file',
  templateUrl: './delete-file.component.html',
  styleUrls: ['./delete-file.component.css']
})
export class DeleteFileComponent {
  @Input() fileId: number;

  constructor(private service: FileService,
              @Inject(GetFolderContentComponent) private parent: GetFolderContentComponent) {}

  deleteFile() {
    this.service.deleteFileById(this.fileId).subscribe(f => {
      console.log('file deleted');
      this.parent.openFolder(this.parent.myFolder.id);
     });
  }
}
