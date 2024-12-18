import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from './../environments/environment';
import { CookieService } from 'ngx-cookie-service';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private cookieService: CookieService) {}

  public register(
    username: string,
    email: string,
    password: string
  ): Observable<any> {
    return this.http
      .post(`${environment.AuthApiUrl}/Authentication/register`, {
        username,
        email,
        password,
      })
      .pipe(
        tap((response: any) => {
          console.log('Registration successful:', response);
        }),
        catchError((error) => {
          let errorMessage = 'Unknown error!';
          if (error.error instanceof ErrorEvent) {
            errorMessage = `Error: ${error.error.message}`;
          } else {
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
          }
          console.error(error);
          return throwError(() => ({ message: error }));
        })
      );
  }

  public login(
    email: string,
    password: string,
    rememberMe: boolean
  ): Observable<any> {
    return this.http
      .post(`${environment.AuthApiUrl}/Authentication/login`, {
        email,
        password,
        rememberMe
      })
      .pipe(
        tap((response: any) => {
          console.log('Login successful:', response);
          this.cookieService.set('YourAppCookie', response.token, rememberMe ? 30 : 1, '/');
        }),
        catchError((error) => {
          let errorMessage = 'Unknown error!';
          if (error.error instanceof ErrorEvent) {
            errorMessage = `Error: ${error.error.message}`;
          } else {
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
          }
          console.error(error);
          return throwError(() => ({ message: error }));
        })
      );
  }

  logout() {
    this.cookieService.delete('YourAppCookie', '/');
    console.log('Logout successful!');
    // return this.http.post('https://your-api-endpoint/authentication/logout', {})
    //   .subscribe(() => {
        
    //   });
  }

  isLoggedIn(): boolean {
    return this.cookieService.check('YourAppCookie');
  }
}
