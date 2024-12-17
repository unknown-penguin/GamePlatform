import { Component } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../services/auth.service';



import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-register-form',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './register-form.component.html',
  styleUrl: './register-form.component.css'
})

export class RegisterFormComponent {
  constructor(private authService: AuthService) {}
  public registerForm = new FormGroup({
    username: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
  });

  public errorMessage: string | null = null;

  public onSubmit(): void {
    const { username, email, password } = this.registerForm.value;
    if (username && email && password) {
      this.authService.register(username, email, password).subscribe({
        next: response => {
          this.errorMessage = null; 
          localStorage.setItem('IsLogged', "true");
        },
        error: error => {
          this.errorMessage = error.message.error.message || 'Registration failed';
        }
      });
    }
  }
}
