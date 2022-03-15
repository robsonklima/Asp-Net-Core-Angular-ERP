import { Route } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { IndicadoresFiliaisDetalhadosComponent } from './indicadores-filiais-detalhados/indicadores-filiais-detalhados.component';

export const dashboardRoutes: Route[] = [
    {
        path     : '',
        component: DashboardComponent
    },
    {
        path     : 'indicadores-filiais-detalhados/:codFilial',
        component: IndicadoresFiliaisDetalhadosComponent
    }
];
