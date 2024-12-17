import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { LoginFormComponent } from '../../login-form/login-form.component';
@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule,MatButtonModule, MatSidenavModule, MatToolbarModule, MatIconModule, LoginFormComponent],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent implements OnInit {
  isPanelOpen = true;
  IsLogged: boolean = false;

  ngOnInit() {
    console.log('IsLogged:', localStorage.getItem('IsLogged'));
    this.IsLogged = localStorage.getItem('IsLogged') == "true" ? true : false;
  }

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen;
  }
}
