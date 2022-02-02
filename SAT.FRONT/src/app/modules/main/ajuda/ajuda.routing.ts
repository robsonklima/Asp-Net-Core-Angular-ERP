import { Route } from '@angular/router';
import { IntroducaoComponent } from './docs/introducao/introducao.component';
import { DocsComponent } from './docs/docs.component';
import { SuporteComponent } from './suporte/suporte.component';
import { TutorialComponent } from './tutorial/tutorial.component';

export const ajudaRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'docs'
    },
    {
        path: 'suporte',
        component: SuporteComponent
    },
    {
        path: 'tutorial',
        component: TutorialComponent
    },
    {
        path    : 'docs',
        component: DocsComponent,
        children: [
            {
                path      : '',
                pathMatch : 'full',
                redirectTo: 'introducao'
            },
            {
                path     : 'introducao',
                component: IntroducaoComponent
            }
        ]
    },
];