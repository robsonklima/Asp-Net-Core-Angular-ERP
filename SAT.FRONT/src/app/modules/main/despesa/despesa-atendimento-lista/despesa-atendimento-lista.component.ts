import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { UserService } from 'app/core/user/user.service';
import { LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import { Filterable } from 'app/core/filters/filterable';
import { MatSidenav } from '@angular/material/sidenav';
import { IFilterable } from 'app/core/types/filtro.types';
import { ActivatedRoute, Router } from '@angular/router';
import { DespesaPeriodoTecnico, DespesaPeriodoTecnicoAtendimentoData, DespesaPeriodoTecnicoAtendimentoItem,
	DespesaPeriodoTecnicoParameters,
	DespesaPeriodoTecnicoStatusEnum } from 'app/core/types/despesa-periodo.types';
import { MatDialog } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { DespesaAtendimentoAdiantamentoDialogComponent } from './despesa-atendimento-adiantamento-dialog/despesa-atendimento-adiantamento-dialog.component';
import { DespesaAtendimentoObservacaoImpressaoComponent } from './despesa-atendimento-observacao-impressao/despesa-atendimento-observacao-impressao.component';
import { Tecnico } from 'app/core/types/tecnico.types';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { RoleEnum } from 'app/core/user/user.types';
import moment from 'moment';
import localePt from '@angular/common/locales/pt';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { DespesaAdiantamentoPeriodo } from 'app/core/types/despesa-adiantamento.types';
import { DespesaAdiantamentoPeriodoService } from 'app/core/services/despesa-adiantamento-periodo.service';
registerLocaleData(localePt);

@Component({
	selector: 'app-despesa-atendimento-lista',
	templateUrl: './despesa-atendimento-lista.component.html',
	styles: [`.list-grid-despesa-atendimento {
            grid-template-columns: 80px 80px 70px 90px 90px 115px auto 50px 150px;
            /* @screen sm { grid-template-columns: 80px 80px 70px 90px 90px 115px auto 50px 150px; }
            @screen md { grid-template-columns: 80px 80px 70px 90px 90px 115px auto 50px 150px; }
            @screen lg { grid-template-columns: 80px 80px 70px 90px 90px 115px auto 50px 150px; } */
        }
    `],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations,
	providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class DespesaAtendimentoListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild('sidenav') sidenav: MatSidenav;

	isLoading: boolean = false;
	atendimentos: DespesaPeriodoTecnicoAtendimentoData;
	codTecnico: string;
	periodoLiberado: DespesaPeriodoTecnicoStatusEnum = DespesaPeriodoTecnicoStatusEnum['LIBERADO PARA ANÁLISE'];
	tecnico: Tecnico;
	despesaPeriodoTecnico: DespesaPeriodoTecnico [] = [];

	constructor(
		protected _userService: UserService,
		private _cdr: ChangeDetectorRef,
		private _route: ActivatedRoute,
		private _router: Router,
		private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService,
		private _tecnicoSvc: TecnicoService,
		private _snack: CustomSnackbarService,
		private _exportacaoService: ExportacaoService,
		private _dialog: MatDialog) {
		super(_userService, "despesa-atendimento");
		this.codTecnico = this._route.snapshot.paramMap.get('codTecnico') || this.userSession.usuario?.codTecnico;
		this.obterTecnico();
	}

	ngAfterViewInit() {
		this.obterDados();

		if (this.sort && this.paginator) {
			this.sort.disableClear = true;
			this._cdr.markForCheck();

			this.sort.sortChange.subscribe(() => {
				this.onSortChanged()
				this.obterDados();
			});
		}

		this.registerEmitters();
		this._cdr.detectChanges();
	}

	private async obterDespesasPeriodoTecnico() {
		this.atendimentos = (await this._despesaPeriodoTecnicoSvc
			.obterAtendimentos(
				{
					codTecnico: this.codTecnico,
					indAtivoPeriodo: this.filter?.parametros?.indAtivo,
					codDespesaPeriodoStatus: this.filter?.parametros?.codDespesaPeriodoStatus,
					inicioPeriodo: this.filter?.parametros?.inicioPeriodo,
					fimPeriodo: this.filter?.parametros?.fimPeriodo,
					pageNumber: this.paginator?.pageIndex + 1,
					pageSize: this.paginator?.pageSize,
					sortActive: 'dataInicio',
					sortDirection: 'desc'
				}).toPromise());
	}

	public async obterDados() {
		this.isLoading = true;

		await this.obterDespesasPeriodoTecnico();

		this.isLoading = false;
	}

	registerEmitters(): void {
		this.sidenav?.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
		})
	}

	public paginar() {
		this.onPaginationChanged();
		this.obterDados();
	}

	criaDespesaPeriodoTecnico(dpi: DespesaPeriodoTecnicoAtendimentoItem): DespesaPeriodoTecnico {
		var dp: DespesaPeriodoTecnico =
		{
			codDespesaPeriodo: dpi.codDespesaPeriodo,
			codTecnico: parseInt(dpi.codTecnico),
			codDespesaPeriodoTecnicoStatus: parseInt(dpi.status.codDespesaPeriodoTecnicoStatus),
			dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
			codUsuarioCad: this.userSession.usuario.codUsuario
		}

		return dp;
	}

	async obterDespesaPeriodoTecnico(dpi: DespesaPeriodoTecnicoAtendimentoItem) {		
		this.despesaPeriodoTecnico = (await this._despesaPeriodoTecnicoSvc.obterPorParametros(
			{
			  codTecnico: this.codTecnico,
			  codDespesaPeriodo: dpi.codDespesaPeriodo
			}).toPromise()).items;			
	}

	async obterTecnico() {
		this.tecnico = (await this._tecnicoSvc.obterPorCodigo(+this.codTecnico).toPromise());
	}

	liberar(dpi: DespesaPeriodoTecnicoAtendimentoItem) {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: 'Deseja liberar este período?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
			if (confirmacao) {
				dpi.status = { codDespesaPeriodoTecnicoStatus: this.periodoLiberado };	
				var dpt = this.obterDespesaPeriodoTecnico(dpi);
				
				if(dpt){
					this._despesaPeriodoTecnicoSvc.atualizar(dp).subscribe(() => {
						this._snack.exibirToast('Período liberado com sucesso!', 'success');
						this.obterDados();
					},
						e => {
							this._snack.exibirToast('Erro ao liberar período.', 'error');
						});
				}
				else
				{
					var dp = this.criaDespesaPeriodoTecnico(dpi);
					this._despesaPeriodoTecnicoSvc.criar(dp).subscribe(() => {
						this._snack.exibirToast('Período liberado com sucesso!', 'success');
						this.obterDados();
						},
						e => {
							this._snack.exibirToast('Erro ao liberar período.', 'error');
						});
				}
			}
		});
	}

	listarAdiantamentos(dpi: DespesaPeriodoTecnicoAtendimentoItem) {
		this._dialog.open(DespesaAtendimentoAdiantamentoDialogComponent, {
			data:
			{
				codTecnico: this.codTecnico,
				codPeriodo: dpi.codDespesaPeriodo
			}
		});
	}

	async imprimirRD(dpi: DespesaPeriodoTecnicoAtendimentoItem) {
		this.isLoading = true;

		const despesaParams: DespesaPeriodoTecnicoParameters = {
			codDespesaPeriodoTecnico: dpi.codDespesaPeriodoTecnico
		} 

		const exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.PDF,
			tipoArquivo: ExportacaoTipoEnum.DESPESA_PERIODO_TECNICO,
			entityParameters: despesaParams
		}

		await this._exportacaoService.exportar(FileMime.PDF, exportacaoParam );
		this.isLoading = false;
	}

	imprimirObservacoes(dpi: DespesaPeriodoTecnicoAtendimentoItem) {
		this._dialog.open(DespesaAtendimentoObservacaoImpressaoComponent, {
			panelClass: 'no-padding-dialog-container',
			data:
			{
				codDespesaPeriodoTecnico: dpi.codDespesaPeriodoTecnico
			}
		});
	}

	isTecnico() { return this.userSession?.usuario?.codPerfil == RoleEnum.FILIAL_TECNICO_DE_CAMPO; };
	
	isLider() {
		return this.userSession?.usuario?.codPerfil == RoleEnum.FILIAL_LIDER ||
			this.userSession?.usuario?.codPerfil == RoleEnum.ADMIN ||
			this.userSession?.usuario?.codPerfil == RoleEnum.FILIAL_COORDENADOR ||
			this.userSession?.usuario?.codPerfil == RoleEnum.FILIAL_LIDER_DE_SETOR
	}

	isLiberado(dpi: DespesaPeriodoTecnicoAtendimentoItem) { return dpi?.status != null && dpi?.status?.nomeDespesaPeriodoTecnicoStatus != 'REPROVADO'; }
}