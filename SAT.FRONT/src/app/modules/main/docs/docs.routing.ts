import { Route } from '@angular/router';
import { DocsComponent } from './docs.component';
import { DocsFormComponent } from './docs-form/docs-form.component';
import { DocsDetalheComponent } from './docs-detalhe/docs-detalhe.component';

export const docsRoutes: Route[] = [
    {
        path: '',
        component: DocsComponent
    },
    {
        path: 'lista',
        component: DocsComponent,
    },
    {
        path: 'form',
        component: DocsFormComponent,
    },
    {
        path: 'form/:codDocumentoSistema',
        component: DocsFormComponent,
    },
    {
        path: 'detalhe/:codDocumentoSistema',
        component: DocsDetalheComponent,
    },
];