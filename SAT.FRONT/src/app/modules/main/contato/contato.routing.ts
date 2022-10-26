import { Route } from '@angular/router';
import { ContatoListaComponent } from '../contato/contato-lista/contato-lista.component';

export const contatoRoutes: Route[] = [
    {
        path     : '',
        component: ContatoListaComponent
    }
];
