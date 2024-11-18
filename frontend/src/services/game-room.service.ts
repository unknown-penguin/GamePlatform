import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

export interface GameRoom {
  id: number;
  name: string;
  players: number;
}

@Injectable({
  providedIn: 'root'
})
export class GameRoomService {

  constructor(private http: HttpClient) {}

  public getGamesRooms(gameId: number): Observable<GameRoom[]> {
    return this.http.get<GameRoom[]>(`http://46.173.140.22:5049/GameRoom/${gameId}`).pipe(
      catchError(error => {
        console.error('Error fetching games rooms:', error);
        return of([]);
      })
    );
  }
}