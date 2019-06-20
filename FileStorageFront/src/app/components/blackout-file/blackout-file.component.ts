import { Component, OnInit, Input, Inject } from '@angular/core';
import { MyFile } from 'src/app/models/file';
import { GetFolderContentComponent } from '../get-folder-content/get-folder-content.component';
import { ImageOperationsService } from 'src/app/services/imageOperations.service';

@Component({
  selector: 'app-blackout-file',
  templateUrl: './blackout-file.component.html',
  styleUrls: ['./blackout-file.component.css']
})
export class BlackoutFileComponent {

  @Input() file: MyFile;
  changing = false;
  constructor(private service: ImageOperationsService,
              @Inject(GetFolderContentComponent) private parent: GetFolderContentComponent) {  }

  blackout() {
    this.service.blackout(this.file).subscribe(() =>
      this.parent.openFolder(this.parent.myFolder.id)
    );
  }
}
