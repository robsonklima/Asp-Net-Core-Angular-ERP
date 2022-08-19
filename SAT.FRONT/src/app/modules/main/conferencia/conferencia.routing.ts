import { Route } from '@angular/router';
import { ConferenciaFormComponent } from './conferencia-form/conferencia-form.component';
import { ConferenciaListaComponent } from './conferencia-lista/conferencia-lista.component';
import { ConferenciaSalaComponent } from './conferencia-sala/conferencia-sala.component';

export const conferenciaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path     : 'lista',
        component: ConferenciaListaComponent
    },
    {
        path     : 'form',
        component: ConferenciaFormComponent
    },
    {
        path     : 'sala/:codConferencia',
        component: ConferenciaSalaComponent
    }
];
