import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { OrcamentoParameters, OrcamentoTipoIntervencao, ViewOrcamentoListaData } from 'app/core/types/orcamento.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-orcamento-lista',
	templateUrl: './orcamento-lista.component.html',
	styles: [`
        .list-grid-orcamentos {
            grid-template-columns: 55px 72px 48px 80px 110px 90px auto 120px 60px 120px 90px 130px 70px 70px;
            
            /* @screen md {
              grid-template-columns: 48px 72px 48px 80px 118px 72px auto 148px 48px 120px;
            }

            @screen lg {
              grid-template-columns: 48px 72px 48px 80px 118px 118px auto 72px 48px 120px 148px 70px 120px;
          } */
        }
    `],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class OrcamentoListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	dataSourceData: ViewOrcamentoListaData;
	isLoading: boolean = false;
	protected _onDestroy = new Subject<void>();

	constructor(
		private _orcamentoSvc: OrcamentoService,
		private _cdr: ChangeDetectorRef,
		private _exportacaoService: ExportacaoService,
		protected _userService: UserService
	) {
		super(_userService, 'orcamento');
	}

	ngAfterViewInit(): void {
		this.obterOrcamentos();
		this.registerEmitters();

		fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
			map((event: any) => {
				return event.target.value;
			})
			, debounceTime(1000)
			, distinctUntilChanged()
		).subscribe((text: string) => {
			this.paginator.pageIndex = 0;
			this.obterOrcamentos(text);
		});

		if (this.sort && this.paginator) {
			this.sort.disableClear = true;
			this._cdr.markForCheck();

			this.sort.sortChange.subscribe(() => {
				this.onSortChanged();
				this.obterOrcamentos();
			});
		}

		this._cdr.detectChanges();
	}

	registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterOrcamentos();
		})
	}

	private async obterOrcamentos(filtro: string = '') {
		this.isLoading = true;

		const params: OrcamentoParameters = {
			pageNumber: this.paginator.pageIndex + 1,
			sortActive: this.filter?.parametros?.sortActive || this.sort.active || 'codOrc',
			sortDirection: this.filter?.parametros?.direction || this.sort.direction || 'desc',
			pageSize: this.filter?.parametros?.qtdPaginacaoLista ?? this.paginator?.pageSize,
			filter: filtro
		};

		const data: ViewOrcamentoListaData = await this._orcamentoSvc
			.obterPorView({
				...params,
				...this.filter?.parametros
			})
			.toPromise();

		this.dataSourceData = data;
		this.isLoading = false;
	}

	obterCorFundoIntervencao(codTipoIntervencao: number) {
		switch (codTipoIntervencao) {
			case OrcamentoTipoIntervencao.ORCAMENTO_APROVADO:
				return 'blue'

			case OrcamentoTipoIntervencao.ORCAMENTO:
				return 'pink'

			case OrcamentoTipoIntervencao.ORCAMENTO_REPROVADO:
				return 'gray'

			default:
				break;
		}
	}

	obterCorFonteIntervencao(codTipoIntervencao: number) {
		if (codTipoIntervencao == OrcamentoTipoIntervencao.ORCAMENTO_APROVADO ||
			codTipoIntervencao == OrcamentoTipoIntervencao.ORCAMENTO_REPROVADO) {
			return 'white'
		}
	}

	public async exportar() {
		this.isLoading = true;

		let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.ORCAMENTO,
			entityParameters: {
				...this.filter?.parametros,
				sortDirection: 'desc',
				pageSize: 100000
			}
		}

		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
		this.isLoading = false;
	}

	paginar() {
		this.onPaginationChanged();
		this.obterOrcamentos();
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
