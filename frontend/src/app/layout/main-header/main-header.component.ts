import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
@Component({
  selector: 'app-main-header',
  standalone: true,
  imports: [MatButtonModule, MatSidenavModule, MatToolbarModule, MatIconModule],
  templateUrl: './main-header.component.html',
  styleUrl: './main-header.component.css'
})
export class MainHeaderComponent {
  isPanelOpen = true;

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen;
  }
}
