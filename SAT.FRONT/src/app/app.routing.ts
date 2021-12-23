import { Route } from '@angular/router';
import { AuthGuard } from 'app/core/auth/guards/auth.guard';
import { NoAuthGuard } from 'app/core/auth/guards/noAuth.guard';
import { LayoutComponent } from 'app/layout/layout.component';
import { InitialDataResolver } from 'app/app.resolvers';

export const appRoutes: Route[] = [
    { path: '', pathMatch: 'full', redirectTo: 'default' },
    { path: 'signed-in-redirect', pathMatch: 'full', redirectTo: 'default' },

    // Auth routes for guests
    {
        path: '',
        canActivate: [NoAuthGuard],
        canActivateChild: [NoAuthGuard],
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'confirmation-required', loadChildren: () => import('app/modules/auth/confirmation-required/confirmation-required.module').then(m => m.AuthConfirmationRequiredModule) },
            { path: 'confirmation-submit', loadChildren: () => import('app/modules/auth/confirmation-submit/confirmation-submit.module').then(m => m.AuthConfirmationSubmitModule) },
            { path: 'forgot-password', loadChildren: () => import('app/modules/auth/forgot-password/forgot-password.module').then(m => m.AuthForgotPasswordModule) },
            { path: 'reset-password', loadChildren: () => import('app/modules/auth/reset-password/reset-password.module').then(m => m.AuthResetPasswordModule) },
            { path: 'sign-in', loadChildren: () => import('app/modules/auth/sign-in/sign-in.module').then(m => m.AuthSignInModule) },
            { path: 'sign-up', loadChildren: () => import('app/modules/auth/sign-up/sign-up.module').then(m => m.AuthSignUpModule) }
        ]
    },

    // Auth routes for authenticated users
    {
        path: '',
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'sign-out', loadChildren: () => import('app/modules/auth/sign-out/sign-out.module').then(m => m.AuthSignOutModule) },
            { path: 'unlock-session', loadChildren: () => import('app/modules/auth/unlock-session/unlock-session.module').then(m => m.AuthUnlockSessionModule) }
        ]
    },

    // Landing routes
    {
        path: '',
        component: LayoutComponent,
        data: {
            layout: 'empty'
        },
        children: [
            { path: 'home', loadChildren: () => import('app/modules/landing/home/home.module').then(m => m.LandingHomeModule) },
        ]
    },

    // App routes
    {
        path: '',
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
        component: LayoutComponent,
        resolve: {
            initialData: InitialDataResolver,
        },
        children: [
            // Main
            { path: 'ordem-servico', loadChildren: () => import('app/modules/main/ordem-servico/ordem-servico.module').then(m => m.OrdemServicoModule) },
            { path: 'relatorio-atendimento', loadChildren: () => import('app/modules/main/relatorio-atendimento/relatorio-atendimento.module').then(m => m.RelatorioAtendimentoModule) },
            { path: 'regiao', loadChildren: () => import('app/modules/main/cadastro/regiao/regiao.module').then(m => m.RegiaoModule) },
            { path: 'autorizada', loadChildren: () => import('app/modules/main/cadastro/autorizada/autorizada.module').then(m => m.AutorizadaModule) },
            { path: 'grupo-equipamento', loadChildren: () => import('app/modules/main/cadastro/grupo-equipamento/grupo-equipamento.module').then(m => m.GrupoEquipamentoModule) },
            { path: 'local-atendimento', loadChildren: () => import('app/modules/main/cadastro/local-atendimento/local-atendimento.module').then(m => m.LocalAtendimentoModule) },
            { path: 'equipamento-contrato', loadChildren: () => import('app/modules/main/cadastro/equipamento-contrato/equipamento-contrato.module').then(m => m.EquipamentoContratoModule) },
            { path: 'equipamento', loadChildren: () => import('app/modules/main/cadastro/equipamento/equipamento.module').then(m => m.EquipamentoModule) },
            { path: 'usuario', loadChildren: () => import('app/modules/main/cadastro/usuario/usuario.module').then(m => m.UsuarioModule) },
            { path: 'tecnico', loadChildren: () => import('app/modules/main/cadastro/tecnico/tecnico.module').then(m => m.TecnicoModule) },
            { path: 'default', loadChildren: () => import('app/modules/main/default/default.module').then(m => m.DefaultModule) },
            { path: 'dashboard', loadChildren: () => import('app/modules/main/dashboard/dashboard.module').then(m => m.DashboardModule) },
            { path: 'docs', loadChildren: () => import('app/modules/main/docs/docs.module').then(m => m.DocsModule) },
            { path: 'agenda-tecnico', loadChildren: () => import('app/modules/main/agenda-tecnico/agenda-tecnico.module').then(m => m.AgendaTecnicoModule) },
            { path: 'configuracoes', loadChildren: () => import('app/modules/main/configuracoes/configuracoes.module').then(m => m.ConfiguracoesModule) },
            { path: 'peca', loadChildren: () => import('app/modules/main/cadastro/peca/peca.module').then(m => m.PecaModule) },
            { path: 'defeito', loadChildren: () => import('app/modules/main/cadastro/defeito/defeito.module').then(m => m.DefeitoModule) },
            { path: 'cidade', loadChildren: () => import('app/modules/main/cadastro/cidade/cidade.module').then(m => m.CidadeModule) },
            { path: 'contrato', loadChildren: () => import('app/modules/main/cadastro/contrato/contrato.module').then(m => m.ContratoModule) },
            { path: 'despesa', loadChildren: () => import('app/modules/main/despesa/despesa.module').then(m => m.DespesaModule) },
            { path: 'ponto', loadChildren: () => import('app/modules/main/ponto/ponto.module').then(m => m.PontoModule) },
            { path: 'instalacao', loadChildren: () => import('app/modules/main/instalacao/instalacao.module').then(m => m.InstalacaoModule) },
            { path: 'dialog', loadChildren: () => import('app/modules/main/dialog/dialog.module').then(m => m.DialogModule) },
            { path: 'filtros', loadChildren: () => import('app/modules/main/filtros/filtro.module').then(m => m.FiltroModule) },

            // 404 & Catch all
            { path: '404-not-found', pathMatch: 'full', loadChildren: () => import('app/modules/main/error/error-404/error-404.module').then(m => m.Error404Module) },
            { path: '**', redirectTo: '404-not-found' }
        ]
    }
];
