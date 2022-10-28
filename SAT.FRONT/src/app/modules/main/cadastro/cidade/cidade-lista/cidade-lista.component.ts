import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { CidadeService } from 'app/core/services/cidade.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { Cidade, CidadeData, CidadeParameters } from 'app/core/types/cidade.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, map } from 'rxjs/operators';

@Component({
	selector: 'app-cidade-lista',
	templateUrl: './cidade-lista.component.html',
	styles: [
		/* language=SCSS */
		`
      .list-grid-cidade {
          grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          
          @screen sm {
              grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          }
      
          @screen md {
              grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          }
      
          @screen lg {
              grid-template-columns: 72px auto 5% 10% 10% 10% 10%;
          }
      }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})

export class CidadeListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: CidadeData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	selectedItem: Cidade | null = null;
	userSession: UserSession;

	constructor(
		protected _userService: UserService,
		private _exportacaoService: ExportacaoService,
		private _cdr: ChangeDetectorRef,
		private _cidadeService: CidadeService
	) {
		super(_userService, 'cidade')
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
		const parametros: CidadeParameters = {
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: 'codCidade' || 'nomeCidade',
			sortDirection: 'asc',
			pageSize: this.paginator?.pageSize,
			filter: filtro
		}

		const data: CidadeData = await this._cidadeService.obterPorParametros({
			...parametros,
			...this.filter?.parametros
		}).toPromise();
		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	paginar() {
		this.obterDados();
	}

	public async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.CIDADE,
			entityParameters: this.filter?.parametros
		}
		await this._exportacaoService.exportar(FileMime.Excel,exportacaoParam);
		this.isLoading = false;
	}

}
