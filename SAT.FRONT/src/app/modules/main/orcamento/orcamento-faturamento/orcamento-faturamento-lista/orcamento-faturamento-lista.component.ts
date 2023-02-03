import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { LocalEnvioNFFaturamentoService } from 'app/core/services/local-envio-nf-faturamento.service';
import { LocalEnvioNFFaturamento, LocalEnvioNFFaturamentoData, LocalEnvioNFFaturamentoParameters } from 'app/core/types/local-envio-nf-faturamento.types';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
	selector: 'app-orcamento-faturamento-lista',
	templateUrl: './orcamento-faturamento-lista.component.html',
	styles: [`
    .list-grid-faturamentos {
      grid-template-columns: 60px 155px auto 200px 150px 150px 160px 150px;
      
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
export class OrcamentoFaturamentoListaComponent implements AfterViewInit {
	[x: string]: any;

	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) private sort: MatSort;
	dataSourceData: LocalEnvioNFFaturamentoData;
	isLoading: boolean = false;
	@ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
	selectedItem: LocalEnvioNFFaturamento | null = null;
	userSession: UserSession;

	constructor(
		private _cdr: ChangeDetectorRef,
		private _localEnvioNFFaturamentoService: LocalEnvioNFFaturamentoService
	) { }

	ngAfterViewInit() {
		this.obterDados();

		if (this.sort && this.paginator) {
			this.sort.disableClear = true;
			this._cdr.markForCheck();

			fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
				map((event: any) => {
					return event.target.value;
				})
				, debounceTime(1000)
				, distinctUntilChanged()
			).subscribe(() => {
				this.paginator.pageIndex = 0;
				this.obterDados();
			});

			this.sort.sortChange.subscribe(() => {
				this.onSortChanged()
				this.obterDados();
			});
		}

		this._cdr.detectChanges();
	}

	async obterDados() {
		this.isLoading = true;

		const parametros: LocalEnvioNFFaturamentoParameters = {

			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: this.sort.active || 'codLocalEnvioNFFaturamento' || 'cnpjFaturamento',
			sortDirection: 'desc',
			pageSize: this.paginator?.pageSize,
			filter: this.searchInputControl.nativeElement.value
		}

		const data: LocalEnvioNFFaturamentoData = await this._localEnvioNFFaturamentoService.obterPorParametros({
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
}