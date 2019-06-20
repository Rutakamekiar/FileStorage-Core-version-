import { Component, Inject, Input } from '@angular/core';
import { FileService } from 'src/app/services/file.service';
import { GetFolderContentComponent } from '../get-folder-content/get-folder-content.component';
import {saveAs as importedSaveAs} from 'file-saver';
import { MyFile } from 'src/app/models/file';

@Component({
  selector: 'app-download-file',
  templateUrl: './download-file.component.html',
  styleUrls: ['./download-file.component.css']
})
export class DownloadFileComponent {
  @Input() file: MyFile;

  constructor(private service: FileService,
              @Inject(GetFolderContentComponent) private parent: GetFolderContentComponent) { }

  downloadFile() {
    console.log('downloading');
    this.service.downloadFile(this.file.id).subscribe(f => {
        importedSaveAs(f, this.file.name);
    });
  }
}
