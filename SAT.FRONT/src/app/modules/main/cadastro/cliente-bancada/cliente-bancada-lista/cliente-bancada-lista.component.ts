import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { ClienteBancadaService } from 'app/core/services/cliente-bancada.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { ClienteBancadaData } from 'app/core/types/cliente-bancada.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-cliente-bancada-lista',
	templateUrl: './cliente-bancada-lista.component.html',
	styles: [
		/* language=SCSS */
		`
      .list-grid-u {
          grid-template-columns: 25% 25% 25% 25% ;
          
          @screen sm {
              grid-template-columns: 25% 25% 25% 25% ;
          }
      
          @screen md {
              grid-template-columns: 25% 25% 25% 25% ;
          }
      
          @screen lg {
              grid-template-columns: 25% 25% 25% 25% ;
          }
      }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class ClienteBancadaListaComponent extends Filterable implements AfterViewInit, IFilterable {

	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: ClienteBancadaData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;

	constructor(
		protected _userService: UserService,
		private _exportacaoService: ExportacaoService,
		private _cdr: ChangeDetectorRef,
		private _clienteBancadaService: ClienteBancadaService
	) {
		super(_userService, 'cliente-bancada')
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
		this.obterDados();
		this.registerEmitters();

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
		this.isLoading = false;
	}

	async obterDados(filtro: string = '') {
		this.isLoading = true;

		this.dataSourceData = await this._clienteBancadaService.obterPorParametros({
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: this.sort?.active,
			sortDirection: this.sort?.direction || 'asc',
			pageSize: this.paginator?.pageSize,
			filter: filtro
		}).toPromise();

		const data: ClienteBancadaData = await this._clienteBancadaService.obterPorParametros({
			...this.dataSourceData,
			...this.filter?.parametros
		}).toPromise();
		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	public async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.CLIENTEBANCADA,
			entityParameters: this.filter?.parametros
		}
		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
		this.isLoading = false;
	}

	paginar() {
		this.obterDados();
	}
}
