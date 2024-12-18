import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-signin-google',
  standalone: true,
  imports: [],
  templateUrl: './signin-google.component.html',
  styleUrl: './signin-google.component.css'
})
export class SigninGoogleComponent implements OnInit {
  receivedCode: string = ''; 
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    const params = new URLSearchParams(window.location.search);
    const code = params.get('code');
    if (code) {
      this.receivedCode = code;
      
      this.http.post('http://localhost:5115/Authentication/google-login', { code: this.receivedCode })
        .subscribe(response => {
          console.log('Google login response:', response);
        });
    }
  }
}
