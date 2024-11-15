import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Game } from '../../services/game.service';
@Component({
  selector: 'app-gamecard',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './gamecard.component.html',
  styleUrl: './gamecard.component.css'
})
export class GamecardComponent {
  @Input() game!: Game;
}
