import { Route } from '@angular/router';
import { LoginComponent } from './docs/autenticacao/login/login.component';
import { DocsComponent } from './docs/docs.component';
import { IntroducaoComponent } from './docs/inicio/introducao/introducao.component';
import { FiltragemComponent } from './docs/ordem-servico/filtragem/filtragem.component';
import { ListagemComponent } from './docs/ordem-servico/listagem/listagem.component';
import { SuporteComponent } from './suporte/suporte.component';

export const ajudaRoutes: Route[] = [
    {
        path     : 'docs',
        component: DocsComponent,
        children : [
            {
                path      : '',
                pathMatch : 'full',
                redirectTo: 'inicio'
            },
            {
                path    : 'inicio',
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
            {
                path    : 'autenticacao',
                children: [
                    {
                        path      : '',
                        pathMatch : 'full',
                        redirectTo: 'login'
                    },
                    {
                        path     : 'login',
                        component: LoginComponent
                    }
                ]
            },
            {
                path    : 'ordem-servico',
                children: [
                    {
                        path      : '',
                        pathMatch : 'full',
                        redirectTo: 'listagem'
                    },
                    {
                        path     : 'listagem',
                        component: ListagemComponent
                    },
                    {
                        path     : 'filtro',
                        component: FiltragemComponent
                    }
                ]
            },
            {
                path    : 'suporte',
                children: [
                    {
                        path      : '',
                        pathMatch : 'full',
                        redirectTo: 'suporte-form'
                    },
                    {
                        path     : 'suporte-form',
                        component: SuporteComponent
                    }
                ]
            },
        ]
    }
];