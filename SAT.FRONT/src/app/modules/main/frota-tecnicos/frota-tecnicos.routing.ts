import { Route } from '@angular/router';
import { AuditoriaFormComponent } from './auditoria/auditoria-form/auditoria-form.component';
import { AuditoriaLayoutComponent } from './auditoria/auditoria-layout/auditoria-layout.component';
import { AuditoriaListaComponent } from './auditoria/auditoria-lista/auditoria-lista.component';
import { CreditoCartaoListaComponent } from './creditos-cartao/credito-cartao-lista/credito-cartao-lista.component';
import { PedidosCreditoComponent } from './pedidos-credito/pedidos-credito.component';
import { ValoresCombustivelFormComponent } from './valores-combustivel/valores-combustivel-form/valores-combustivel-form.component';
import { ValoresCombustivelListaComponent } from './valores-combustivel/valores-combustivel-lista/valores-combustivel-lista.component';
import { CartaoCombustivelListaComponent } from './cartoes-combustivel/cartao-combustivel-lista/cartao-combustivel-lista.component';
import { CartaoCombustivelFormComponent } from './cartoes-combustivel/cartao-combustivel-form/cartao-combustivel-form.component';
import { CartaoCombustivelDetalheComponent } from './cartoes-combustivel/cartao-combustivel-detalhe/cartao-combustivel-detalhe.component';
import { TransacoesCartaoListaComponent } from './transacoes-cartao/transacoes-cartao-lista/transacoes-cartao-lista.component';

export const frotaTecnicosRoutes: Route[] = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'lista'
    },
    {
        path: 'lista',
        component: AuditoriaListaComponent
    },
    {
        path: 'auditoria/form',
        component: AuditoriaFormComponent
    },
    {
        path: 'auditoria/:codAuditoria',
        component: AuditoriaLayoutComponent
    },
    {
        path: 'valores-combustivel',
        component: ValoresCombustivelListaComponent
    },
    {
        path: 'valores-combustivel/form',
        component: ValoresCombustivelFormComponent
    },
    {
        path: 'valores-combustivel/form/:codDespesaConfiguracaoCombustivel',
        component: ValoresCombustivelFormComponent
    },
    {
        path: 'pedidos-credito',
        component: PedidosCreditoComponent
    },
    {
        path: 'creditos-cartao',
        component: CreditoCartaoListaComponent
    },
    {
        path: 'cartoes-combustivel',
        component: CartaoCombustivelListaComponent
    },
    {
        path: 'cartoes-combustivel/form',
        component: CartaoCombustivelFormComponent
    },
    {
        path: 'cartoes-combustivel/form/:codDespesaCartaoCombustivel',
        component: CartaoCombustivelFormComponent
    },
    {
        path: 'cartoes-combustivel/detalhe/:codDespesaCartaoCombustivel',
        component: CartaoCombustivelDetalheComponent
    },
    {
        path: 'transacoes-cartao',
        component: TransacoesCartaoListaComponent
    }
];
