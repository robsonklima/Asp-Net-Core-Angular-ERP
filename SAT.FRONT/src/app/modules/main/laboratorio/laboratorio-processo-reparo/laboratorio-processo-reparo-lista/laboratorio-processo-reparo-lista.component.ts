import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { Filterable } from 'app/core/filters/filterable';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { ORItemData, ORItemParameters } from 'app/core/types/or-item.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-laboratorio-processo-reparo-lista',
  	templateUrl: './laboratorio-processo-reparo-lista.component.html',
	styles: [
		`.list-grid-reparo {
            grid-template-columns: 72px 128px auto 256px 128px 128px 64px 256px 112px;
            
            @screen sm {
                grid-template-columns: 72px 128px auto 256px 128px 128px 64px 256px 112px;
            }
        
            @screen md {
                grid-template-columns: 72px 128px auto 256px 128px 128px 64px 256px 112px;
            }
        
            @screen lg {
                grid-template-columns: 72px 128px auto 256px 128px 128px 64px 256px 112px;
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
	userSession: UserSession;

	constructor(
		protected _userService: UserService,
		private _exportacaoService: ExportacaoService,
		private _cdr: ChangeDetectorRef,
		private _orItemService: ORItemService
	) {
		super(_userService, 'processo-reparo')
		this.userSession = JSON.parse(this._userService.userSession);
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

	onSidenavClosed(): void {
		if (this.paginator) this.paginator.pageIndex = 0;
		this.loadFilter();
		this.obterDados();
	}

	ngAfterViewInit(): void {
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

		this.dataSourceData = data;
    console.log(data);
    
		this.isLoading = false;
		this._cdr.detectChanges();
	}

  calcularDiasEmReparo(inicio: string) {
    if (inicio)
      return moment.duration(moment(inicio).diff(moment())).asDays();
  }

	paginar() {
		this.obterDados();
	}

	public async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.OR_ITEM,
			entityParameters: this.filter?.parametros
		}
		await this._exportacaoService.exportar(FileMime.Excel,exportacaoParam);
		this.isLoading = false;
	}
}
