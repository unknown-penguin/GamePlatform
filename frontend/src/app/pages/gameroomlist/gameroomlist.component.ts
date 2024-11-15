import { Component, OnInit } from '@angular/core';
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
  gameRooms: GameRoom[] = [];
  errorMessage: string | null = null;

  constructor(private gameRoomService: GameRoomService) {}

  ngOnInit() {
    this.fetchGameRooms();
  }

  fetchGameRooms() {
    this.gameRoomService.fetchGameRooms()
      .pipe(
        catchError(error => {
          this.errorMessage = 'Failed to load game rooms';
          return of([]);
        })
      )
      .subscribe(rooms => this.gameRooms = rooms);
  }
}