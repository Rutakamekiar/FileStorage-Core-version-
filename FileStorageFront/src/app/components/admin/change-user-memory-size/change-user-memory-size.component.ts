import { Component, OnInit, Input, Inject } from '@angular/core';
import { AdminService } from 'src/app/services/admin.service';
import { RootFoldersComponent } from '../root-folders/root-folders.component';
import { Folder } from 'src/app/models/folder';

@Component({
  selector: 'app-change-user-memory-size',
  templateUrl: './change-user-memory-size.component.html',
  styleUrls: ['./change-user-memory-size.component.css']
})
export class ChangeUserMemorySizeComponent {

  @Input() folder: Folder;
  size: number;
  creating: false;
  constructor(private service: AdminService,
              @Inject(RootFoldersComponent) private parent: RootFoldersComponent) { }

  changeMemorySize() {
    this.service.changeMemorySize(this.folder.userId, this.size).subscribe(() =>
    this.parent.openFolder(this.folder.id));
    this.creating = false;
  }
}
