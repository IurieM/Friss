import { Component, Input } from '@angular/core';
import { NgModel } from '@angular/forms';

@Component({
    selector: 'show-error',
    templateUrl: 'show-error.component.html'
})
export class ShowErrorComponent {
    @Input() model: NgModel;
}
