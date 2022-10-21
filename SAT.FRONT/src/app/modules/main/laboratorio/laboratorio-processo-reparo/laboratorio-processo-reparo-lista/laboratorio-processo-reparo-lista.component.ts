import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { Filterable } from 'app/core/filters/filterable';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { UsuarioService } from 'app/core/services/usuario.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { ORItem, ORItemData, ORItemParameters } from 'app/core/types/or-item.types';
import { orStatusConst } from 'app/core/types/or-status.types';
import { statusConst } from 'app/core/types/status-types';
import { Usuario, UsuarioData, UsuarioParameters } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import _ from 'lodash';
import moment from 'moment';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { LaboratorioProcessoReparoFormComponent } from '../laboratorio-processo-reparo-form/laboratorio-processo-reparo-form.component';

@Component({
	selector: 'app-laboratorio-processo-reparo-lista',
	templateUrl: './laboratorio-processo-reparo-lista.component.html',
	styles: [
		`.list-grid-reparo {
            grid-template-columns: 72px 72px 128px auto 94px 128px 108px 64px 156px 112px 64px;
            
            @screen sm {
                grid-template-columns: 72px 72px 128px auto 94px 128px 108px 64px 156px 112px 64px;
            }
        
            @screen md {
                grid-template-columns: 72px 72px 128px auto 94px 128px 108px 64px 156px 112px 64px;
            }
        
            @screen lg {
                grid-template-columns: 72px 72px 128px auto 94px 128px 108px 64px 156px 112px 64px;
            }
        }`
	]
})

export class LaboratorioProcessoReparoListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: ORItemData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	usuariosTecnicos: Usuario[] = [];
	itemSelecionado: ORItem;
	userSession: UserSession;

	constructor(
		protected _userService: UserService,
		private _exportacaoService: ExportacaoService,
		private _cdr: ChangeDetectorRef,
		private _dialog: MatDialog,
		private _orItemService: ORItemService,
		private _usuarioService: UsuarioService
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

	registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
		});
	}

	loadFilter(): void {
		super.loadFilter();
	}

	onSidenavClosed(): void {
		if (this.paginator) this.paginator.pageIndex = 0;
		this.loadFilter();
		this.obterDados();
	}

	async obterDados(filtro: string = '') {
		this.isLoading = true;
		const parametros: ORItemParameters = {
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: this.sort.active || 'codORItem',
			sortDirection: this.sort.direction || 'desc',
			pageSize: this.paginator?.pageSize,
			filter: filtro
		}

		const data: ORItemData = await this._orItemService.obterPorParametros({
			...parametros,
			...this.filter?.parametros
		}).toPromise();

		this.usuariosTecnicos = (await this.obterUsuariosTecnicos()).items;
		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	calcularDiasEmReparo(inicio: string) {
		if (inicio)
			return moment.duration(moment(inicio).diff(moment())).asDays();
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

	async obterUsuariosTecnicos(): Promise<UsuarioData> {
		let params: UsuarioParameters = {
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeUsuario',
			sortDirection: 'asc',
			codPerfil: 61,
		};

		return await this._usuarioService
			.obterPorParametros(params)
			.toPromise();
	}

	obterCorStatus(cod: number): string {
		switch (cod) {
			case orStatusConst.CONFERENCIA_LABORATORIO:
				return 'bg-black-200';
			case orStatusConst.AGUARDANDO_REPARO:
				return 'bg-white';
			case orStatusConst.TRANSFERENCIA_CD_ESTOQUE:
				return 'bg-grey-100';
			case orStatusConst.PARCIAL_FALTA_DE_INSUMO:
				return 'bg-orange-100';
			case orStatusConst.PECA_LIBERADA:
				return '#66cc00';
			case orStatusConst.REAPROVEITAMENTO_SUCATA:
				return 'bg-grey-400';
			case orStatusConst.EM_REPARO:
				return 'bg-yellow-100';
			case orStatusConst.TRANSFERIDO_TECNICO:
				return 'bg-yellow-200';
			case orStatusConst.SUPORTE:
				return 'bg-blue-200';
			case orStatusConst.OR_ENCERRADA:
				return 'bg-green-100';
			default:
				return '';
		}
	}

	abrirForm(item: ORItem) {
		const dialogRef = this._dialog.open(LaboratorioProcessoReparoFormComponent, {
			width: '640px',
			data: {
				item: item
			},
		});

		dialogRef.afterClosed().subscribe(confirmacao => {
			if (confirmacao) this.obterDados();
		});
	}

	toggleSelecionarTodos(e: any) {
		this.dataSourceData.items = this.dataSourceData.items.map(i => { return { ...i, selecionado: e.checked } });
	}

	toggleTranferencia() {
		return _.find(this.dataSourceData?.items, { selecionado: true });
	}

	paginar() {
		this.obterDados();
	}
}
