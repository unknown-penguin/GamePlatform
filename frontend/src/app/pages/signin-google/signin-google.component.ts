import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-signin-google',
  standalone: true,
  imports: [],
  templateUrl: './signin-google.component.html',
  styleUrl: './signin-google.component.css'
})
export class SigninGoogleComponent implements OnInit {
  recivedCode: string = ''; 
  ngOnInit(): void {
    let code = window.location.href.split('&').find((param) => param.startsWith('code='));
    console.log('code:', code);
    if (code) {
      this.recivedCode = code;
    }
  }
}
