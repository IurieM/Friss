import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../../../shared/services/alert-service';
import { FileService } from '../../file.service';
import { FileListModel } from '../../file';
import { MatDialogRef, MatDialog } from '@angular/material';
import { ConfirmComponent } from '../../../../shared/components/confirm/confirm.component';
import { messages } from '../../../../shared/pipes/translate/lang-en';

@Component({
  selector: 'file-list',
  templateUrl: './file-list.component.html'
})
export class FileListComponent implements OnInit {
  files: FileListModel[] = [];
  confirmRef: MatDialogRef<ConfirmComponent>;

  constructor(private fileService: FileService, private alertService: AlertService, public dialog: MatDialog, ) { }

  ngOnInit(): void {
    this.getFiles();
  }

  downloadFile(fileId: number) {
    this.fileService.getFile(fileId).subscribe((response: any) => {
      var blob = new Blob([response], { type: response.type });
      var url = window.URL.createObjectURL(blob);
      window.open(url);
    });
  }

  getFiles() {
    this.fileService.getFiles().subscribe((files: any) => this.files = files);
  }

  removeFile(fileId: number) {
    this.confirmRef = this.dialog.open(ConfirmComponent, { disableClose: true, width: '300px' });
    this.confirmRef.componentInstance.Message = messages["File.Delete.Confirm"];
    this.confirmRef.componentInstance.onAccept
      .subscribe(() => this.fileService.deleteFile(fileId).subscribe(() => {
        this.confirmRef.close();
        this.alertService.openSuccess(messages["File.Removed"])
        this.getFiles();
      }));
  }
}
