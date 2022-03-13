import { Route } from '@angular/router';
import { VersoesComponent } from './inicio/introducao/versoes/versoes.component';
import { DocsComponent } from './docs.component';
import { IntroducaoComponent } from './inicio/introducao/introducao.component';
import { SuporteComponent } from './suporte/suporte.component';
import { OrdemServicoComponent } from './ordem-servico/ordem-servico.component';
import { AutenticacaoComponent } from './autenticacao/autenticacao.component';
import { AppTecnicosComponent } from './inicio/app-tecnicos/app-tecnicos.component';

export const docsRoutes: Route[] = [
    {
        path     : '',
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
                        redirectTo: 'introducao'
                    },
                    {
                        path     : 'introducao',
                        component: IntroducaoComponent
                    },
                    {
                        path     : 'versoes',
                        component: VersoesComponent
                    },
                    {
                        path     : 'app-tecnicos',
                        component: AppTecnicosComponent
                    }
                ]
            },
            {
                path    : 'autenticacao',
                children: [
                    {
                        path      : '',
                        pathMatch : 'full',
                        component: AutenticacaoComponent
                    }
                ]
            },
            {
                path    : 'ordem-servico',
                children: [
                    {
                        path      : '',
                        pathMatch : 'full',
                        component: OrdemServicoComponent
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