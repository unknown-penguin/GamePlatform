import { Routes } from '@angular/router';
import { GamelistComponent } from './pages/gamelist/gamelist.component';
import { GameroomlistComponent } from './pages/gameroomlist/gameroomlist.component';
import { NewgameroomComponent } from './pages/newgameroom/newgameroom.component';
export const routes: Routes = [{ path: '*', component: GamelistComponent},
    { path: 'games', component: GamelistComponent},
    { path: 'game/rooms/:name', component: GameroomlistComponent},
    { path: 'games/:name/new', component: NewgameroomComponent},
];
