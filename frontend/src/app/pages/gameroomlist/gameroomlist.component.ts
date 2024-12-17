import { Component, OnInit, Input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { GameRoomService, GameRoom } from '../../../services/game-room.service';
import { catchError } from 'rxjs/operators';
import { of, firstValueFrom } from 'rxjs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { GameService } from '../../../services/game.service';
import { Game } from '../../models/Game';
import { Router } from '@angular/router';
@Component({
  selector: 'app-gameroomlist',
  standalone: true,
  imports: [
    MatIconModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatListModule,
  ],
  templateUrl: './gameroomlist.component.html',
  styleUrls: ['./gameroomlist.component.css'],
})
export class GameroomlistComponent implements OnInit {
  selectedGame: Game | null = null;
  gameRooms: GameRoom[] = [];
  errorMessage: string | null = null;

  constructor(
    private gameRoomService: GameRoomService,
    private gameService: GameService,
    private router: Router
  ) {}

  async ngOnInit() {
    const gameName = this.extractGameNameFromUrl();
    
    if (!gameName) {
      this.errorMessage = 'Invalid game name in URL.';
      return;
    }
    try {
      this.selectedGame = await firstValueFrom(this.gameService.GetGame(gameName));
      if (!this.selectedGame) {
        this.errorMessage = `Game not found: ${gameName}`;
        return;
      }
      await this.fetchGameRooms(this.selectedGame.id);
    } catch (error) {
      this.errorMessage = `Failed to load game details: ${(error as Error).message}`;
    }
  }

  private extractGameNameFromUrl(): string {
    const url = window.location.href;
    return url.substring(url.lastIndexOf('/') + 1);
  }

  protected ReturnToPreviousPage() {
    this.router.navigate([`/games`]);
  }
  protected AddNewGameRoom() {
    this.router.navigate([`/games/${this.selectedGame?.name}/new`]);
  }
  private fetchGameRooms(gameId: number) {
    this.gameRoomService
      .getGamesRooms(gameId)
      .pipe(
        catchError(() => {
          this.errorMessage = `Failed to load game rooms ${gameId}`;
          return of<GameRoom[]>([]);
        })
      )
      .subscribe((rooms: GameRoom[]) => (this.gameRooms = rooms));
  }
}
