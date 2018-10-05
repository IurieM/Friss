import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../shared/services/auth.service';
import { AuthenticateUserCommand } from './login.model';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  pageTitle = 'Log In';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    if (!this.authService.isExpired) {
      this.router.navigate(["/dashboard/files"]);
    }
  }

  login(loginForm: NgForm): void {
    if (loginForm && loginForm.valid) {
      const userName = loginForm.form.value.username;
      const password = loginForm.form.value.password;
      let command = new AuthenticateUserCommand(userName, password);
      this.authService.login(command).subscribe(() => {
        this.router.navigate(['dashboard/files']);
      });
    }
  }
}