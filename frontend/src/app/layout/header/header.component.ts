import { Component } from '@angular/core';
import { LoginFormComponent } from '../../login-form/login-form.component';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [LoginFormComponent,CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  public isLogged: boolean = false;
  constructor(private router: Router, private authService: AuthService) {
    this.isLogged = this.authService.isLoggedIn();
  }
  
  Login() {
    this.router.navigate(['/login']);
  }
  Register() {
    this.router.navigate(['/register']);
  }
  Logout() {
    this.authService.logout();
    //this.router.navigate(['/logout']);
  }
}
