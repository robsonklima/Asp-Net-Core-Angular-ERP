import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { PecaParameters, PecaStatus } from 'app/core/types/peca.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { ClientePecaService } from 'app/core/services/cliente-peca.service';
import { ClientePecaData, ClientePecaParameters } from 'app/core/types/cliente-peca.types';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { FileMime } from 'app/core/types/file.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';

@Component({
	selector: 'app-cliente-peca-lista',
	templateUrl: './cliente-peca-lista.component.html',
	styles: [
		/* language=SCSS */
		`
      .list-grid-ge {
          grid-template-columns: 20% 100px 100px 20% 20% auto;
          
          @screen sm {
              grid-template-columns: 20% 100px 100px 20% 20% auto;
          }
      
          @screen md {
              grid-template-columns: 20% 100px 100px 20% 20% auto;
          }
      
          @screen lg {
              grid-template-columns: 20% 100px 100px 20% 20% auto;
          }
      }
    `
	],
})
export class ClientePecaListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: ClientePecaData;
	byteArray;
	isLoading: boolean = false;
	pecaStatus: string[] = [];

	constructor(
		private _cdr: ChangeDetectorRef,
		private _clientePecaService: ClientePecaService,
		private _exportacaoService: ExportacaoService,
		protected _userService: UserService
	) { super(_userService, 'cliente-peca'); }

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
		this.obterStatus();

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

	public async obterDados(filter: string = '') {
		this.isLoading = true;		
		const params: ClientePecaParameters =
		{
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: this.sort?.active,
			sortDirection: this.sort?.direction || 'desc',
			pageSize: this.paginator?.pageSize,
			filter: filter
		}

		const data: ClientePecaData = await this._clientePecaService.obterPorParametros({
			...params,
			...this.filter?.parametros
		}).toPromise();
		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	private async obterStatus(): Promise<void> {
		Object.keys(PecaStatus)
			.filter((e) => isNaN(Number(e)))
			.forEach((tr) =>
				this.pecaStatus.push(tr));
	}

	public async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.CLIENTEPECA,
			entityParameters: this.filter?.parametros
		}
		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
		this.isLoading = false;
	}

	public paginar() { this.obterDados(); }
}
