import { MyFile } from './my-file';

export class Folder {
  public id: string;

  public name: string;

  public path: string;

  public files: MyFile[];

  public folders: Folder[];

  public ParentFolderId: string;
}
