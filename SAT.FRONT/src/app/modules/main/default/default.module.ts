import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { DefaultComponent } from './default.component';
import { defaultRoutes } from './default.routing';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatMenuModule } from '@angular/material/menu';
import { MatTabsModule } from '@angular/material/tabs';
import { MonitoramentoSatComponent } from '../dashboard/monitoramento-sat/monitoramento-sat.component';
import { DashboardModule } from '../dashboard/dashboard.module';

@NgModule({
    declarations: [
        DefaultComponent,
        
    ],
    imports: [
        RouterModule.forChild(defaultRoutes),
        MatButtonModule,
        MatIconModule,
        SharedModule,
        TranslocoModule,
        MatButtonToggleModule,
        MatMenuModule,
        MatTabsModule,
    //    MonitoramentoSatComponent
        DashboardModule
    ]
})
export class DefaultModule {
}