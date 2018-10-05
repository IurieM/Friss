import { NgModule } from '@angular/core';
import {
    MatCardModule, MatIconModule, MatInputModule,
    MatButtonModule, MatListModule, MatSnackBarModule, MatTooltipModule, MatDialogModule
} from '@angular/material';

@NgModule({
    imports: [MatCardModule, MatIconModule, MatInputModule,
        MatButtonModule, MatListModule, MatSnackBarModule, MatTooltipModule, MatDialogModule],
    exports: [MatCardModule, MatIconModule, MatInputModule,
        MatButtonModule, MatListModule, MatSnackBarModule, MatTooltipModule, MatDialogModule],
})
export class MaterialModule { }
