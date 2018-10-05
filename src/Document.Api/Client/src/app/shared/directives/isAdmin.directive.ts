import { Directive, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Roles } from '../model';

@Directive({
    selector: '[isAdmin]'
})
export class IsAdminDirective implements OnInit {

    constructor(private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef,
        private authService: AuthService) {
    }


    ngOnInit() {
        if (this.authService.currentUser && this.authService.currentUser.role === Roles.Admin) {
            this.viewContainer.createEmbeddedView(this.templateRef);
            return;
        }
        this.viewContainer.clear();
    }
}
