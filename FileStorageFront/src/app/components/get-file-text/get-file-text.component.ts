import { Component, OnInit, Input, Inject } from '@angular/core';
import { MyFile } from 'src/app/models/file';
import { TxtOperationsService } from 'src/app/services/txtOperations.service';
import { GetFolderContentComponent } from '../get-folder-content/get-folder-content.component';

@Component({
  selector: 'app-get-file-text',
  templateUrl: './get-file-text.component.html',
  styleUrls: ['./get-file-text.component.css']
})
export class GetFileTextComponent {
  @Input() file: MyFile;
  changing = false;
  constructor(private txtService: TxtOperationsService,
              @Inject(GetFolderContentComponent) private parent: GetFolderContentComponent) {  }
  getTxtFile() {
    this.txtService.getTxtFile(this.file).subscribe(f =>
      window.alert(f.text)
    );
  }
}
