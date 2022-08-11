import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContratoListaComponent } from './contrato-lista/contrato-lista.component';
import { ContratoFormComponent } from './contrato-form/contrato-form.component';
import { contratoRoutes } from './contrato.routing';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { RouterModule } from '@angular/router';
import { FuseHighlightModule } from '@fuse/components/highlight';
import { TranslocoModule } from '@ngneat/transloco';
import { SharedModule } from 'app/shared/shared.module';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { FuseAlertModule } from '@fuse/components/alert';
import { FuseCardModule } from '@fuse/components/card';
import { ContratoFormLayoutComponent } from './contrato-form-layout/contrato-form-layout.component';
import { ContratoModeloListaComponent } from './contrato-modelo/contrato-modelo-lista/contrato-modelo-lista.component';
import { MatRadioModule } from '@angular/material/radio';
import { ContratoModeloFormComponent } from './contrato-modelo/contrato-modelo-form/contrato-modelo-form.component';
import { ContratoSlaComponent } from './contrato-sla/contrato-sla.component';
import { ContratoFiltroComponent } from './contrato-filtro/contrato-filtro.component';
import { ContratoEquipamentosComponent } from './contrato-equipamentos/contrato-equipamentos.component';
import { ContratoClientePecaComponent } from './contrato-cliente-peca/contrato-cliente-peca.component';
import { FiltroModule } from '../../filtros/filtro.module';
import { ContratoServicoListaComponent } from './contrato-servico/contrato-servico-lista/contrato-servico-lista.component';
import { ContratoServicoFormComponent } from './contrato-servico/contrato-servico-form/contrato-servico-form.component';

const maskConfigFunction: () => Partial<IConfig> = () => {
	return {
		validation: false,
	};
};

@NgModule({
	declarations: [
		ContratoListaComponent,
		ContratoFormComponent,
		ContratoFormLayoutComponent,
		ContratoModeloListaComponent,
		ContratoServicoListaComponent,
		ContratoServicoFormComponent,
		ContratoModeloFormComponent,
		ContratoSlaComponent,
		ContratoFiltroComponent,
		ContratoEquipamentosComponent,
		ContratoClientePecaComponent
	],
	imports: [
		CommonModule,
		RouterModule.forChild(contratoRoutes),
		NgxMaskModule.forRoot(maskConfigFunction),
		MatButtonToggleModule,
		MatChipsModule,
		MatRadioModule,
		MatDatepickerModule,
		MatDividerModule,
		MatMomentDateModule,
		FuseHighlightModule,
		MatButtonModule,
		MatCheckboxModule,
		MatFormFieldModule,
		MatIconModule,
		MatInputModule,
		MatMenuModule,
		MatPaginatorModule,
		MatProgressBarModule,
		MatDialogModule,
		MatRippleModule,
		MatSortModule,
		MatSelectModule,
		MatSlideToggleModule,
		SharedModule,
		MatTableModule,
		MatTabsModule,
		TranslocoModule,
		NgxMatSelectSearchModule,
		FuseCardModule,
		MatBadgeModule,
		MatTooltipModule,
		MatSidenavModule,
		MatListModule,
		MatStepperModule,
		FuseAlertModule,
		MatProgressSpinnerModule,   
		FiltroModule,
		MatSidenavModule
		
	]
})
export class ContratoModule { }
