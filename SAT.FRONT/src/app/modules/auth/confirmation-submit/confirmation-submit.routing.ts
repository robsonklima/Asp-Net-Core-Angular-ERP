import { Route } from '@angular/router';
import { AuthConfirmationSubmitComponent } from 'app/modules/auth/confirmation-submit/confirmation-submit.component';

export const authConfirmationSubmitRoutes: Route[] = [
    {
        path     : ':codUsuarioDispositivo',
        component: AuthConfirmationSubmitComponent
    }
];
