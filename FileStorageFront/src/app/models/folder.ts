import { MyFile } from './file';

export class Folder {
    constructor(public id: number = 0,
                public name: string = '',
                public userId: string = '',
                public path: string = '',
                public files: MyFile[] = [],
                public folders: Folder[] = [],
                public parentFolderId: number = null
                ) {}
}
