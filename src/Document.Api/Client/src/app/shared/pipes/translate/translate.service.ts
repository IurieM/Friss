import { Injectable } from '@angular/core';
import { messages } from './lang-en';

@Injectable()
export class TranslateService {
    messageList: any = messages;
    get(key: string) {
        return this.messageList[key] || key;
    }
}
