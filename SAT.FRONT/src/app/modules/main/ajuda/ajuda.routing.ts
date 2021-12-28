import { Route } from '@angular/router';
import { AjudaFaqComponent } from './ajuda-faq/ajuda-faq.component';
import { AjudaSuporteComponent } from './ajuda-suporte/ajuda-suporte.component';
import { AjudaTutorialComponent } from './ajuda-tutorial/ajuda-tutorial.component';

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
    {
        path: 'tutorial',
        component: AjudaTutorialComponent
    },
];