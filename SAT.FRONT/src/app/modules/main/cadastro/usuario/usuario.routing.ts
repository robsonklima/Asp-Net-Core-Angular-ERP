import { Route } from '@angular/router';
import { UsuarioFormComponent } from './usuario-form/usuario-form.component';
import { UsuarioListaComponent } from './usuario-lista/usuario-lista.component';

export const usuarioRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: UsuarioListaComponent,
    },
    {
        path: 'form',
        component: UsuarioFormComponent,
    },
    {
        path: 'form/:codUsuario',
        component: UsuarioFormComponent,
    },
];
