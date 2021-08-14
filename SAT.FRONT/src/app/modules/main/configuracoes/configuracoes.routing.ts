import { Route } from '@angular/router';
import { ConfiguracoesComponent } from './configuracoes.component';


export const configuracoesRoutes: Route[] = [
    {
        path     : ':codUsuario',
        component: ConfiguracoesComponent
    }
];
