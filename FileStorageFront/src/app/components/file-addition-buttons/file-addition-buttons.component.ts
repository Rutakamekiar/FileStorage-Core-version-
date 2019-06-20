import { FileService } from '../../services/file.service';
import { MyFile } from '../../models/file';
import { Component, OnInit, Input, Inject } from '@angular/core';
import { GetFolderContentComponent } from '../get-folder-content/get-folder-content.component';
import { ImageOperationsService } from 'src/app/services/imageOperations.service';
import { TxtOperationsService } from 'src/app/services/txtOperations.service';

@Component({
  selector: 'app-file-addition-buttons',
  templateUrl: './file-addition-buttons.component.html',
  styleUrls: ['./file-addition-buttons.component.css']
})
export class FileAdditionButtonsFileComponent {
  @Input() file: MyFile;
  changing = false;
  constructor(private service: ImageOperationsService,
              private txtService: TxtOperationsService,
              @Inject(GetFolderContentComponent) private parent: GetFolderContentComponent) {  }

  blackout() {
    this.service.blackout(this.file).subscribe(() =>
      this.parent.openFolder(this.parent.myFolder.id)
    );
  }
  getTxtFileSymbolsCount() {
    this.txtService.getTxtFileSymbolsCount(this.file).subscribe(f =>
      window.alert('Symbols count = ' + f)
    );
  }
  getTxtFile() {
    this.txtService.getTxtFile(this.file).subscribe(f =>
      window.alert(f.text)
    );
  }
}
