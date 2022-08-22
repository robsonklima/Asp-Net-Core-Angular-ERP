import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent, Subject } from 'rxjs';
import { OrcamentoFaturamentoService } from 'app/core/services/orcamento-faturamento.service';
import { OrcamentoFaturamento, OrcamentoFaturamentoData, OrcamentoFaturamentoParameters, OrcamentoFaturamentoViewModel } from 'app/core/types/orcamento-faturamento.types';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import moment from 'moment';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';

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
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild('searchInputControl') searchInputControl: ElementRef;
	@ViewChild(MatSort) sort: MatSort;
	dataSourceData: OrcamentoFaturamentoData;
	isLoading: boolean = false;
	protected _onDestroy = new Subject<void>();

	constructor(
		private _orcamentoFaturamentoSvc: OrcamentoFaturamentoService,
		private _cdr: ChangeDetectorRef,
		protected _userService: UserService,
		private _snack: CustomSnackbarService,
	) {
		super(_userService, 'orcamento');
	}

	ngAfterViewInit(): void {
		this.obterFaturamentos();
		this.registerEmitters();
		this._cdr.detectChanges();
	}

	registerEmitters(): void {
		fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
			map((event: any) => {
				return event.target.value;
			})
			, debounceTime(1000)
			, distinctUntilChanged()
		).subscribe((text: string) => {
			this.paginator.pageIndex = 0;
			this.obterFaturamentos(text);
		});
	}

	private async obterFaturamentos(filtro: string = '') {
		this.isLoading = true;

		const params: OrcamentoFaturamentoParameters = {
			pageNumber: this.paginator.pageIndex + 1,
			sortActive: this.filter?.parametros?.sortActive || this.sort.active || 'codOrc',
			sortDirection: this.filter?.parametros?.direction || this.sort.direction || 'desc',
			pageSize: this.filter?.parametros?.qtdPaginacaoLista ?? this.paginator?.pageSize,
			filter: filtro
		};

		let data: OrcamentoFaturamentoData = await this._orcamentoFaturamentoSvc
			.obterPorParametros(params)
			.toPromise();
		this.dataSourceData = data;
		this.isLoading = false;
	}

	salvar(faturamentoVM: OrcamentoFaturamentoViewModel) {
		if (!faturamentoVM.numNF) {
			this._snack.exibirToast('Favor informar o número da NF', 'error');
			return;			
		}
		
		if (!faturamentoVM.dataEmissao) {
			this._snack.exibirToast('Favor informar a data de emissão da NF', 'error');			
			return;
		}

		const faturamento: OrcamentoFaturamento = {
			codOrcamentoFaturamento: faturamentoVM.codigo,
			caminhoDanfe: faturamentoVM.caminhoDanfe,
			codClienteBancada: faturamentoVM.codClienteBancada,
			codFilial: faturamentoVM.codFilial,
			codOrcamento: faturamentoVM.codOrc,
			indFaturado: 1,
			dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
			codUsuarioCad: this.userSession.usuario.codUsuario,
			dataEmissaoNF: faturamentoVM.dataEmissao,
			descricaoNotaFiscal: faturamentoVM.descNF,
			indRegistroDanfe: faturamentoVM.indRegistroDanfe,
			numNF: faturamentoVM.numNF,
			numOrcamento: faturamentoVM.numOrcamento,
			numOSPerto: faturamentoVM.codOS,
			qtdePeca: faturamentoVM.qtdePeca,
			valorPeca: faturamentoVM.valorPeca,
			valorServico: faturamentoVM.valorServico
		};

		if (faturamentoVM.indFaturado)
			this.atualizar(faturamento);
		else
			this.criar(faturamento)
	}

	criar(faturamento: OrcamentoFaturamento) {
		this._orcamentoFaturamentoSvc.criar(faturamento).subscribe(() => {
			this._snack.exibirToast("Registro criado com sucesso!", "success");
		});
	}

	atualizar(faturamento: OrcamentoFaturamento) {
		let obj = {
			...faturamento,
			...{
				indFaturado: 1,
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario.codUsuario
			}
		}

		this._orcamentoFaturamentoSvc.atualizar(faturamento).subscribe(() => {
			this._snack.exibirToast("Registro atualizado com sucesso!", "success");
		});
	}

	paginar() {
		this.onPaginationChanged();
		this.obterFaturamentos();
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}