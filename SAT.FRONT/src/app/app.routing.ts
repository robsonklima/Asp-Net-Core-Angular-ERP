import { Route } from '@angular/router';
import { AuthGuard } from 'app/core/guards/auth.guard';
import { NoAuthGuard } from 'app/core/guards/noAuth.guard';
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
            { path: 'esqueceu-senha', loadChildren: () => import('app/modules/auth/esqueceu-senha/esqueceu-senha.module').then(m => m.EsqueceuSenhaModule) },
            { path: 'confirmacao-nova-senha', loadChildren: () => import('app/modules/auth/confirmacao-nova-senha/confirmacao-nova-senha.module').then(m => m.ConfirmacaoNovaSenhaModule) },
            { path: 'sign-in', loadChildren: () => import('app/modules/auth/sign-in/sign-in.module').then(m => m.AuthSignInModule) },
            { path: 'sign-up', loadChildren: () => import('app/modules/auth/sign-up/sign-up.module').then(m => m.AuthSignUpModule) },
            { path: 'orcamento-aprovacao', loadChildren: () => import('app/modules/guest/orcamento-aprovacao/orcamento-aprovacao.module').then(m => m.OrcamentoAprovacaoModule) }
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
            { path: 'ordem-servico', loadChildren: () => import('app/modules/main/ordem-servico/ordem-servico.module').then(m => m.OrdemServicoModule) },
            { path: 'relatorio-atendimento', loadChildren: () => import('app/modules/main/relatorio-atendimento/relatorio-atendimento.module').then(m => m.RelatorioAtendimentoModule) },
            { path: 'acao', loadChildren: () => import('app/modules/main/cadastro/acao/acao.module').then(m => m.AcaoModule) },
            { path: 'autorizada', loadChildren: () => import('app/modules/main/cadastro/autorizada/autorizada.module').then(m => m.AutorizadaModule) },
            { path: 'cidade', loadChildren: () => import('app/modules/main/cadastro/cidade/cidade.module').then(m => m.CidadeModule) },
            { path: 'cliente', loadChildren: () => import('app/modules/main/cadastro/cliente/cliente.module').then(m => m.ClienteModule) },
            { path: 'contrato', loadChildren: () => import('app/modules/main/cadastro/contrato/contrato.module').then(m => m.ContratoModule) },
            { path: 'defeito', loadChildren: () => import('app/modules/main/cadastro/defeito/defeito.module').then(m => m.DefeitoModule) },
            { path: 'equipamento', loadChildren: () => import('app/modules/main/cadastro/equipamento/equipamento.module').then(m => m.EquipamentoModule) },
            { path: 'equipamento-contrato', loadChildren: () => import('app/modules/main/cadastro/equipamento-contrato/equipamento-contrato.module').then(m => m.EquipamentoContratoModule) },
            { path: 'filial', loadChildren: () => import('app/modules/main/cadastro/filial/filial.module').then(m => m.FilialModule) },
            { path: 'grupo-equipamento', loadChildren: () => import('app/modules/main/cadastro/grupo-equipamento/grupo-equipamento.module').then(m => m.GrupoEquipamentoModule) },
            { path: 'tipo-equipamento', loadChildren: () => import('app/modules/main/cadastro/tipo-equipamento/tipo-equipamento.module').then(m => m.TipoEquipamentoModule) },
            { path: 'cliente-bancada', loadChildren: () => import('app/modules/main/cadastro/cliente-bancada/cliente-bancada.module').then(m => m.ClienteBancadaModule) },
            { path: 'acao-causa', loadChildren: () => import('app/modules/main/cadastro/acao-causa/acao-causa.module').then(m => m.AcaoCausaModule) },
            { path: 'defeito-causa', loadChildren: () => import('app/modules/main/cadastro/defeito-causa/defeito-causa.module').then(m => m.DefeitoCausaModule) },
            { path: 'equipamento-modulo', loadChildren: () => import('app/modules/main/cadastro/equipamento-modulo/equipamento-modulo.module').then(m => m.EquipamentoModuloModule) },
            { path: 'cliente-peca', loadChildren: () => import('app/modules/main/cadastro/cliente-peca/cliente-peca.module').then(m => m.ClientePecaModule) },
            { path: 'cliente-peca-generica', loadChildren: () => import('app/modules/main/cadastro/cliente-peca-generica/cliente-peca-generica.module').then(m => m.ClientePecaGenericaModule) },
            { path: 'formas-pagamento', loadChildren: () => import('app/modules/main/cadastro/formas-pagamento/formas-pagamento.module').then(m => m.FormasPagamentoModule) },
            { path: 'local-atendimento', loadChildren: () => import('app/modules/main/cadastro/local-atendimento/local-atendimento.module').then(m => m.LocalAtendimentoModule) },
            { path: 'feriado', loadChildren: () => import('app/modules/main/cadastro/feriado/feriado.module').then(m => m.FeriadoModule) },
            { path: 'lider-tecnico', loadChildren: () => import('app/modules/main/cadastro/lider-tecnico/lider-tecnico.module').then(m => m.LiderTecnicoModule) },
            { path: 'ferramenta-tecnico', loadChildren: () => import('app/modules/main/cadastro/ferramenta-tecnico/ferramenta-tecnico.module').then(m => m.FerramentaTecnicoModule) },
            { path: 'peca', loadChildren: () => import('app/modules/main/cadastro/peca/peca.module').then(m => m.PecaModule) },
            { path: 'regiao', loadChildren: () => import('app/modules/main/cadastro/regiao/regiao.module').then(m => m.RegiaoModule) },
            { path: 'regiao-autorizada', loadChildren: () => import('app/modules/main/cadastro/regiao-autorizada/regiao-autorizada.module').then(m => m.RegiaoAutorizadaModule) },
            { path: 'ans', loadChildren: () => import('app/modules/main/cadastro/ans/ans.module').then(m => m.ANSModule) },
            { path: 'tecnico', loadChildren: () => import('app/modules/main/cadastro/tecnico/tecnico.module').then(m => m.TecnicoModule) },
            { path: 'transportadora', loadChildren: () => import('app/modules/main/cadastro/usuario/usuario.module').then(m => m.UsuarioModule) },
            { path: 'usuario', loadChildren: () => import('app/modules/main/cadastro/usuario/usuario.module').then(m => m.UsuarioModule) },
            { path: 'default', loadChildren: () => import('app/modules/main/default/default.module').then(m => m.DefaultModule) },
            { path: 'dashboard', loadChildren: () => import('app/modules/main/dashboard/dashboard.module').then(m => m.DashboardModule) },
            { path: 'agenda-tecnico', loadChildren: () => import('app/modules/main/agenda-tecnico/agenda-tecnico.module').then(m => m.AgendaTecnicoModule) },
            { path: 'configuracoes', loadChildren: () => import('app/modules/main/configuracoes/configuracoes.module').then(m => m.ConfiguracoesModule) },
            { path: 'despesa', loadChildren: () => import('app/modules/main/despesa/despesa.module').then(m => m.DespesaModule) },
            { path: 'ponto', loadChildren: () => import('app/modules/main/ponto/ponto.module').then(m => m.PontoModule) },
            { path: 'instalacao', loadChildren: () => import('app/modules/main/instalacao/instalacao.module').then(m => m.InstalacaoModule) },
            { path: 'dialog', loadChildren: () => import('app/modules/main/dialog/dialog.module').then(m => m.DialogModule) },
            { path: 'filtros', loadChildren: () => import('app/modules/main/filtros/filtro.module').then(m => m.FiltroModule) },
            { path: 'docs', loadChildren: () => import('app/modules/main/docs/docs.module').then(m => m.DocsModule) },
            { path: 'orcamento', loadChildren: () => import('app/modules/main/orcamento/orcamento.module').then(m => m.OrcamentoModule) },
            { path: 'conferencia', loadChildren: () => import('app/modules/main/conferencia/conferencia.module').then(m => m.ConferenciaModule) },
            { path: 'tecnico-plantao', loadChildren: () => import('app/modules/main/tecnico-plantao/tecnico-plantao.module').then(m => m.TecnicoPlantaoModule) },
            { path: 'importacao', loadChildren: () => import('app/modules/main/importacao/importacao.module').then(m => m.ImportacaoModule) },
            { path: 'ticket', loadChildren: () => import('app/modules/main/ticket/ticket.module').then(m => m.TicketModule) },
            { path: 'frota-tecnico', loadChildren: () => import('app/modules/main/frota-tecnicos/frota-tecnicos.module').then(m => m.FrotaTecnicosModule) },
            { path: 'mensagem', loadChildren: () => import('app/modules/main/mensagem/mensagem.module').then(m => m.MensagemModule) },
            { path: 'suporte-stn', loadChildren: () => import('app/modules/main/suporte-stn/suporte-stn.module').then(m => m.SuporteSTNModule) },
            { path: 'laboratorio', loadChildren: () => import('app/modules/main/laboratorio/laboratorio.module').then(m => m.LaboratorioModule) },
            { path: 'contato', loadChildren: () => import('app/modules/main/contato/contato.module').then(m => m.ContatoModule) },
            { path: 'partes-pecas', loadChildren: () => import('app/modules/main/partes-pecas/partes-pecas.module').then(m => m.PartesPecasModule) },
            { path: 'perfil', loadChildren: () => import('app/modules/main/cadastro/perfil/perfil.module').then(m => m.PerfilModule) },

            // Catch all errors
            { path: '404-not-found', pathMatch: 'full', loadChildren: () => import('app/modules/main/erro/erro-404/erro-404.module').then(m => m.Erro404Module) },
            { path: '403-forbidden', pathMatch: 'full', loadChildren: () => import('app/modules/main/erro/erro-403/erro-403.module').then(m => m.Erro403Module) },
            { path: '500-internal-server-error', pathMatch: 'full', loadChildren: () => import('app/modules/main/erro/erro-500/erro-500.module').then(m => m.Erro500Module) },
            { path: '0-connection-refused', pathMatch: 'full', loadChildren: () => import('app/modules/main/erro/erro-0/erro-0.module').then(m => m.Erro0Module) },
            { path: '503-service-unavailable', pathMatch: 'full', loadChildren: () => import('app/modules/main/erro/erro-503/erro-503.module').then(m => m.Erro503Module) },
            { path: '**', redirectTo: '404-not-found' }
        ]
    }
];
