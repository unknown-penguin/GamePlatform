import { Component, OnInit, Input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { GameRoomService, GameRoom } from '../../../services/game-room.service';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-gameroomlist',
  standalone: true,
  imports: [MatIconModule, CommonModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatListModule],
  templateUrl: './gameroomlist.component.html',
  styleUrls: ['./gameroomlist.component.css']
})
export class GameroomlistComponent implements OnInit {
  gameId!: number;
  gameRooms: GameRoom[] = [];
  errorMessage: string | null = null;

  constructor(
    private gameRoomService: GameRoomService) {}

  ngOnInit() {
    const url = window.location.href;
    this.gameId = parseInt(url.split('/').pop() || '0', 10);
    this.fetchGameRooms();
  }

  fetchGameRooms() {
    console.log('Fetching game rooms for game', this.gameId);
    this.gameRoomService.getGamesRooms(this.gameId)
      .pipe(
        catchError(() => {
          this.errorMessage = 'Failed to load game rooms';
          return of<GameRoom[]>([]);
        })
      )
      .subscribe((rooms: GameRoom[]) => this.gameRooms = rooms);
  }
}