import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/AuthService';

@Component({
  selector: 'app-app-header',
  templateUrl: './app-header.component.html',
  styleUrl: './app-header.component.css'
})
export class AppHeaderComponent {
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  onLogin(): void {
    this.router.navigate(['/login']);
  }

  onLogout(): void {
    this.authService.logout();
    }
  get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

}
