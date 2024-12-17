import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login-form.component.html',
  styleUrl: './login-form.component.css',
})
export class LoginFormComponent {
  errorMessage: string | null = null;
  public loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });
  onSubmit(): void {
    const { email, password } = this.loginForm.value;
    if (email && password) {
      console.log('Login form submitted:', email, password);
    }
  }
 

  googleLogin(): void {
    const params = new URLSearchParams({
      client_id: '801213326678-mn4hso84qjbvivaheubj0rrjint53bso.apps.googleusercontent.com',
      redirect_uri: 'http://localhost:4200/signin-google',
      scope: 'openid email',
      response_type: 'code',
      response_mode: 'query',
      state: 'fooabr',
      nonce: 'sjpbz260ini'
    });
  
    const googleAuthUrl = `https://accounts.google.com/o/oauth2/v2/auth?${params.toString()}`;
    window.location.href = googleAuthUrl;
  }
}