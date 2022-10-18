import { UserService } from './../../../../../core/user/user.service';
import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { EquipamentoContratoData, EquipamentoContratoParameters } from 'app/core/types/equipamento-contrato.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { fromEvent, interval, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';
import { Filterable } from 'app/core/filters/filterable';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';

@Component({
	selector: 'app-equipamento-contrato-lista',
	templateUrl: './equipamento-contrato-lista.component.html',
	styles: [
		/* language=SCSS */
		`
    .list-grid-contrato {
      grid-template-columns: 70px 110px 70px 180px 120px auto 80px 80px 50px 160px 80px 50px;
      
      /* @screen sm {
          grid-template-columns: 72px 345px 240px 120px 120px 120px 120px 120px auto;
      }

      @screen md {
          grid-template-columns: 72px 345px 240px 120px 120px 120px 120px 120px auto;
      }

      @screen lg {
          grid-template-columns: 72px 345px 240px 120px 120px 120px 120px 120px auto;
      } */
    }  
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class EquipamentoContratoListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: EquipamentoContratoData;
	isLoading: boolean = false;
	protected _onDestroy = new Subject<void>();

	constructor(
		protected _userService: UserService,
		private _cdr: ChangeDetectorRef,
		private _equipamentoContratoService: EquipamentoContratoService,
		private _exportacaoService: ExportacaoService,

	) {
		super(_userService, 'equipamento-contrato')
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

	ngAfterViewInit() {
		interval(3 * 60 * 1000)
			.pipe(
				startWith(0),
				takeUntil(this._onDestroy)
			)
			.subscribe(() => {
				this.obterDados();
			});

		this.registerEmitters();

		fromEvent(this.searchInputControl?.nativeElement , 'keyup').pipe(
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

		this._cdr.detectChanges();
	}

	async obterDados(filter: string = '') {
		this.isLoading = true;
		const params: EquipamentoContratoParameters = {
			pageNumber: this.paginator.pageIndex + 1,
			sortActive: this.sort?.active,
			sortDirection: this.filter?.parametros?.direction || this.sort.direction || 'desc',
			pageSize: this.filter?.parametros?.qtdPaginacaoLista ?? this.paginator?.pageSize,
			filter: filter
		};
		const data: EquipamentoContratoData = await this._equipamentoContratoService
			.obterPorParametros({
				...params,
				...this.filter?.parametros
			})
			.toPromise();
		this.dataSourceData = data;
		this.isLoading = false;
	}

	async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.EQUIPAMENTO_CONTRATO,
			entityParameters: this.filter?.parametros
		}

		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);

		this.isLoading = false;
	}

	paginar() {
		this.obterDados();
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
