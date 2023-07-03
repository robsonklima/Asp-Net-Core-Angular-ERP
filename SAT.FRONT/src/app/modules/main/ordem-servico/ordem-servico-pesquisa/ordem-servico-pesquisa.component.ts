import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { ClienteService } from 'app/core/services/cliente.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { OrdemServicoData } from 'app/core/types/ordem-servico.types';
import { statusConst } from 'app/core/types/status-types';
import { TipoIntervencaoEnum } from 'app/core/types/tipo-intervencao.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
	selector: 'app-ordem-servico-pesquisa',
	templateUrl: './ordem-servico-pesquisa.component.html',
	styles: [`
    .list-grid-pesquisa {
      grid-template-columns: 72px 90px 158px 158px auto 218px;
      
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
export class OrdemServicoPesquisaComponent implements OnInit, OnDestroy {
	@ViewChild(MatSort) private sort: MatSort;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	form: FormGroup;
	dataSourceData: OrdemServicoData;
	isLoading: boolean = false;
	userSession: UserSession;
	clientes: Cliente[] = [];
	validaCliente: boolean = this._userService.isCustomer;
	clienteFilterCtrl: FormControl = new FormControl();
	private _unsubscribeAll: Subject<any> = new Subject<any>();
	protected _onDestroy = new Subject<void>();

	constructor(
		private _formBuilder: FormBuilder,
		private _snack: CustomSnackbarService,
		private _cdr: ChangeDetectorRef,
		private _osSvc: OrdemServicoService,
		private _clienteService: ClienteService,
		private _router: Router,
		private _userService: UserService
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngOnInit(): void {
		this.criarForm();

		if (this.sort && this.paginator) {
			this.sort.sortChange.subscribe(() => {
				this.paginator.pageIndex = 0;
				this.pesquisar();
			});
		}

		this.clienteFilterCtrl.valueChanges
		.pipe(
			takeUntil(this._onDestroy),
			debounceTime(700),
			distinctUntilChanged()
		)
		.subscribe(() => {
			this.obterClientes(this.clienteFilterCtrl.value);
		});

		this.obterClientes();
		this._cdr.detectChanges();
	}

	criarForm() {
		this.form = this._formBuilder.group({
			codOS: [''],
			numSerie: [''],
			numOSCliente: [''],
			numOSQuarteirizada: [''],
			dataFechamentoFim: [''],
			dataFechamentoInicio: [''],
			numRAT: [''],
			numAgencia: [''],
			codClientes: ['']
		});
	}

	pesquisar() {
		const form = this.form.getRawValue();
		const isEmpty = Object.values(form).every(x => x === null || x === '' || x === undefined);

		if (isEmpty) {
			this._snack.exibirToast('Favor informar sua pesquisa', 'warning');
			return;
		}
		else
			this.obterChamados();
	}

	async obterClientes(filtro: string = '') {
		let params: ClienteParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeFantasia',
			sortDirection: 'asc',
			codCliente: this.userSession?.usuario?.codCliente,
			pageSize: 1000
		};

		const data = await this._clienteService
			.obterPorParametros(params)
			.toPromise();

		this.clientes = data.items;
	}

	monitorarDigitacaoForm(e) {
		if (e.keyCode === 13)
			this.pesquisar()
	}

	private obterChamados() {
		const form = this.form.getRawValue();
		this.isLoading = true;

		let codClientes;
		if(this.userSession.usuario.codCliente)
			codClientes = this.userSession.usuario.codCliente.toString();

		else
			if (form.codClientes.length)
				codClientes = form.codClientes?.join(',');

		this._osSvc.obterPorParametros({
			...{
				pageNumber: this.paginator?.pageIndex + 1,
				sortActive: this.sort.active || 'codOS',
				sortDirection: this.sort.direction || 'desc',
				pageSize: this.paginator?.pageSize,
				codTiposIntervencao: this.validaCliente ? `${TipoIntervencaoEnum.CORRETIVA},
															${TipoIntervencaoEnum.COFRE},
															${TipoIntervencaoEnum.PREVENTIVA},
															${TipoIntervencaoEnum.ORCAMENTO},
															${TipoIntervencaoEnum.ORC_APROVADO},
															${TipoIntervencaoEnum.ORC_REPROVADO},
															${TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE},
															${TipoIntervencaoEnum.MANUTENÇÃO_GERENCIAL}` : null,
				codClientes: codClientes
			},
			...form
		}).subscribe((data: OrdemServicoData) => {
			if (data.items.length === 1) {
				this._router.navigate([`ordem-servico/detalhe/${data.items[0].codOS}`]);
				return;
			}

			if (data.items.length === 0) {
				this._snack.exibirToast('Nenhum chamado encontrado', 'error');
			}

			this.dataSourceData = data;
			this.isLoading = false;
			this._cdr.detectChanges();
		}, () => {
			this.isLoading = false;
		});
	}

	paginar() {
		this.pesquisar();
	}

	ngOnDestroy(): void {
		this._unsubscribeAll.next();
		this._unsubscribeAll.complete();
	}
}
