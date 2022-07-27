import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { first } from 'rxjs/internal/operators/first';
import { LocalEnvioNFFaturamento } from 'app/core/types/local-envio-nf-faturamento.types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { LocalEnvioNFFaturamentoService } from 'app/core/services/local-envio-nf-faturamento.service';
import { ClienteService } from 'app/core/services/cliente.service';
import { ContratoService } from 'app/core/services/contrato.service';
import { statusConst } from 'app/core/types/status-types';
import { debounceTime, distinctUntilChanged, takeUntil, timeInterval } from 'rxjs/operators';
import { GeolocalizacaoService } from 'app/core/services/geolocalizacao.service';
import { GeolocalizacaoServiceEnum } from 'app/core/types/geolocalizacao.types';

@Component({
	selector: 'app-orcamento-faturamento-form',
	templateUrl: './orcamento-faturamento-form.component.html'
})
export class OrcamentoFaturamentoFormComponent implements OnInit {
	codLocalEnvioNFFaturamento: number;
	localEnvioNFFaturamento: LocalEnvioNFFaturamento;
	clientes: Cliente[] = [];
	contratos: Contrato[] = [];
	isAddMode: boolean;
	form: FormGroup;
	userSession: UsuarioSessao;
	clienteFilterCtrl: FormControl = new FormControl();
	contratoFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
		private _formBuilder: FormBuilder,
		private _snack: CustomSnackbarService,
		private _route: ActivatedRoute,
		private _userService: UserService,
		private _localEnvioNFFaturamentoService: LocalEnvioNFFaturamentoService,
		private _clienteService: ClienteService,
		private _contratoService: ContratoService,
		private _location: Location,
		private _geolocalizacaoService: GeolocalizacaoService
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.codLocalEnvioNFFaturamento = +this._route.snapshot.paramMap.get('codLocalEnvioNFFaturamento');
		this.isAddMode = !this.codLocalEnvioNFFaturamento;

		this.obterClientes();
		this.obterContratos();

		this.form = this._formBuilder.group({
			codLocalEnvioNFFaturamento: [
				{
					value: undefined,
					disabled: true
				}, Validators.required
			],
			codCliente: [undefined, Validators.required],
			codContrato: [undefined, Validators.required],
			cepFaturamento: ['', Validators.required],
			enderecoFaturamento: [undefined, Validators.required],
			complementoFaturamento: [undefined],
			numeroFaturamento: [undefined, Validators.required],
			bairroFaturamento: [undefined, Validators.required],
			nomeCidadeFaturamento: [undefined, Validators.required],
			siglaUFFaturamento: [undefined, Validators.required],
			cnpjFaturamento: [undefined, Validators.required],
			inscricaoEstadualFaturamento: [undefined, Validators.required],
			responsavelFaturamento: [undefined, Validators.required],
			emailFaturamento: [undefined, Validators.required],
			foneFaturamento: [undefined, Validators.required],
			faxFaturamento: [undefined],
			razaoSocialFaturamento: [undefined, Validators.required],
			cepEnvioNF: ['', Validators.required],
			enderecoEnvioNF: [undefined, Validators.required],
			complementoEnvioNF: [undefined],
			numeroEnvioNF: [undefined, Validators.required],
			bairroEnvioNF: [undefined, Validators.required],
			nomeCidadeEnvioNF: [undefined, Validators.required],
			siglaUFEnvioNF: [undefined, Validators.required],
			cnpjEnvioNF: [undefined, Validators.required],
			inscricaoEstadualEnvioNF: [undefined, Validators.required],
			responsavelEnvioNF: [undefined, Validators.required],
			emailEnvioNF: [undefined, Validators.required],
			foneEnvioNF: [undefined, Validators.required],
			faxEnvioNF: [undefined],
			razaoSocialEnvioNF: [undefined, Validators.required],
		});

