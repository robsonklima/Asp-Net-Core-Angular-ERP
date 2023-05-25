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
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FuseAlertModule } from '@fuse/components/alert';
import { NgApexchartsModule } from 'ng-apexcharts';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ServicosComponent } from './servicos/servicos.component';
import { ServidoresComponent } from './servidores/servidores.component';
import { DisponibilidadeComponent } from './disponibilidade/disponibilidade.component';
import { OciosidadeComponent } from './ociosidade/ociosidade.component';
import { UtilizacaoComponent } from './utilizacao/utilizacao.component';
import { LogDetalheComponent } from './log-detalhe/log-detalhe.component';
import { FuseCardModule } from '@fuse/components/card';
import { UtilizacaoUsuariosDialogComponent } from './utilizacao/utilizacao-usuarios-dialog/utilizacao-usuarios-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
// import { MatFormFieldModule } from '@angular/material/form-field';

@NgModule({
    declarations: [
        DefaultComponent,
        ServicosComponent,
        ServidoresComponent,
        DisponibilidadeComponent,
        OciosidadeComponent,
        UtilizacaoComponent,
        LogDetalheComponent,
        UtilizacaoUsuariosDialogComponent
    ],
    imports: [
        RouterModule.forChild(defaultRoutes),
        SharedModule,
        MatButtonModule,
        MatIconModule,
        TranslocoModule,
        MatButtonToggleModule,
        MatMenuModule,
        MatTabsModule,
        MatProgressSpinnerModule,
        FuseAlertModule,
        NgApexchartsModule,
        MatIconModule,
        MatTooltipModule,
        MatProgressBarModule,
        FuseCardModule,
        MatDialogModule
        // MatFormFieldModule
    ]
})
export class DefaultModule {
}