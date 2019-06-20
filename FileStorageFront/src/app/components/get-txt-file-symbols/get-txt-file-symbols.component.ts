import { Component, OnInit, Input, Inject } from '@angular/core';
import { MyFile } from 'src/app/models/file';
import { TxtOperationsService } from 'src/app/services/txtOperations.service';
import { GetFolderContentComponent } from '../get-folder-content/get-folder-content.component';

@Component({
  selector: 'app-get-txt-file-symbols',
  templateUrl: './get-txt-file-symbols.component.html',
  styleUrls: ['./get-txt-file-symbols.component.css']
})
export class GetTxtFileSymbolsComponent {
  @Input() file: MyFile;
  changing = false;
  constructor(private txtService: TxtOperationsService,
              @Inject(GetFolderContentComponent) private parent: GetFolderContentComponent) {  }
  getTxtFileSymbolsCount() {
    this.txtService.getTxtFileSymbolsCount(this.file).subscribe(f =>
      window.alert('Symbols count = ' + f)
    );
  }

}
