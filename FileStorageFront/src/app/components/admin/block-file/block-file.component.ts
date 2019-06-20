import { AdminService } from './../../../services/admin.service';
import { Component, OnInit, Input, Inject } from '@angular/core';
import { MyFile } from 'src/app/models/file';
import { RootFoldersComponent } from '../root-folders/root-folders.component';

@Component({
  selector: 'app-block-file',
  templateUrl: './block-file.component.html',
  styleUrls: ['./block-file.component.css']
})
export class BlockFileComponent {
  @Input() file: MyFile;
  constructor(private service: AdminService,
              @Inject(RootFoldersComponent) private parent: RootFoldersComponent) { }
  blockFile() {
    this.service.blockFile(this.file.id).subscribe(() => {
      this.parent.openFolder(this.file.folderId);
    });
  }
}
