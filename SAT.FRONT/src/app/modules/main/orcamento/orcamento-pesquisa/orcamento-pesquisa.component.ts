import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrcamentoData, OrcamentoTipoIntervencao } from 'app/core/types/orcamento.types';
import { OrdemServicoData } from 'app/core/types/ordem-servico.types';
import _ from 'lodash';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
	selector: 'app-ordem-servico-pesquisa',
	templateUrl: './orcamento-pesquisa.component.html',
	styles: [`
    .list-grid-pesquisa {
      grid-template-columns: 72px 100px 160px 158px auto 250px;
      
      /* @screen sm {
          grid-template-columns: 72px 90px 158px 158px auto 218px;
      }

      @screen md {
          grid-template-columns: 72px 90px 158px 158px auto 218px;
      }

      @screen lg {
          grid-template-columns: 72px 90px 158px 158px auto 218px;
      } */
    }  
  `],
	encapsulation: ViewEncapsulation.None,
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class OrcamentoPesquisaComponent implements OnInit, OnDestroy {
	@ViewChild(MatSort) private sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	form: FormGroup;
	dataSourceData: OrcamentoData;
	isLoading: boolean = false;
	private _unsubscribeAll: Subject<any> = new Subject<any>();

	constructor(
		private _formBuilder: FormBuilder,
		private _snack: CustomSnackbarService,
		private _cdr: ChangeDetectorRef,
		private _orcService: OrcamentoService,
		private _router: Router
	) { }

	ngOnInit(): void {
		this.form = this._formBuilder.group({
			codOS: [''],
			numOrc: [''],
			numOSCliente: [''],
			numOSQuarteirizada: [''],
			numRAT: [''],
			numAgencia: ['']
		});

		if (this.sort && this.paginator) {
			this.sort.sortChange.subscribe(() => {
				this.paginator.pageIndex = 0;
				this.pesquisar();
			});
		}

		this._cdr.detectChanges();
	}

	pesquisar() {
		const form = this.form.getRawValue();
		const isEmpty = Object.values(form).every(x => x === null || x === '');
		if (isEmpty) {
			this._snack.exibirToast('Favor informar sua pesquisa', 'warning');
			return;
		}

		this.obterOrcamentos();
	}

	monitorarDigitacaoForm(e) {
		if (e.keyCode === 13) {
			this.pesquisar()
		}
	}

	private obterOrcamentos() {
		const form = this.form.getRawValue();
		this.isLoading = true;

		this._orcService.obterPorParametros({
			pageNumber: this.paginator?.pageIndex + 1,
			sortActive: this.sort.active || 'numero',
			sortDirection: this.sort.direction || 'desc',
			pageSize: this.paginator?.pageSize,
			codigoOrdemServico: form.codOS,
			numRAT: form.codOS,
			numOSQuarteirizada: form.numOSQuarteirizada,
			numOSCliente: form.numOSCliente,
			filter: form.numOrc,

		}).subscribe((data: OrcamentoData) => {
			if (data.items.length === 1) {
				this._router.navigate([`orcamento/detalhe/${data.items[0].codOrc}`]);
				return;
			}

			if (data.items.length === 0) {
				this._snack.exibirToast('Nenhum orcamento encontrado', 'error');
			}

			this.dataSourceData = data;
			this.isLoading = false;
			this._cdr.detectChanges();
		}, () => {
			this.isLoading = false;
		});
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

	paginar() {
		this.pesquisar();
	}

	ngOnDestroy(): void {
		this._unsubscribeAll.next();
		this._unsubscribeAll.complete();
	}
}
