import { Route } from '@angular/router';
import { DuasEtapasComponent } from './docs/autenticacao/duas-etapas/duas-etapas.component';
import { LoginComponent } from './docs/autenticacao/login/login.component';
import { DocsComponent } from './docs/docs.component';
import { IntroducaoComponent } from './docs/inicio/introducao/introducao.component';
import { ExportacaoComponent } from './docs/ordem-servico/exportacao/exportacao.component';
import { FiltragemComponent } from './docs/ordem-servico/filtragem/filtragem.component';
import { ListagemComponent } from './docs/ordem-servico/listagem/listagem.component';
import { NovoComponent } from './docs/ordem-servico/novo/novo.component';
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
                    },
                    {
                        path     : 'duas-etapas',
                        component: DuasEtapasComponent
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
                    },
                    {
                        path     : 'exportacao',
                        component: ExportacaoComponent
                    },
                    {
                        path     : 'novo',
                        component: NovoComponent
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