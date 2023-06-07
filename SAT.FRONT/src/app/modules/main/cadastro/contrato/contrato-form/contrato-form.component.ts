import { TipoIndiceReajusteService } from './../../../../../core/services/tipo-indice-reajuste.service';
import { TipoContratoService } from './../../../../../core/services/tipo-contrato.service';
import { Contrato } from './../../../../../core/types/contrato.types';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClienteService } from 'app/core/services/cliente.service';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Cliente } from 'app/core/types/cliente.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { TipoContrato } from 'app/core/types/tipo-contrato.types';
import { TipoIndiceReajuste } from 'app/core/types/tipo-indice-reajuste.types';
import { ContratoReajuste } from 'app/core/types/contrato-reajuste.types';
import { ContratoReajusteService } from 'app/core/services/contrato-reajuste.service';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { statusConst } from 'app/core/types/status-types';
import { OrcFormaPagamento } from 'app/core/types/orcamento-forma-pagamento.types';
import { PosVenda } from 'app/core/types/pos-venda.types';
import { PosVendaService } from 'app/core/services/pos-venda.service';
import { OrcFormaPagamentoService } from 'app/core/services/orcamento-forma-pagamento.service';
import { OrcDadosBancarios } from 'app/core/types/orcamento-dados-bancarios.types';
import { OrcDadosBancariosService } from 'app/core/services/orcamento-dados-bancarios.service';
import moment from 'moment';

@Component({
	selector: 'app-contrato-form',
	templateUrl: './contrato-form.component.html',
})
export class ContratoFormComponent implements OnInit {
	@Output() cod = new EventEmitter();
	codContrato: number;
	contrato: Contrato;
	contratoReajuste: ContratoReajuste;
	form: FormGroup;
	isAddMode: boolean;
	isLoading: boolean;
	userSession: UsuarioSessao;
	clientes: Cliente[] = [];
	searching: boolean;
	protected _onDestroy = new Subject<void>();
	tipoContrato: TipoContrato[];
	tipoReajuste: TipoIndiceReajuste[];
	clienteFilterCtrl: FormControl = new FormControl();
	posVendas: PosVenda[] = [];
	orcFormasPagamento: OrcFormaPagamento[] = [];
	orcDadosBancarios: OrcDadosBancarios[] = [];

	constructor(
		private _formBuilder: FormBuilder,
		private _route: ActivatedRoute,
		private _router: Router,
		private _userService: UserService,
		private _contratoService: ContratoService,
		private _contratoReajusteService: ContratoReajusteService,
		private _snack: CustomSnackbarService,
		private _clienteService: ClienteService,
		private _tipoContratoService: TipoContratoService,
		private _tipoIndiceReajusteService: TipoIndiceReajusteService,
		private _posVendaService: PosVendaService,
		private _orcFormaPagamentoService: OrcFormaPagamentoService,
		private _orcDadosBancariosService: OrcDadosBancariosService,
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.isLoading = true;
		this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
		this.isAddMode = !this.codContrato;
		this.inicializarForm();
		await this.obterClientes();
		await this.obterDados();
		this.isLoading= false;
	}

	private async obterDados() {
		this.tipoContrato = (await this._tipoContratoService.obterPorParametros({}).toPromise()).items;
		this.tipoReajuste = (await this._tipoIndiceReajusteService.obterPorParametros({}).toPromise()).items;
		this.posVendas = (await this._posVendaService.obterPorParametros({}).toPromise()).items;
		this.orcFormasPagamento = (await this._orcFormaPagamentoService.obterPorParametros({}).toPromise()).items;
		this.orcDadosBancarios = (await this._orcDadosBancariosService.obterPorParametros({}).toPromise()).items;
		this.contratoReajuste = (await this._contratoReajusteService
			.obterPorParametros({ codContrato: this.codContrato, indAtivo: 1 })
			.toPromise()).items.pop();

		if (!this.isAddMode) {
			let data = await this._contratoService.obterPorCodigo(this.codContrato).toPromise();
			this.contrato = data;
			this.cod.emit(data.nroContrato);
			this.form.patchValue({ ...this.contratoReajuste, ...this.contrato });
		}
	}

