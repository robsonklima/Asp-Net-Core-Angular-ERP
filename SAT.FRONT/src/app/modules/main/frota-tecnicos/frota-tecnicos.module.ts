import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatMenuModule } from '@angular/material/menu';
import { FiltroModule } from '../filtros/filtro.module';
import { FormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSortModule } from '@angular/material/sort';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { AuditoriaFormComponent } from './auditoria/auditoria-form/auditoria-form.component';
import { AuditoriaListaComponent } from './auditoria/auditoria-lista/auditoria-lista.component';
import { AuditoriaFiltroComponent } from './auditoria/auditoria-filtro/auditoria-filtro.component';
import { ValoresCombustivelListaComponent } from './valores-combustivel/valores-combustivel-lista/valores-combustivel-lista.component';
import { ValoresCombustivelFormComponent } from './valores-combustivel/valores-combustivel-form/valores-combustivel-form.component';
import { ValoresCombustivelFiltroComponent } from './valores-combustivel/valores-combustivel-filtro/valores-combustivel-filtro.component';
import { frotaTecnicosRoutes } from './frota-tecnicos.routing';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AuditoriaLayoutComponent } from './auditoria/auditoria-layout/auditoria-layout.component';
import { AuditoriaDetalhesComponent } from './auditoria/auditoria-detalhes/auditoria-detalhes.component';
import { MatTabsModule } from '@angular/material/tabs';
import { FuseCardModule } from '@fuse/components/card';
import { AuditoriaFotoComponent } from './auditoria/auditoria-foto/auditoria-foto.component';
import { AuditoriaAcessoriosComponent } from './auditoria/auditoria-acessorios/auditoria-acessorios.component';
import { MatTableModule } from '@angular/material/table';
import { NgxMaskModule } from 'ngx-mask/lib/ngx-mask.module';
import { AuditoriaUtilizacaoComponent } from './auditoria/auditoria-utilizacao/auditoria-utilizacao.component';


@NgModule({
  declarations: [
    AuditoriaListaComponent,
    AuditoriaFormComponent,
    AuditoriaFiltroComponent,
    AuditoriaLayoutComponent,
    AuditoriaDetalhesComponent,
    AuditoriaFotoComponent,
    AuditoriaAcessoriosComponent,
    AuditoriaUtilizacaoComponent,
    ValoresCombustivelListaComponent,
    ValoresCombustivelFormComponent,
    ValoresCombustivelFiltroComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(frotaTecnicosRoutes),
    MatPaginatorModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSidenavModule,
    SharedModule,
    TranslocoModule,
    MatSortModule,
    MatInputModule,
    FuseHighlightModule,
    NgxMatSelectSearchModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatSelectModule,
    MatDatepickerModule,
    MatTooltipModule,
    FormsModule,
    FiltroModule,
    MatMenuModule,
    MatTabsModule,
    FuseCardModule,
    MatTableModule
  ]
})
export class FrotaTecnicosModule { }
