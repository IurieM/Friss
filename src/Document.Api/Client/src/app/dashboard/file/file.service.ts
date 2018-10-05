import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  private fileUrl = environment.api.document;

  constructor(private http: HttpClient) { }

  getFiles() {
    return this.http.get(`${this.fileUrl}/file`);
  }

  getFile(fileId: number) {
    return this.http.get(`${this.fileUrl}/file/${fileId}`,{ responseType: "blob" });
  }

  addFile(file: any) {
    return this.http.post(`${this.fileUrl}/file`, file);
  }

  deleteFile(fileId: number) {
    return this.http.delete(`${this.fileUrl}/file/${fileId}`);
  }
}
