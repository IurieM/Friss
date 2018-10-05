import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'fr-menu',
  templateUrl: './menu.component.html'
})
export class MenuComponent {
  pageTitle = 'Document Management';


  constructor(private router: Router, private authService: AuthService) { }

  get userName(): string {
    if (this.authService.currentUser) {
      return this.authService.currentUser.username;
    }
    return '';
  }

  logOut(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
