import { TipoIndiceReajusteService } from './../../../../../core/services/tipo-indice-reajuste.service';
import { TipoContratoService } from './../../../../../core/services/tipo-contrato.service';
import { Contrato } from './../../../../../core/types/contrato.types';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClienteService } from 'app/core/services/cliente.service';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Cliente } from 'app/core/types/cliente.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { TipoContrato } from 'app/core/types/tipo-contrato.types';
import { TipoIndiceReajuste } from 'app/core/types/tipo-indice-reajuste.types';
import { ContratoReajuste } from 'app/core/types/contrato-reajuste.types';
import { ContratoReajusteService } from 'app/core/services/contrato-reajuste.service';

@Component({
	selector: 'app-contrato-form',
	templateUrl: './contrato-form.component.html',
})
export class ContratoFormComponent implements OnInit {
	codContrato: number;
	contrato: Contrato;
	contratoReajuste: ContratoReajuste;
	form: FormGroup;
	isAddMode: boolean;
	userSession: UsuarioSessao;
	clientes: Cliente[] = [];
	searching: boolean;
	protected _onDestroy = new Subject<void>();
	tipoContrato: TipoContrato[];
	tipoReajuste: TipoIndiceReajuste[];

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
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.codContrato = +this._route.snapshot.paramMap.get('codContrato');
		this.isAddMode = !this.codContrato;
		this.inicializarForm();

		// Main Obj
		await this.obterClientes();
		await this.obterDados();
	}


	private async obterDados() {
		this.tipoContrato = (await this._tipoContratoService.obterPorParametros({}).toPromise()).items;
		this.tipoReajuste = (await this._tipoIndiceReajusteService.obterPorParametros({}).toPromise()).items;
		this.contratoReajuste = (await this._contratoReajusteService
			.obterPorParametros({ codContrato: this.codContrato, indAtivo: 1 })
			.toPromise()).items.pop();
		if (!this.isAddMode) {
			let data = await this._contratoService.obterPorCodigo(this.codContrato).toPromise();
			this.contrato = data;

			this.form.patchValue(this.contrato);
			this.form.patchValue(this.contratoReajuste);
		}
	}

	private async obterClientes(filter: string = '') {
		this.clientes = (await this._clienteService.obterPorParametros({
			filter: filter,
			indAtivo: 1,
			pageSize: 500,
			sortActive: 'nomeFantasia',
			sortDirection: 'asc'
		}).toPromise()).items;
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
			console.log(form);
			console.log(ct);
			
			let ctReajs: ContratoReajuste = {
				codContrato: ct.codContrato,
				codTipoIndiceReajuste: form.codTipoIndiceReajuste,
				percReajuste: form.percReajuste,
				indAtivo: 1,
				codUsuarioCad: obj.codUsuarioManut,
				dataHoraCad: obj.dataHoraManut
			}

			this._contratoReajusteService.atualizar(ctReajs).subscribe();
			this._snack.exibirToast("Registro atualizado com sucesso!", "success");
			this.form.enable();
		});
	}

	private criar(): void {
		const form: any = this.form.getRawValue();
		let obj = {
			...this.contrato,
			...form,
			...{
				indAtivo: 1,
				dataHoraSolicitacao: moment().format('YYYY-MM-DD HH:mm:ss'),
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario?.codUsuario,
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
				indAtivo: 1,
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
			nomeResponsavelPerto: [undefined, Validators.required],
			nomeResponsavelCliente: [undefined, Validators.required],
			objetoContrato: [undefined],
			semCobertura: [undefined],
			valTotalContrato: [undefined, Validators.required],
			indPermitePecaEspecifica: [undefined, Validators.required],
			numDiasSubstEquip: [undefined]

		});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
