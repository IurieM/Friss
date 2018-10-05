import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './interceptors/token-interceptor';
import { ErrorInterceptor } from './interceptors/error-interceptor';
import { MaterialModule } from '../material.module';
import { AuthService } from './services/auth.service';
import { AuthGuard } from './services/auth-guard.service';
import { AlertService } from './services/alert-service';
import { ShowErrorComponent } from './components/show-error/show-error.component';
import { TranslateModule } from './pipes/translate/transalte.module';
import { CommonService } from './services/common.service';
import { IsAdminDirective } from './directives/isAdmin.directive';
import { FileUploadComponent } from './components/file-upload/file-upload.component';
import { ConfirmComponent } from './components/confirm/confirm.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    TranslateModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AuthService,
    AuthGuard,
    AlertService,
    CommonService
  ],
  declarations: [
    ShowErrorComponent,
    FileUploadComponent,
    IsAdminDirective,
    ConfirmComponent
  ],
  entryComponents: [
    ConfirmComponent
  ],
  exports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    TranslateModule,
    ShowErrorComponent,
    FileUploadComponent,
    IsAdminDirective,
  ]
})
export class SharedModule { }
