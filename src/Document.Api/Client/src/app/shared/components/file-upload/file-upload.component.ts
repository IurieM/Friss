import { Component, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { CommonService } from '../../services/common.service';

@Component({
    selector: 'file-upload',
    template: `
        <div class="form-group">
            <button mat-raised-button color="primary" type="button" name="uploadButton" mat-button (click)="fileInput.click()">
                    {{'Upload' | translate}}
            </button>
             <label>{{filename}}</label>
            <input class='d-none' type="file" #fileInput (change)="onFileUpload()" />
        </div>
     `
})
export class FileUploadComponent {
    @ViewChild('fileInput') fileInput: any;
    @Output() onFileRead = new EventEmitter<any>();
    @Input() fileType: string;
    @Input() maxFileSize: number;
    filename: string;
    constructor(private commonService: CommonService, ) {

    }

    onFileUpload() {
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {

            if (!this.isFileTypeValid(fi.files[0])) {
                return;
            }

            if (!this.isFileSizeValid(fi.files[0])) {
                return;
            }
            this.filename = fi.files[0].name;
            this.onFileRead.emit(fi.files[0]);
        }
    }

    private isFileTypeValid(file: File) {
        if (!this.fileType) {
            return true;
        }

        if (!file.name.endsWith(this.fileType)) {
            let message = this.commonService.translateService.get('Upload.InvalidFileType').replace('{ext}', this.fileType);
            this.commonService.alertService.openError(message);
            return false;
        }

        return true;
    }

    private isFileSizeValid(file: File) {
        if (!this.maxFileSize || this.maxFileSize == 0) {
            return true;
        }
        var fileSize = file.size;
        if (fileSize > this.maxFileSize) {
            let message = this.commonService.translateService.get('Upload.FileTooBig').replace('{size}', this.maxFileSize.toString());
            this.commonService.alertService.openError(message);
            return false;
        }
        return true;
    }
}