	private async obterClientes(filter: string = '') {
		this.clientes = (await this._clienteService.obterPorParametros({
			filter: filter,
			indAtivo: statusConst.ATIVO,
			pageSize: 500,
			sortActive: 'nomeFantasia',
			sortDirection: 'asc'
		}).toPromise()).items;

		this.clienteFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(400),
				distinctUntilChanged()
			)
			.subscribe(() =>
			{
				this.obterClientes(this.clienteFilterCtrl.value);
			});
	}

	salvar(): void {
		this.isAddMode ? this.criar() : this.atualizar();
	}

	private atualizar(): void {
		this.form.disable();	

		const form: any = this.form.getRawValue();
		let obj = {
			...this.contrato,
			...form,
			...{
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario?.codUsuario
			}
		};

		Object.keys(obj).forEach((key) => {
			typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		});
		
		this._contratoService.atualizar(obj).subscribe((ct) => {
	
			let ctReajs: ContratoReajuste = {
				codContrato: ct.codContrato,
				codTipoIndiceReajuste: form.codTipoIndiceReajuste,
				percReajuste: form.percReajuste,
				indAtivo: statusConst.ATIVO,
				codUsuarioCad: obj.codUsuarioManut,
				dataHoraCad: obj.dataHoraManut
			}

			this._contratoReajusteService.atualizar(ctReajs).subscribe();
			this._snack.exibirToast("Registro atualizado com sucesso!", "success");
			this.cod.emit(ct.nroContrato);
			this.form.enable();
		});
	}

	private criar(): void {
		const form: any = this.form.getRawValue();
		let obj = {
			...this.contrato,
			...form,
			...{
				indAtivo: statusConst.ATIVO,
				dataHoraSolicitacao: moment().format('YYYY-MM-DD HH:mm:ss'),
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario?.codUsuario,
				mtbfnominal: 70
			}
		};

		Object.keys(obj).forEach((key) => {
			typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
		});

		this._contratoService.criar(obj).subscribe((ct) => {
			
			let ctReajs: ContratoReajuste = {
				codContrato: ct.codContrato,
				codTipoIndiceReajuste: form.codTipoIndiceReajuste,
				percReajuste: form.percReajuste,
				indAtivo: statusConst.ATIVO,
				codUsuarioCad: obj.codUsuarioCad,
				dataHoraCad: obj.dataHoraCad
			}

			this._contratoReajusteService.criar(ctReajs).subscribe();
			this._snack.exibirToast("Registro adicionado com sucesso!", "success");
			this._router.navigate(['contrato/' + ct.codContrato]);
		});
	}

	private inicializarForm(): void {
		this.form = this._formBuilder.group({
			codContrato: [
				{
					value: undefined,
					disabled: true,
				}, [Validators.required]
			],
			codContratoPai: [undefined],
			codCliente: [undefined, Validators.required],
			codTipoContrato: [undefined, Validators.required],
			codTipoIndiceReajuste: [undefined, Validators.required],
			percReajuste: [undefined],
			nroContrato: [undefined, Validators.required],
			nomeContrato: [undefined, Validators.required],
			dataContrato: [undefined, Validators.required],
			dataAssinatura: [undefined, Validators.required],
			dataInicioVigencia: [undefined, Validators.required],
			dataFimVigencia: [undefined, Validators.required],
			dataInicioPeriodoReajuste: [undefined],
			dataFimPeriodoReajuste: [undefined],
			nomeResponsavelPerto: [undefined],
			nomeResponsavelCliente: [undefined, Validators.required],
			objetoContrato: [undefined],
			semCobertura: [undefined],
			valTotalContrato: [undefined, Validators.required],
			indPermitePecaEspecifica: [undefined],
			numDiasSubstEquip: [undefined],
			codPosVenda: [undefined, Validators.required],
			codOrcFormaPagamento: [undefined, Validators.required],
			codOrcDadosBancarios: [undefined, Validators.required],
			indPedido: [undefined],
		});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}