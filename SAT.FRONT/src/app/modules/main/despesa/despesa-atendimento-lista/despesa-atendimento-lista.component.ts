import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { AfterViewInit, ChangeDetectorRef, Component, LOCALE_ID, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaAdiantamentoPeriodoService } from 'app/core/services/despesa-adiantamento-periodo.service';
import { DespesaAdiantamentoService } from 'app/core/services/despesa-adiantamento.service';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { DespesaAdiantamentoData, DespesaAdiantamentoPeriodo, DespesaAdiantamentoPeriodoData } from 'app/core/types/despesa-adiantamento.types';
import {
	DespesaPeriodoTecnico, DespesaPeriodoTecnicoAtendimentoData, DespesaPeriodoTecnicoAtendimentoItem,
	DespesaPeriodoTecnicoData,
	DespesaPeriodoTecnicoParameters,
	DespesaPeriodoTecnicoStatusEnum
} from 'app/core/types/despesa-periodo.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';
import { RoleEnum } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import _ from 'lodash';
import moment from 'moment';
import { DespesaAtendimentoAdiantamentoDialogComponent } from './despesa-atendimento-adiantamento-dialog/despesa-atendimento-adiantamento-dialog.component';
import { DespesaAtendimentoObservacaoImpressaoComponent } from './despesa-atendimento-observacao-impressao/despesa-atendimento-observacao-impressao.component';
registerLocaleData(localePt);

