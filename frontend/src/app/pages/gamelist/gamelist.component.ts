import { Component, OnInit } from '@angular/core';
import { GameService } from '../../../services/game.service';
import { Game } from '../../models/Game';
import { CommonModule } from '@angular/common';
import { catchError } from 'rxjs/operators';
import { of, firstValueFrom } from 'rxjs';
import { GamecardComponent } from '../../gamecard/gamecard.component';

@Component({
  selector: 'app-gamelist',
  standalone: true,
  imports: [CommonModule, GamecardComponent],
  templateUrl: './gamelist.component.html',
  styleUrls: ['./gamelist.component.css'],
})
export class GamelistComponent implements OnInit {
  games: Game[] = [];
  errorMessage: string | null = null;
  constructor(private gameService: GameService) {}

  ngOnInit() {
    this.fetchGames();
  }

  fetchGames() {
    this.gameService
      .fetchGames()
      .pipe(
        catchError((error) => {
          this.errorMessage = 'Failed to load games';
          return of([]);
        })
      )
      .subscribe((games) => (this.games = games));
  }
  async AddGame() {
    // Create a new game object
    const game: Game = {
      id: 0,
      name: 'New Game',
      description: 'Description',
      image: 'https://via.placeholder.com/150',
    };
    try {
      const result = await firstValueFrom(
        this.gameService.AddGame(game).pipe(
          catchError((error) => {
            this.errorMessage = 'Failed to add game';
            return of(null);
          })
        )
      );
    } catch (error) {
      this.errorMessage = 'Failed to add game';
    }
  }
}
