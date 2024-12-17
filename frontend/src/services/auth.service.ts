import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from './../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }
 
  public register(username: string, email: string, password: string): Observable<any> {
    return this.http.post(`${environment.AuthApiUrl}/Authentication/register`, { username, email, password }).pipe(
      tap((response: any) => {
        console.log('Registration successful:', response);
      }),
      catchError(error => {
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
}
