import { Component, Inject, Input } from '@angular/core';
import { FolderService } from 'src/app/services/folder.service';
import { GetFolderContentComponent } from '../get-folder-content/get-folder-content.component';
import { Folder } from 'src/app/models/folder';

@Component({
  selector: 'app-update-folder',
  templateUrl: './update-folder.component.html',
  styleUrls: ['./update-folder.component.css']
})
export class UpdateFolderComponent {
  @Input() folder: Folder;
  changing = false;
  constructor(private service: FolderService,
              @Inject(GetFolderContentComponent) private parent: GetFolderContentComponent) {}

  onClickUpdate() {
    this.service.updateFolder(this.folder).subscribe(() =>
      this.parent.openFolder(this.parent.myFolder.id)
    );
  }
}
