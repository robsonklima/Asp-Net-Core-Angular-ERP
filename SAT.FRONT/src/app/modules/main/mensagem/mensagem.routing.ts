import { Route } from '@angular/router';
import { MensagemTecnicoFormComponent } from './tecnico/mensagem-tecnico-form/mensagem-tecnico-form.component';
import { MensagemTecnicoListaComponent } from './tecnico/mensagem-tecnico-lista/mensagem-tecnico-lista.component';

export const mensagemRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'tecnico/lista'
    },
    {
        path: 'tecnico/lista',
        component: MensagemTecnicoListaComponent
    },
    {
        path: 'tecnico/form',
        component: MensagemTecnicoFormComponent
    }
];
