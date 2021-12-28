import { Route } from '@angular/router';
import { AjudaFaqComponent } from './ajuda-faq/ajuda-faq.component';
import { AjudaSuporteComponent } from './ajuda-suporte/ajuda-suporte.component';

export const ajudaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'faq'
    },
    {
        path: 'faq',
        component: AjudaFaqComponent
    },
    {
        path: 'suporte',
        component: AjudaSuporteComponent
    },
];