import { Routes } from '@angular/router';
import { GamelistComponent } from './pages/gamelist/gamelist.component';
import { GameroomlistComponent } from './pages/gameroomlist/gameroomlist.component';
export const routes: Routes = [ { path: 'games', component: GamelistComponent},
    { path: 'game/rooms/:name', component: GameroomlistComponent},
];
