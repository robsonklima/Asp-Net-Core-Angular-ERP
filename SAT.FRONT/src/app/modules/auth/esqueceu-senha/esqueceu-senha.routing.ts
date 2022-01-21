import { Route } from '@angular/router';
import { EsqueceuSenhaComponent } from 'app/modules/auth/esqueceu-senha/esqueceu-senha.component';

export const esqueceuSenhaRoutes: Route[] = [
    {
        path     : '',
        component: EsqueceuSenhaComponent
    }
];
