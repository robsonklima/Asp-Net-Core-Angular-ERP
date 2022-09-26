import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { Auditoria, AuditoriaData, AuditoriaParameters } from 'app/core/types/auditoria.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-auditoria-lista',
	templateUrl: './auditoria-lista.component.html',
	styles: [
		/* language=SCSS */
		`
      .list-grid-auditoria {
		grid-template-columns: 142px 15% 10% auto 10% 15%;
      
      @screen sm {
		grid-template-columns: 142px 15% 10% auto 10% 15%;
      }
  
      @screen md {
		grid-template-columns: 142px 15% 10% auto 10% 15%;
      }
  
      @screen lg {
		grid-template-columns: 142px 15% 10% auto 10% 15%;
      }
      }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})

export class AuditoriaListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	dataSourceData: AuditoriaData;
	pendencia: boolean = false;
	isLoading: boolean = false;
	selectedItem: Auditoria | null = null;
	userSession: UserSession;

	constructor(
		protected _userService: UserService,
		private _exportacaoService: ExportacaoService,
		private _cdr: ChangeDetectorRef,
		private _route: ActivatedRoute,
		private _auditoriaService: AuditoriaService
	) {
		super(_userService, 'auditoria')
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
		this.isLoading = true;
		this.registerEmitters();
		this.obterDados();

		fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
			map((event: any) => {
				return event.target.value;
			})
			, debounceTime(1000)
			, distinctUntilChanged()
		).subscribe((text: string) => {
			this.paginator.pageIndex = 0;
			this.obterDados(text);
		});

		if (this.sort && this.paginator) {
			this.sort.disableClear = true;
			this._cdr.markForCheck();

			this.sort.sortChange.subscribe(() => {
				this.onSortChanged();
				this.obterDados();
			});
		}

		this._cdr.detectChanges();
	}

	async obterDados(filtro: string = '') {
		this.isLoading = true;

		const parametros: AuditoriaParameters = {

			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: this.sort.active || 'codAuditoria',
			sortDirection: 'desc',
			pageSize: this.paginator?.pageSize,
			filter: filtro
		}

		const data: AuditoriaData = await this._auditoriaService.obterPorParametros({
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
			tipoArquivo: ExportacaoTipoEnum.AUDITORIA,
			entityParameters: this.filter?.parametros
		}
		await this._exportacaoService.exportar(FileMime.Excel,exportacaoParam);
		this.isLoading = false;
	}

}
