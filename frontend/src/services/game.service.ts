import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { environment } from './../environments/environment';
import { Game } from '../app/models/Game';

@Injectable({
 providedIn: 'root'
})
export class GameService {
  constructor(private http: HttpClient) {}

  public fetchGames(): Observable<Game[]> {
    return this.http.get<Game[]>(`${environment.GameApiUrl}/game`).pipe(
      catchError(error => {
        console.error('Error fetching games:', error);
        return of([]); 
      })
    );
  }
  public GetGame(gameId: number): Observable<Game>;
  public GetGame(gameIds: string): Observable<Game>;

  public GetGame(game: number | string): Observable<Game> {
    const url = typeof game === 'number' 
      ? `${environment.GameApiUrl}/game/${game}` 
      : `${environment.GameApiUrl}/game/byname/${game}`;
      
    return this.http.get<Game>(url).pipe(
      catchError(error => {
        console.error('Error fetching game:', error);
        return of(); 
      })
    );
  }
  

  public AddGame(game: Game): Observable<Game | null> {
    return this.http.post<Game>(`${environment.GameApiUrl}/game`, game).pipe(
      catchError(error => {
        console.error('Error adding game:', error);
        return of(null);
      })
    );
  }
}