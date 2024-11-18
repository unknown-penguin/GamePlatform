import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [MatButtonModule, MatSidenavModule, MatToolbarModule, MatIconModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  isPanelOpen = true;

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen;
  }
}
