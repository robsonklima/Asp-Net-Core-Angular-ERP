import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { Tecnico, TecnicoData, TecnicoParameters } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { ExportacaoService } from './../../../../../core/services/exportacao.service';

@Component({
	selector: 'app-tecnico-lista',
	templateUrl: './tecnico-lista.component.html',
	styles: [
		/* language=SCSS */
		`
      .list-grid-ge {
          grid-template-columns: 72px auto 5% 10% 10% 60px 60px;
          
          /* @screen sm {
              grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          }
      
          @screen md {
              grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          }
      
          @screen lg {
              grid-template-columns: 72px auto 5% 10% 10% 48px 42px;
          } */
      }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class TecnicoListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') public sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: TecnicoData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	selectedItem: Tecnico | null = null;
	userSession: UserSession;

	constructor(
		private _cdr: ChangeDetectorRef,
		private _tecnicoService: TecnicoService,
		private _exportacaoService: ExportacaoService,
		protected _userService: UserService
	) {
		super(_userService, 'tecnico')
		this.userSession = JSON.parse(this._userService.userSession);
	}

	registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
		})
	}

	onSidenavClosed(): void {
		if (this.paginator) this.paginator.pageIndex = 0;

		this.loadFilter();
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

		const params: TecnicoParameters = {
			...{
				pageNumber: this.paginator?.pageIndex + 1,
				sortActive: this.sort?.active || 'nome',
				sortDirection: this.sort?.direction || 'asc',
				pageSize: this.paginator?.pageSize,
				filter: filtro
			},
			...this.filter?.parametros
		}

		const data = await this._tecnicoService
			.obterPorParametros(params)
			.toPromise();

		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.TECNICO,
			entityParameters: this.filter?.parametros
		}

		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);

		this.isLoading = false;
	}

	paginar() {
		this.obterDados();
	}
}
