import { Route } from '@angular/router';
import { AjudaFaqComponent } from './ajuda-faq/ajuda-faq.component';

export const ajudaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'ajuda-faq'
    },
    {
        path: 'ajuda-faq',
        component: AjudaFaqComponent
    },
    {
        path: 'ajuda-suporte',
        component: AjudaFaqComponent
    },
];