		if (!this.isAddMode) {
			this._localEnvioNFFaturamentoService.obterPorCodigo(this.codLocalEnvioNFFaturamento)
				.pipe(first())
				.subscribe(data => {
					this.registrarEmitters();
					this.obterClientes(data.cliente.nomeFantasia);
					this.obterContratos(data.contrato.nomeContrato);
					this.localEnvioNFFaturamento = data;
					this.form.patchValue(data);
					this.form.controls['codContrato'].setValue(data.contrato.codContrato)
				});
		}
	}

	salvar(): void {
		this.isAddMode ? this.criar() : this.atualizar();
	}

	atualizar(): void {
		const form: any = this.form.getRawValue();

		let obj = {
			...this.localEnvioNFFaturamento,
			...form,
			...{
				codUsuarioManut: this.userSession.usuario.codUsuario
			}
		};

		this._localEnvioNFFaturamentoService.atualizar(obj).subscribe(() => {
			this._snack.exibirToast(`Local Envio NF Faturamento atualizado com sucesso!`, "success");
			this._location.back();
		});
	}

	criar(): void {
		const form = this.form.getRawValue();

		let obj = {
			...this.localEnvioNFFaturamento,
			...form,
			...{
				codUsuarioCad: this.userSession.usuario.codUsuario
			}
		};

		this._localEnvioNFFaturamentoService.criar(obj).subscribe(() => {
			this._snack.exibirToast(`Local Envio NF Faturamento adicionado com sucesso!`, "success");
			this._location.back();
		});
	}

	private async obterClientes(filtro: string = '') {
		const params: ClienteParameters = {
			sortActive: 'nomeFantasia',
			sortDirection: 'asc',
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			pageSize: 50
		}

		const data = await this._clienteService.obterPorParametros(params).toPromise();
		this.clientes = data.items;
	}

	private async obterContratos(filtro: string = '') {
		const codCliente = this.form.controls['codCliente'].value;

		const params: ContratoParameters = {
			sortActive: 'nomeContrato',
			sortDirection: 'asc',
			codCliente: codCliente,
			filter: filtro,
			pageSize: 1000
		}

		const data = await this._contratoService.obterPorParametros(params).toPromise();

		console.log(data);

		this.contratos = data.items;
	}

	registrarEmitters() {
		this.clienteFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(500),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterClientes(this.clienteFilterCtrl.value);
			});

		this.form.controls['codCliente'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(500),
				distinctUntilChanged()
			)
			.subscribe(() => {

				if (!this.isAddMode) {
					this.obterContratos(this.localEnvioNFFaturamento.contrato.nomeContrato);
				}
				else{
					this.obterContratos();
				}
			});

		this.contratoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(500),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterContratos(this.contratoFilterCtrl.value);
			});

		this.form.controls['cepFaturamento'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(500),
				distinctUntilChanged()
			)
			.subscribe((cep: string) => {
				if (cep.length === 8)
					this._geolocalizacaoService.obterPorParametros({
						enderecoCep: cep,
						geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.VIACEP
					}).subscribe((geo) => {
						this.form.controls['enderecoFaturamento'].setValue(geo.endereco);
						this.form.controls['numeroFaturamento'].setValue(this.localEnvioNFFaturamento.numeroFaturamento);
						this.form.controls['bairroFaturamento'].setValue(geo.bairro);
						this.form.controls['nomeCidadeFaturamento'].setValue(geo.cidade);
						this.form.controls['siglaUFFaturamento'].setValue(geo.estado);
					}, e => {
						this._snack.exibirToast(e.error.errorMessage || 'Erro ao consultar as coordenadas', 'error');
					});
			});

		this.form.controls['cepEnvioNF'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(500),
				distinctUntilChanged()
			)
			.subscribe((cep: string) => {
				if (cep.length === 8)
					this._geolocalizacaoService.obterPorParametros({
						enderecoCep: cep,
						geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.VIACEP
					}).subscribe((geo) => {
						this.form.controls['enderecoEnvioNF'].setValue(geo.endereco);
						this.form.controls['numeroEnvioNF'].setValue(this.localEnvioNFFaturamento.numeroEnvioNF);
						this.form.controls['bairroEnvioNF'].setValue(geo.bairro);
						this.form.controls['nomeCidadeEnvioNF'].setValue(geo.cidade);
						this.form.controls['siglaUFEnvioNF'].setValue(geo.estado);
					}, e => {
						this._snack.exibirToast(e.error.errorMessage || 'Erro ao consultar as coordenadas', 'error');
					});
			});
	}

	copiarDadosLocal() {
		this.form.controls['cepEnvioNF'].setValue(this.form.controls['cepFaturamento'].value);
		this.form.controls['enderecoEnvioNF'].setValue(this.form.controls['enderecoFaturamento'].value);
		this.form.controls['complementoEnvioNF'].setValue(this.form.controls['complementoFaturamento'].value);
		this.form.controls['numeroEnvioNF'].setValue(this.form.controls['numeroFaturamento'].value);
		this.form.controls['bairroEnvioNF'].setValue(this.form.controls['bairroFaturamento'].value);
		this.form.controls['nomeCidadeEnvioNF'].setValue(this.form.controls['nomeCidadeFaturamento'].value);
		this.form.controls['siglaUFEnvioNF'].setValue(this.form.controls['siglaUFFaturamento'].value);
		this.form.controls['cnpjEnvioNF'].setValue(this.form.controls['cnpjFaturamento'].value);
		this.form.controls['inscricaoEstadualEnvioNF'].setValue(this.form.controls['inscricaoEstadualFaturamento'].value);
		this.form.controls['responsavelEnvioNF'].setValue(this.form.controls['responsavelFaturamento'].value);
		this.form.controls['emailEnvioNF'].setValue(this.form.controls['emailFaturamento'].value);
		this.form.controls['foneEnvioNF'].setValue(this.form.controls['foneFaturamento'].value);
		this.form.controls['faxEnvioNF'].setValue(this.form.controls['faxFaturamento'].value);
		this.form.controls['razaoSocialEnvioNF'].setValue(this.form.controls['razaoSocialFaturamento'].value);

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
