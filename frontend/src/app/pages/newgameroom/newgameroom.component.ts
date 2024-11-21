import { Component} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
@Component({
  selector: 'app-newgameroom',
  standalone: true,
  imports: [FormsModule, MatFormFieldModule, MatInputModule, MatButtonModule],
  templateUrl: './newgameroom.component.html',
  styleUrls: ['./newgameroom.component.css']
})
export class NewgameroomComponent {
  gameRoom = { name: '', players: 0 };
  errorMessage: string | null = null;
  constructor(private router: Router) {}
  createGameRoom() {
    if (!this.gameRoom.name || this.gameRoom.players <= 0) {
      this.errorMessage = 'Please provide valid room details.';
    } else {
      this.errorMessage = null;
      // Logic to create game room
    }
  }
  ReturnToPreviousPage() {
    this.router.navigate([`/games/${this.gameRoom.name}`]);
  }
}
