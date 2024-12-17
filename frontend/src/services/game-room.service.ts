import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { environment } from './../environments/environment';
import { GameService } from './game.service';
export interface GameRoom {
  id: number;
  name: string;
  players: number;
}

@Injectable({
  providedIn: 'root'
})
export class GameRoomService {

  constructor(private http: HttpClient, private gameService: GameService) {}

  public getGamesRooms(gameId: number): Observable<GameRoom[]> {
    return this.http.get<GameRoom[]>(`${environment.GameApiUrl}/GameRoom/gamerooms/${gameId}`).pipe(
      catchError(error => {
        console.error('Error fetching games rooms:', error);
        return of([]);
      })
    );
  }
}