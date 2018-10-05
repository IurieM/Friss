import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SharedModule } from '../../shared/shared.module';
import { FileService } from './file.service';
import { UploadComponent } from './components/upload/upload.component';
import { FileListComponent } from './components/file-list/file-list.component';
import { FileRoutingModule } from './file-route.module';

@NgModule({
  imports: [
    SharedModule,
    FileRoutingModule,
  ],
  providers: [FileService],
  declarations: [
    FileListComponent,
    UploadComponent
  ]
})
export class FileModule { }
