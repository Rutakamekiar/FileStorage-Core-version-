import { FileService } from './../../services/file.service';
import { MyFile } from './../../models/file';
import { Component, OnInit, Input, Inject } from '@angular/core';
import { GetFolderContentComponent } from '../get-folder-content/get-folder-content.component';

@Component({
  selector: 'app-update-file',
  templateUrl: './update-file.component.html',
  styleUrls: ['./update-file.component.css']
})
export class UpdateFileComponent {
  @Input() file: MyFile;
  changing = false;
  constructor(private service: FileService,
              @Inject(GetFolderContentComponent) private parent: GetFolderContentComponent) {  }

  onClickUpdate() {
    this.service.updateFile(this.file).subscribe(() =>
      this.parent.openFolder(this.parent.myFolder.id)
    );
  }
}
