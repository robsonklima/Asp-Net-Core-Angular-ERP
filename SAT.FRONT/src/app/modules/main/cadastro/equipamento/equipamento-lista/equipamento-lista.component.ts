import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { EquipamentoData, EquipamentoParameters } from 'app/core/types/equipamento.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-equipamento-lista',
	templateUrl: './equipamento-lista.component.html',
	styles: [`
    .list-grid {
      grid-template-columns: 72px 245px 245px auto;
      
      @screen sm {
          grid-template-columns: 72px 245px 245px auto;
      }

      @screen md {
          grid-template-columns: 72px 245px 245px auto;
      }

      @screen lg {
          grid-template-columns: 72px 245px 245px auto;
      }
    }  
  `],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class EquipamentoListaComponent extends Filterable implements AfterViewInit, IFilterable {

	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: EquipamentoData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	userSession: UserSession;

	constructor(
		private _cdr: ChangeDetectorRef,
		private _equipamentoService: EquipamentoService,
		private _exportacaoService: ExportacaoService,
		protected _userService: UserService
	) {
		super(_userService, 'equipamento')
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

		const params: EquipamentoParameters = {
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: this.sort?.active,
			sortDirection: this.sort?.direction || 'asc',
			pageSize: this.paginator?.pageSize,
			filter: filtro
		};

		const data: EquipamentoData = await this._equipamentoService.obterPorParametros({
			...params,
			...this.filter?.parametros
		}).toPromise();
		this.dataSourceData = data;
		this.isLoading = false;
		this._cdr.detectChanges();
	}

	public async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.PDF,
			tipoArquivo: ExportacaoTipoEnum.EQUIPAMENTO,
			entityParameters: {}
		}
		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
		this.isLoading = false;
	}

	paginar() {
		this.obterDados();
	}
}
