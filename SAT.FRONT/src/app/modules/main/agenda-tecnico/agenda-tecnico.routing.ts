import { Route } from '@angular/router';
import { AgendaTecnicoComponent } from 'app/modules/main/agenda-tecnico/agenda-tecnico.component';
import { RoteiroMapaComponent } from './roteiro-mapa/roteiro-mapa.component';

export const agendaTecnicoRoutes: Route[] = [
    {
        path     : '',
        component: AgendaTecnicoComponent,
    },
    {
        path     : 'roteiro',
        component: RoteiroMapaComponent,
    }
];
