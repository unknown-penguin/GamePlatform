import { Component } from '@angular/core';
import { LoginFormComponent } from '../../login-form/login-form.component';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [LoginFormComponent,CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  public isLogged: boolean = true;
  isLoginModalOpen = false;
  constructor(private router: Router) {
  }
  
  Login() {
    this.router.navigate(['/login']);
  }
  Register() {
    this.router.navigate(['/register']);
  }
  Logout() {
    this.router.navigate(['/logout']);
  }
}
