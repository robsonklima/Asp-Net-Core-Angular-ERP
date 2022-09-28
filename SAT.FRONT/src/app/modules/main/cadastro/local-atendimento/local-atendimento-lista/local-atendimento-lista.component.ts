import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { LocalAtendimentoData, LocalAtendimentoParameters } from 'app/core/types/local-atendimento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-local-atendimento-lista',
	templateUrl: './local-atendimento-lista.component.html',
	styles: [
		/* language=SCSS */
		`
      .list-grid-la {
          grid-template-columns: 72px 72px 56px auto 112px 112px 112px 112px 42px;
          
          /* @screen sm {
              grid-template-columns: 72px 72px 56px auto 112px 112px 112px 112px 42px;
          }
      
          @screen md {
              grid-template-columns: 72px 72px 56px auto 112px 112px 112px 112px 42px;
          }
      
          @screen lg {
              grid-template-columns: 72px 72px 56px auto 112px 112px 112px 112px 42px;
          } */
      }
    `
	],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class LocalAtendimentoListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') public sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: LocalAtendimentoData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	userSession: UserSession;

	constructor(
		private _cdr: ChangeDetectorRef,
		private _localAtendimentoService: LocalAtendimentoService,
		protected _userService: UserService,
		private _exportacaoService: ExportacaoService
	) {
		super(_userService, 'local-atendimento')
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
				this.obterDados(text);
				debugger
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
		
		const params: LocalAtendimentoParameters = {
			...{
				pageNumber: this.paginator?.pageIndex + 1,
				sortActive: this.sort.active || 'nomeLocal' || 'numAgencia',
				sortDirection: this.sort?.direction || 'asc',
				pageSize: this.paginator?.pageSize,
				filter: filtro
			},
			...this.filter?.parametros
		}

		const data = await this._localAtendimentoService
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
			tipoArquivo: ExportacaoTipoEnum.LOCALATENDIMENTO,
			entityParameters: this.filter?.parametros
		}

		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);

		this.isLoading = false;
	}

	paginar() {
		this.obterDados();
	}
}
