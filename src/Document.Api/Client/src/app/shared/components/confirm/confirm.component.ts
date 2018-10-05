import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
    selector: 'confirm',
    templateUrl: 'confirm.component.html',
    styleUrls: ['confirm.component.css']
})
export class ConfirmComponent {
    @Input() Message: string;
    @Output() onAccept = new EventEmitter();

    constructor(public dialogRef: MatDialogRef<ConfirmComponent>) {
    }

    accept() {
        this.onAccept.emit();
    }
}
