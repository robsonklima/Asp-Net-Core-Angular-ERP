import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { OrcamentoData, OrcamentoFaturamento, OrcamentoParameters } from 'app/core/types/orcamento.types';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { Subject } from 'rxjs';

@Component({
	selector: 'app-orcamento-financeiro-faturamento-lista',
	templateUrl: './orcamento-financeiro-faturamento-lista.component.html',
	styles: [`
    .list-grid-financeiro-faturamentos {
      grid-template-columns: 200px 70px 70px 100px 100px auto 130px 100px 70px 100px;
      
      /* @screen sm {
          grid-template-columns: 72px 155px auto 155px 155px 72px 155px 72px;
      }

      @screen md {
          grid-template-columns: 72px 155px auto 155px 155px 72px 155px 72px;
      }

      @screen lg {
          grid-template-columns: 72px 155px auto 155px 155px 72px 155px 72px;
      } */
    }  
  `],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class OrcamentoFinanceiroFaturamentoListaComponent extends Filterable implements AfterViewInit, IFilterable {
	@ViewChild('sidenav') sidenav: MatSidenav;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;

	@ViewChild(MatSort) sort: MatSort;

	dataSourceData: OrcamentoData;
	isLoading: boolean = false;
	protected _onDestroy = new Subject<void>();

	constructor(
		private _orcamentoSvc: OrcamentoService,
		private _cdr: ChangeDetectorRef,
		protected _userService: UserService
	) {
		super(_userService, 'orcamento');
	}

	ngAfterViewInit(): void {
		this.obterOrcamentos();
		this.registerEmitters();
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
			filter: filtro,
			isFaturamento: true,
		};

		let data: OrcamentoData = await this._orcamentoSvc
			.obterPorParametros(params)
			.toPromise();
		
		this.dataSourceData = data;
		this.isLoading = false;
	}

	faturar(orc: any){
		let orcamentoFaturamento: OrcamentoFaturamento = {
			codOrcamento: 0,
			codClienteBancada: '1',
			codFilial: 1,
			numOSPerto: 1,
			numOrcamento: '',
			descricaoNotaFiscal: '',
			valorPeca: orc.valorTotal,
			qtdePeca: 1,
			valorServico: '',
			numNF: orc.numNF,
			dataEmissaoNF: orc.dataEmissaoNF,
			indFaturado: 1,
			indRegistroDanfe: '0',
			caminhoDanfe: '',
			codUsuarioCad: '',
			dataHoraCad: ''
		}
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