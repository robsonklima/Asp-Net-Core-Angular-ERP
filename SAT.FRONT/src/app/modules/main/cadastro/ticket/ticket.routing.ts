import { Route } from '@angular/router';
import { TicketDetalheComponent } from './ticket-detalhe/ticket-detalhe.component';
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
        path: 'detalhe/:codTicket',
        component: TicketDetalheComponent,
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
