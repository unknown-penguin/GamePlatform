import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
export interface Game {
  id: number;
  name: string;
  description: string;
  image: string;
}

@Injectable({
 providedIn: 'root'
})
export class GameService {
  constructor(private http: HttpClient) {}

  public fetchGames(): Observable<Game[]> {
    return this.http.get<Game[]>('https://localhost/game').pipe(
      catchError(error => {
        console.error('Error fetching games:', error);
        return of([]); 
      })
    );
  }

  public AddGame(game: Game): Observable<Game | null> {
    return this.http.post<Game>('https://localhost/game', game).pipe(
      catchError(error => {
        console.error('Error adding game:', error);
        return of(null);
      })
    );
  }
}