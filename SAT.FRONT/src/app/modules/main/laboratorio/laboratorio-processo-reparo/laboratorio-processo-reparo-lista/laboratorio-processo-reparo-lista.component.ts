import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { Filterable } from 'app/core/filters/filterable';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { ORItem, ORItemData, ORItemParameters } from 'app/core/types/or-item.types';
import { orStatusConst } from 'app/core/types/or-status.types';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import _ from 'lodash';
import moment from 'moment';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { LaboratorioProcessoReparoDetalheComponent } from '../laboratorio-processo-reparo-detalhe/laboratorio-processo-reparo-detalhe.component';
import { LaboratorioProcessoReparoHistoricoComponent } from '../laboratorio-processo-reparo-historico/laboratorio-processo-reparo-historico.component';
import { ProcessoReparoListaMaisOpcoesComponent } from './processo-reparo-lista-mais-opcoes/processo-reparo-lista-mais-opcoes.component';

@Component({
	selector: 'app-laboratorio-processo-reparo-lista',
	templateUrl: './laboratorio-processo-reparo-lista.component.html',
	styles: [
		`.list-grid-reparo {
            grid-template-columns: 72px 72px 88px auto 94px 128px 108px 72px 98px 112px 60px 92px;
        }`
	]
})

export class LaboratorioProcessoReparoListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: ORItemData;
	isLoading: boolean = false;
	itemSelecionado: ORItem;
	userSession: UserSession;

	constructor(
		private _dialog: MatDialog,
		private _cdr: ChangeDetectorRef,
		private _orItemService: ORItemService,
		private _exportacaoService: ExportacaoService,
		protected _userService: UserService,
	) {
		super(_userService, 'processo-reparo')
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngAfterViewInit(): void {
		this.registerEmitters();
		this.obterDados();

		if (this.sort && this.paginator)
		{
			fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
				map((event: any) => {
					return event.target.value;
				})
				, debounceTime(700)
				, distinctUntilChanged()
			).subscribe((text: string) => {
				this.paginator.pageIndex = 0;
				this.searchInputControl.nativeElement.val = text;
				this.obterDados(text);
			});

			this.sort.disableClear = true;
			this._cdr.markForCheck();

			this.sort.sortChange.subscribe(() => {
				this.paginator.pageIndex = 0;
				this.obterDados();
			});
		}

		this._cdr.detectChanges();
	}

	async obterDados(filtro: string = '') {
		this.isLoading = true;

		const parametros: ORItemParameters = {
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: this.sort.active || 'codORItem',
			sortDirection: this.sort.direction || 'desc',
			pageSize: this.paginator?.pageSize,
			filter: filtro,
			nomeTecnico: this.isPerfilTecnico() ? this.userSession.usuario.nomeUsuario : this.filter?.parametros?.nomeTecnico
		}

		const data: ORItemData = await this._orItemService.obterPorParametros({
			...this.filter?.parametros,
			...parametros,
		}).toPromise();

		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	calcularDiasEmReparo(inicio: string) {
		if (inicio) return moment.duration(moment(inicio).diff(moment())).asDays();
	}

	isPerfilTecnico(): boolean {
		if (this.userSession.usuario.codPerfil == PerfilEnum.TECNICO)
			return true;

		if (this.userSession.usuario.codPerfil == PerfilEnum.ASSISTENTE)
			return true;

		return false
	}

	async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.OR_ITEM,
			entityParameters: this.filter?.parametros
		}
		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
		this.isLoading = false;
	}

	obterCorStatus(cod: number): string {
		switch (cod)
		{
			case orStatusConst.CONFERENCIA_LABORATORIO:
				return 'bg-black-200 border-black-400 border-1';
			case orStatusConst.AGUARDANDO_REPARO:
				return 'bg-white border-grey-200 border-1';
			case orStatusConst.TRANSFERENCIA_CD_ESTOQUE:
				return 'bg-grey-100 border-grey-300 border-1';
			case orStatusConst.PARCIAL_FALTA_DE_INSUMO:
				return 'bg-orange-100 border-orange-300 border-1';
			case orStatusConst.PECA_LIBERADA:
				return 'bg-green-200 border-green-400 border-1';
			case orStatusConst.REAPROVEITAMENTO_SUCATA:
				return 'bg-grey-400 border-grey-600 border-1';
			case orStatusConst.EM_REPARO:
				return 'bg-yellow-100 border-yellow-300 border-1';
			case orStatusConst.TRANSFERIDO_TECNICO:
				return 'bg-yellow-200 border-yellow-400 border-1';
			case orStatusConst.SUPORTE:
				return 'bg-blue-200 border-blue-400 border-1';
			case orStatusConst.OR_ENCERRADA:
				return 'bg-green-100 border-green-300 border-1';
			default:
				return '';
		}
	}

	abrirForm(item: ORItem) {
		const dialogRef = this._dialog.open(LaboratorioProcessoReparoDetalheComponent, {
			width: '768px',
			data: {
				item: item
			},
		});

		dialogRef.afterClosed().subscribe(confirmacao => {
			if (confirmacao) this.obterDados();
		});
	}

	verificarExistemItensSelecionados() {
		return this.dataSourceData?.items?.filter(i => i.selecionado)?.length;
	}

	abrirMaisOpcoes() {
		const itens = this.dataSourceData.items.filter(i => i.selecionado);

		const dialogRef = this._dialog.open(ProcessoReparoListaMaisOpcoesComponent, {
			data: {
				itens: itens
			},
		});

		dialogRef.afterClosed().subscribe(confirmacao => {
			if(confirmacao) this.obterDados();
		});
	}

	toggleSelecionarTodos(e: any) {
		this.dataSourceData.items = this.dataSourceData.items.map(i => { return { ...i, selecionado: e.checked } });
	}

	toggleTranferencia() {
		return _.find(this.dataSourceData?.items, { selecionado: true });
	}

	onSidenavClosed(): void {
		if (this.paginator) this.paginator.pageIndex = 0;
		this.loadFilter();
		this.obterDados();
	}

	registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
		})
	}

	loadFilter(): void {
		super.loadFilter();
	}

	abrirHistorico(item: ORItem) {
		const dialogRef = this._dialog.open(LaboratorioProcessoReparoHistoricoComponent, {
			width: '1024px',
			data: {
				item : item
			},
		});

		dialogRef.afterClosed().subscribe(confirmacao => {
			if(confirmacao) this.obterDados();
		});
	}

	paginar() {
		this.obterDados();
	}
}
