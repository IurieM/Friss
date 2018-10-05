import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthenticateUserCommand } from '../../login/login.model';
import { environment } from '../../../environments/environment';
import { constants } from '../constants';

interface User {
    id: number,
    username: string,
    role: string,
    token: string
}

@Injectable()
export class AuthService {
    authUrl: string = environment.api.document;

    constructor(private http: HttpClient) { }

    login(command: AuthenticateUserCommand): Observable<User> {
        return this.http.post<string>(`${this.authUrl}/authentication/login`, command).pipe(map((user: User) => {
            if (user && user.token) {
                localStorage.setItem(constants.cacheKeys.currentUser, JSON.stringify(user));
            }

            return user;
        }));;
    }

    get currentUser(): User {
        return JSON.parse(localStorage.getItem(constants.cacheKeys.currentUser));
    }

    get isExpired(): boolean {
        return this.currentUser == null;
    }

    logout(): void {
        localStorage.removeItem(constants.cacheKeys.currentUser)
    }
}