@Component({
	selector: 'app-despesa-atendimento-lista',
	templateUrl: './despesa-atendimento-lista.component.html',
	styles: [`.list-grid-despesa-atend {
            grid-template-columns: 80px 80px 70px 90px 90px 115px auto 50px 150px;
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
	adiantamentos: DespesaAdiantamentoData;
	adiantamentosPeriodos: DespesaAdiantamentoPeriodoData;
	codTecnico: string;
	periodoLiberado: DespesaPeriodoTecnicoStatusEnum = DespesaPeriodoTecnicoStatusEnum['LIBERADO PARA ANÁLISE'];
	tecnico: Tecnico;
	despesaPeriodoTecnico: DespesaPeriodoTecnico[] = [];

	constructor(
		protected _userService: UserService,
		private _cdr: ChangeDetectorRef,
		private _route: ActivatedRoute,
		private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService,
		private _despesaAdiantamentoPeriodoSvc: DespesaAdiantamentoPeriodoService,
		private _despesaAdiantamentoSvc: DespesaAdiantamentoService,
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
					codDespesaPeriodoTecnicoStatus: this.filter?.parametros?.codDespesaPeriodoTecnicoStatus,
					inicioPeriodo: this.filter?.parametros?.inicioPeriodo,
					fimPeriodo: this.filter?.parametros?.fimPeriodo,
					pageNumber: this.paginator?.pageIndex + 1,
					pageSize: this.paginator?.pageSize,
					sortActive: 'dataInicio',
					sortDirection: 'desc'
				}).toPromise());
	}

	private async obterAdiantamentos() {
		this.adiantamentos = (await this._despesaAdiantamentoSvc
			.obterPorParametros(
				{
					codTecnicos: this.codTecnico
				}).toPromise());
	}

	private async obterAdiantamentosPeriodos() {
		this.adiantamentosPeriodos = (await this._despesaAdiantamentoPeriodoSvc
			.obterPorParametros(
				{
					codTecnico: +(this.codTecnico)
				}).toPromise());
	}

	public async obterDados() {
		this.isLoading = true;

		await this.obterDespesasPeriodoTecnico();
		await this.obterAdiantamentos();
		await this.obterAdiantamentosPeriodos();

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

	async liberar(dpi: DespesaPeriodoTecnicoAtendimentoItem) {
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

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
			if (confirmacao) {
				dpi.status = { codDespesaPeriodoTecnicoStatus: this.periodoLiberado };
				const dp = this.criaDespesaPeriodoTecnico(dpi);
				const dpt = (await this.obterDespesaPeriodoTecnico(dpi)).items.shift();

				if (dpt) {
					dpt.codDespesaPeriodoTecnicoStatus = 1;
					this._despesaPeriodoTecnicoSvc.atualizar(dpt).subscribe(() => {
						this._snack.exibirToast('Período liberado com sucesso!', 'success');
						this.obterDados();
						this.atualizaAdiantamentoUtilizado(dpi);
					},
						e => {
							this._snack.exibirToast('Erro ao liberar período.', 'error');
						});
				}
				else {
					this._despesaPeriodoTecnicoSvc.criar(dp).subscribe(() => {
						this._snack.exibirToast('Período liberado com sucesso!', 'success');
						this.obterDados();
						this.atualizaAdiantamentoUtilizado(dpi);
					},
						e => {
							this._snack.exibirToast('Erro ao liberar período.', 'error');
						});
				}
			}
		});
	}

	atualizaAdiantamentoUtilizado(dpi: DespesaPeriodoTecnicoAtendimentoItem) {
		const despesas = this.atendimentos.items.filter((a) => a.codDespesaPeriodo == dpi.codDespesaPeriodo);
		const adiantamentos = (this.adiantamentos.items);
		const adiantamentosPeriodos = (this.adiantamentosPeriodos.items);
		let totalDespesa = _.sum(despesas.map(d => d.totalDespesa));

		for (const adiantamento of adiantamentos) {
			const adiantamentosUtilizados = adiantamentosPeriodos.filter(u => u.codDespesaAdiantamento == adiantamento.codDespesaAdiantamento);
			const saldo = adiantamento.valorAdiantamento - _.sum(adiantamentosUtilizados.map(au => au.valorAdiantamentoUtilizado));
						
			if (!saldo) continue;

			if (saldo >= totalDespesa) {								
				const adiantamentoUtilizado: DespesaAdiantamentoPeriodo = {
					codDespesaAdiantamento: adiantamento.codDespesaAdiantamento,
					codDespesaPeriodo: dpi.codDespesaPeriodo,
					codUsuarioCad: this.userSession.usuario.codUsuario,
					dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
					valorAdiantamentoUtilizado: totalDespesa
				}
				
				if(adiantamentoUtilizado.valorAdiantamentoUtilizado > 0)
				{	
					this._despesaAdiantamentoPeriodoSvc.criar(adiantamentoUtilizado)
						.subscribe(() => {}, (e) => {
							this._snack.exibirToast('Erro ao inserir valor de adiantamento utilizado', 'error');
						});
				}

				break;
			} else {			
				totalDespesa = totalDespesa - saldo;

				const adiantamentoUtilizado: DespesaAdiantamentoPeriodo = {
					codDespesaAdiantamento: adiantamento.codDespesaAdiantamento,
					codDespesaPeriodo: dpi.codDespesaPeriodo,
					codUsuarioCad: this.userSession.usuario.codUsuario,
					dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
					valorAdiantamentoUtilizado: saldo
				}
				
				if(adiantamentoUtilizado.valorAdiantamentoUtilizado > 0)
				{
					this._despesaAdiantamentoPeriodoSvc.criar(adiantamentoUtilizado)
						.subscribe(() => {}, (e) => {
							this._snack.exibirToast('Erro ao inserir valor de adiantamento utilizado', 'error');
						});
				}
			}
		}
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

	async obterDespesaPeriodoTecnico(dpi: DespesaPeriodoTecnicoAtendimentoItem): Promise<DespesaPeriodoTecnicoData> {
		return (await this._despesaPeriodoTecnicoSvc.obterPorParametros({
			codTecnico: this.codTecnico,
			codDespesaPeriodo: dpi.codDespesaPeriodo
		}).toPromise());
	}

	async obterTecnico() {
		this.tecnico = (await this._tecnicoSvc.obterPorCodigo(+this.codTecnico).toPromise());
	}

	listarAdiantamentos(dpi: DespesaPeriodoTecnicoAtendimentoItem) {
		this._dialog.open(DespesaAtendimentoAdiantamentoDialogComponent, {
			data: {
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

		await this._exportacaoService.exportar(FileMime.PDF, exportacaoParam);
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
		return this.userSession?.usuario?.codPerfil == RoleEnum.LIDER ||
			this.userSession?.usuario?.codPerfil == RoleEnum.ANALISTA ||
			this.userSession?.usuario?.codPerfil == RoleEnum.ASSISTENTE ||
			this.userSession?.usuario?.codPerfil == RoleEnum.ADM_DO_SISTEMA ||
			this.userSession?.usuario?.codPerfil == RoleEnum.COORDENADOR ||
			this.userSession?.usuario?.codPerfil == RoleEnum.SUPERVISOR
	}

	isPermiteObservacoes()
	{
		return this.userSession?.usuario?.codPerfil == RoleEnum.ANALISTA ||
			this.userSession?.usuario?.codPerfil == RoleEnum.LIDER ||
			this.userSession?.usuario?.codPerfil == RoleEnum.ADM_DO_SISTEMA ||
			this.userSession?.usuario?.codPerfil == RoleEnum.COORDENADOR ||
			this.userSession?.usuario?.codPerfil == RoleEnum.SUPERVISOR
	}

	isLiberado(dpi: DespesaPeriodoTecnicoAtendimentoItem) { return dpi?.status != null && dpi?.status?.nomeDespesaPeriodoTecnicoStatus != 'REPROVADO'; }
}