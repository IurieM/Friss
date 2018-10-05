import { Component, EventEmitter, Output } from '@angular/core';
import { FileService } from '../../file.service';
import { CommonService } from '../../../../shared/services/common.service';

@Component({
    selector: 'upload-file',
    templateUrl: 'upload.component.html'
})
export class UploadComponent {
    file: any;
    fileName: string;
    @Output() onFileUploaded = new EventEmitter();
    constructor(private fileService: FileService, private commonService: CommonService) {

    }

    updateModel(file: any) {
        this.file = file;
        this.fileName = file.name;
    }

    save() {
        let message = this.commonService.translateService.get("File.Uploaded");
        
        let input = new FormData();
        input.append('File', this.file);
        this.fileService.addFile(input).subscribe(() => 
        {
            this.commonService.alertService.openSuccess(message);
            this.onFileUploaded.emit();
        });
    }
}
