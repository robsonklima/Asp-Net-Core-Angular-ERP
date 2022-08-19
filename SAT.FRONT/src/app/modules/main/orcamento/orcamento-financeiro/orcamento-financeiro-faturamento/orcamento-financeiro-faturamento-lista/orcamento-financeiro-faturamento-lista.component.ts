import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent, Subject } from 'rxjs';
import { OrcamentoFaturamentoService } from 'app/core/services/orcamento-faturamento.service';
import { OrcamentoFaturamentoData, OrcamentoFaturamentoParameters, OrcamentoFaturamentoViewModel } from 'app/core/types/orcamento-faturamento.types';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import moment from 'moment';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Router } from '@angular/router';

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
		private _router: Router
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
		if (faturamentoVM.indFaturado) 
			this.atualizar(faturamentoVM);		
		else 
			this.criar(faturamentoVM)
	}

	criar(faturamentoVM: OrcamentoFaturamentoViewModel){	
		let obj = {
			...faturamentoVM,
			...{
				indFaturado: 1,
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario.codUsuario,
			}
		}

	}

	atualizar(faturamentoVM: OrcamentoFaturamentoViewModel){
		let obj = {
			...faturamentoVM,
			...{
				indFaturado: 1,
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario.codUsuario,
				codOrcamentoFaturamento: faturamentoVM.codigo
			}
		}

		console.log(obj);		
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