import { Route } from '@angular/router';
import { PerfilFormComponent } from './perfil-form/perfil-form.component';
import { PerfilListaComponent } from './perfil-lista/perfil-lista.component';

export const perfilRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: PerfilListaComponent,
    },
    {
        path: 'form',
        component: PerfilFormComponent,
    },
    {
        path: 'form/:codPerfil/:codSetor',
        component: PerfilFormComponent,
    },
];
