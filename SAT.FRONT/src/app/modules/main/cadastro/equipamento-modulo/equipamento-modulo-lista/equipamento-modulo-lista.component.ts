import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { EquipamentoModuloService } from 'app/core/services/equipamento-modulo.service';
import { EquipamentoModuloData, EquipamentoModuloParameters } from 'app/core/types/equipamento-modulo.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { FileMime } from 'app/core/types/file.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';

@Component({
	selector: 'app-equipamento-modulo-lista',
	templateUrl: './equipamento-modulo-lista.component.html',
	styles: [
		/* language=SCSS */
		`
    .list-grid-u {
      grid-template-columns: 142px auto 40% 150px 150px;
  }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class EquipamentoModuloListaComponent extends Filterable implements AfterViewInit, IFilterable {

	@ViewChild('sidenav') public sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: EquipamentoModuloData;
	isLoading: boolean = false;

	userSession: UserSession;

	constructor(
		protected _userService: UserService,
		private _equipamentoModuloService: EquipamentoModuloService,
		private _cdr: ChangeDetectorRef,
		private _exportacaoService: ExportacaoService
	) {
		super(_userService, 'equipamento-modulo')
		this.userSession = JSON.parse(this._userService.userSession);
	}

	registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
		})
	}

	async ngAfterViewInit() {
		this.registerEmitters();
		this.obterDados();

		if (this.sort && this.paginator) {
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

		const parametros: EquipamentoModuloParameters = {
			...{
				pageNumber: this.paginator?.pageIndex + 1,
				sortActive: 'codEquip',
				sortDirection: 'asc',
				pageSize: this.paginator?.pageSize,
				filter: filtro
			},
			...this.filter?.parametros
		}

		const data = await this._equipamentoModuloService.obterPorParametros(parametros).toPromise();
		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.EQUIPAMENTOMODULO,
			entityParameters: this.filter?.parametros
		}
		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);

		this.isLoading = false;
	}


	paginar() {
		this.obterDados();
	}
}