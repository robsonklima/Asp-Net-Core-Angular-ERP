import { perfilRoutes } from './perfil.routing';
import { ListFormModule } from 'app/shared/listForm.module';
import { NgModule } from '@angular/core';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FiltroModule } from '../../filtros/filtro.module';
import { PerfilFiltroComponent } from './perfil-filtro/perfil-filtro.component';
import { PerfilFormComponent } from './perfil-form/perfil-form.component';
import { PerfilListaComponent } from './perfil-lista/perfil-lista.component';
import { PerfilFormNavegacaoComponent } from './perfil-form/perfil-form-navegacao/perfil-form-navegacao.component';
import { FuseCardModule } from '@fuse/components/card';
import { MatTabsModule } from '@angular/material/tabs';
import { PerfilFormRecursosBloqueadosComponent } from './perfil-form/perfil-form-recursos-bloqueados/perfil-form-recursos-bloqueados.component';
import { MatTreeModule } from '@angular/material/tree';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { PerfilFormRecursosBloqueadosDialogComponent } from './perfil-form/perfil-form-recursos-bloqueados/perfil-form-recursos-bloqueados-dialog/perfil-form-recursos-bloqueados-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    PerfilListaComponent,
    PerfilFormComponent,
    PerfilFiltroComponent,
    PerfilFormNavegacaoComponent,
    PerfilFormRecursosBloqueadosComponent,
    PerfilFormRecursosBloqueadosDialogComponent
  ],
  imports: [
    ListFormModule.configureRoutes(perfilRoutes),
    MatPaginatorModule,
    MatIconModule,
    MatFormFieldModule,
    MatButtonModule,
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
    MatMenuModule,
    FiltroModule,
    MatSidenavModule,
    FuseCardModule,
    MatTabsModule,
    MatTreeModule,
    MatSlideToggleModule,
    MatDialogModule
  ]
})

export class PerfilModule { }