import { Route } from '@angular/router';
import { VersoesComponent } from './versoes/versoes.component';
import { DuasEtapasComponent } from './autenticacao/duas-etapas/duas-etapas.component';
import { LoginComponent } from './autenticacao/login/login.component';
import { DocsComponent } from './docs.component';
import { IntroducaoComponent } from './inicio/introducao/introducao.component';
import { ExportacaoComponent } from './ordem-servico/exportacao/exportacao.component';
import { FiltragemComponent } from './ordem-servico/filtragem/filtragem.component';
import { ListagemComponent } from './ordem-servico/listagem/listagem.component';
import { NovoComponent } from './ordem-servico/novo/novo.component';
import { SuporteComponent } from './suporte/suporte.component';

export const docsRoutes: Route[] = [
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
                    },
                    {
                        path     : 'versoes',
                        component: VersoesComponent
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
                path     : 'suporte',
                component: SuporteComponent
            }
        ]
    }
];