import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Injectable()
export class AlertService {
    duration: number = 3000;

    constructor(private snackBar: MatSnackBar) {
    }

    openError(message: string) {
        this.snackBar.open(message, 'Undo', {
            duration: this.duration
        });
    }

    openSuccess(message: string) {
        this.snackBar.open(message, 'Undo', {
            duration: this.duration
        });
    }
}