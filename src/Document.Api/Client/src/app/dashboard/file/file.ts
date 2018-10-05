export interface FileListModel {
    id: number;
    name: string;
    uploadedDate: Date;
    lastAccessedDate: Date;
    uploadedBy: string;
}

export class FileModel {
    id: number;
    name: string;
    content: any;
}
