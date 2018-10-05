import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateService } from './translate.service';
import { TranslatePipe } from './translate.pipe';

@NgModule({
    imports: [CommonModule],
    providers: [TranslateService],
    declarations: [TranslatePipe],
    exports: [TranslatePipe]
})

export class TranslateModule { }
