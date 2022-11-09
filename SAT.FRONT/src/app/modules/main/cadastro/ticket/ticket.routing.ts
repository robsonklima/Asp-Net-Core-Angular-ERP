import { Route } from '@angular/router';
import { TicketFormComponent } from './ticket-form/ticket-form.component';
import { TicketListaComponent } from './ticket-lista/ticket-lista.component';


export const ticketRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: TicketListaComponent,
    },
    {
        path: 'form',
        component: TicketFormComponent,
    },
    {
        path: 'form/:codTicket',
        component: TicketFormComponent,
    }
];
