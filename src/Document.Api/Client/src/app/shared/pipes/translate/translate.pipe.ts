import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from './translate.service';

@Pipe({
    name: 'translate'
})

export class TranslatePipe implements PipeTransform {

    constructor(private translateService: TranslateService) { }

    transform(value: string): any {
        if (!value) {
            return;
        }
        return this.translateService.get(value);
    }
}