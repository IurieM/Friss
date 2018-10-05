import { Injectable } from '@angular/core';
import { TranslateService } from '../pipes/translate/translate.service';
import { AlertService } from './alert-service';

@Injectable()
export class CommonService {
    constructor(public alertService: AlertService,
        public translateService: TranslateService) {
    }
}
