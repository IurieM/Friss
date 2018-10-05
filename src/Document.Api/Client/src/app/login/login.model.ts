export interface LoginModel {
    userName: string;
    password: string;
}

export interface User {
    id: number;
    userName: string;
}

export class AuthenticateUserCommand {
    constructor(public username: string, public password: string) { }
}
