import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { AcaoService } from 'app/core/services/acao.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { AcaoData, AcaoParameters } from 'app/core/types/acao.types';
import { FileMime } from 'app/core/types/file.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent, interval, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';



@Component({
	selector: 'app-acao-lista',
	templateUrl: './acao-lista.component.html',
	styles: [
		/* language=SCSS */
		`
    .list-grid-u {
      grid-template-columns: 142px 80px 50% 25% 42px;
      
      @screen sm {
          grid-template-columns: 142px 80px 50% 25% 42px;
      }
  
      @screen md {
          grid-template-columns: 142px 80px 50% 25% 42px;
      }
  
      @screen lg {
          grid-template-columns: 142px 80px 50% 25% 42px;
      }
  }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class AcaoListaComponent extends Filterable implements AfterViewInit, IFilterable {

	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: AcaoData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl', { read: ElementRef }) searchInputControl: ElementRef;
	userSession: UserSession;
	protected _onDestroy = new Subject<void>();

	constructor(
		private _acaoService: AcaoService,
		private _cdr: ChangeDetectorRef,
		private _exportacaoService: ExportacaoService,
		protected _userService: UserService
	) {
		super(_userService, 'acao')
		//this.userSession = JSON.parse(this._userService.userSession);
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

	async ngAfterViewInit() {
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

	ngOnInit(): void {

	}

	async obterDados(filtro: string = '') {
		this.isLoading = true;
		const parametros: AcaoParameters = {
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: 'codEAcao',
			sortDirection: 'asc',
			pageSize: this.paginator?.pageSize,
			filter: filtro
		}

		const data: AcaoData = await this._acaoService.obterPorParametros({
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
			tipoArquivo: ExportacaoTipoEnum.ACAO,
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
