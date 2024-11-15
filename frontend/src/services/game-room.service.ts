import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

export interface GameRoom {
  name: string;
  players: number;
}

@Injectable({
 providedIn: 'root'
})
export class GameRoomService {
  constructor() {}

  fetchGameRooms(): Observable<GameRoom[]> {
    // Simulate an API call
    return of([
      { name: 'Room 1', players: 5 },
      { name: 'Room 2', players: 3 },
      { name: 'Room 3', players: 8 }
    ]);
  }
}