import { LOCALE_ID, NgModule } from '@angular/core';
import { PontoPeriodoListaComponent } from './ponto-periodo-lista/ponto-periodo-lista.component';
import { RouterModule } from '@angular/router';
import { pontoRoutes } from './ponto.routing';
import { SharedModule } from 'app/shared/shared.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { MatMenuModule } from '@angular/material/menu';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CommonModule } from '@angular/common';
import { TranslocoModule } from '@ngneat/transloco';
import { PontoColaboradorListaComponent } from './ponto-colaborador-lista/ponto-colaborador-lista.component';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { PontoPeriodoFormComponent } from './ponto-periodo-form/ponto-periodo-form.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { PontoTurnoListaComponent } from './ponto-turno-lista/ponto-turno-lista.component';
import { PontoTurnoFormComponent } from './ponto-turno-form/ponto-turno-form.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { PontoHorariosListaComponent } from './ponto-horarios-lista/ponto-horarios-lista.component';
import { registerLocaleData } from '@angular/common';
import localeBr from '@angular/common/locales/pt';
import { PontoRelatoriosAtendimentoComponent } from './ponto-relatorios-atendimento/ponto-relatorios-atendimento.component';
import { MatTableModule } from '@angular/material/table';
import { PontoInconsistenciaFormComponent } from './ponto-inconsistencia-form/ponto-inconsistencia-form.component';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { MatCheckboxModule } from '@angular/material/checkbox';

registerLocaleData(localeBr, 'pt');

const maskConfigFunction: () => Partial<IConfig> = () => {
  return {
      validation: false,
  };
};

@NgModule({
  declarations: [
    PontoPeriodoListaComponent,
    PontoColaboradorListaComponent,
    PontoPeriodoFormComponent,
    PontoTurnoListaComponent,
    PontoTurnoFormComponent,
    PontoHorariosListaComponent,
    PontoRelatoriosAtendimentoComponent,
    PontoInconsistenciaFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(pontoRoutes),
    NgxMaskModule.forRoot(maskConfigFunction),
    MatPaginatorModule,
    SharedModule,
    MatIconModule,
    MatSortModule,
    MatMenuModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressBarModule,
    TranslocoModule,
    MatButtonModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
    MatDatepickerModule,
    MatOptionModule,
    MatSelectModule,
    MatTableModule,
    MatCheckboxModule
  ],
  providers: [{ provide: LOCALE_ID, useValue: 'pt' }]
})
export class PontoModule { }
