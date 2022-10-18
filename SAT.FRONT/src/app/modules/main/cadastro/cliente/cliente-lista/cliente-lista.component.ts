import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { ClienteService } from 'app/core/services/cliente.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { ClienteData, ClienteParameters } from 'app/core/types/cliente.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-cliente-lista',
	templateUrl: './cliente-lista.component.html',
	styles: [
		/* language=SCSS */
		`
      .list-grid-cliente {
          grid-template-columns: 142px auto 25% 25% 42px;
          
          @screen sm {
              grid-template-columns: 142px auto 25% 25% 42px;
          }
      
          @screen md {
              grid-template-columns: 142px auto 25% 25% 42px;
          }
      
          @screen lg {
              grid-template-columns: 142px auto 25% 25% 42px;
          }
      }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class ClienteListaComponent extends Filterable implements AfterViewInit, IFilterable {

	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: ClienteData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;

	constructor(
		private _cdr: ChangeDetectorRef,
		private _clienteService: ClienteService,
		protected _userService: UserService,
		private _exportacaoService: ExportacaoService
	) {
		super(_userService, 'cliente')
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
	}

	async obterDados(filtro: string = '') {
		this.isLoading = true;
		const parametros: ClienteParameters = {
			pageNumber: this.paginator?.pageIndex + 1,
			//sortActive: this.sort?.active || 'razaoSocial',
			sortDirection: 'asc',
			pageSize: this.paginator?.pageSize,
			filter: filtro
		};

		const data: ClienteData = await this._clienteService.obterPorParametros({
			...parametros,
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
			tipoArquivo: ExportacaoTipoEnum.CLIENTE,
			entityParameters: this.filter?.parametros
		}
		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
		this.isLoading = false;
	}

	paginar() {
		this.obterDados();
	}
}
