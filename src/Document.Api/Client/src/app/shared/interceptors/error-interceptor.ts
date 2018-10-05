import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlertService } from '../services/alert-service';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private alertService: AlertService, private router: Router, private authService: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            const error = err.error || err.statusText;
            if (err.status === 401) {
                this.authService.logout();
                this.router.navigate(['/login']);
            }
            if (err.status !== 200) {
                this.alertService.openError(error);
            }

            return throwError(error);
        }))
    }
}