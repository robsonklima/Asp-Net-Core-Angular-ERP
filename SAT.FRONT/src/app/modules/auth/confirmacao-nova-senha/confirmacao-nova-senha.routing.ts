import { Route } from '@angular/router';
import { ConfirmacaoNovaSenhaComponent } from 'app/modules/auth/confirmacao-nova-senha/confirmacao-nova-senha.component';

export const confirmacaoNovaSenhaRoutes: Route[] = [
    {
        path     : ':codRecuperaSenha',
        component: ConfirmacaoNovaSenhaComponent
    }
];
