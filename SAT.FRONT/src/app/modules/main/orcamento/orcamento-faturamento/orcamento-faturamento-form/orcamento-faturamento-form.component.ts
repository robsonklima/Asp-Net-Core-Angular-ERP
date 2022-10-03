import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
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
import { LocalAtendimento, LocalAtendimentoParameters } from 'app/core/types/local-atendimento.types';
import Enumerable from 'linq';
import { LocalEnvioNFFaturamentoVinculadoService } from 'app/core/services/local-envio-nf-faturamento-vinculado.service';
import { LocalEnvioNFFaturamentoVinculado } from 'app/core/types/local-envio-nf-faturamento-vinculado.types';
import moment from 'moment';

@Component({
	selector: 'app-orcamento-faturamento-form',
	templateUrl: './orcamento-faturamento-form.component.html',
	styles: [`
		.list-grid-locais-faturamento {
            grid-template-columns: 80px auto 100px 50px;
            
            @screen sm {
                grid-template-columns: 80px auto 100px 50px;
            }
        }
	`],
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class OrcamentoFaturamentoFormComponent implements OnInit {
	codLocalEnvioNFFaturamento: number;
	localEnvioNFFaturamento: LocalEnvioNFFaturamento;
	localEnvioNFFaturamentoVinculado: LocalEnvioNFFaturamentoVinculado[];
	clientes: Cliente[] = [];
	contratos: Contrato[] = [];
	isAddMode: boolean;
	formLocalEnvioNF: FormGroup;
	formLocalEnvioNFVinculado: FormGroup;
	isLoading: boolean;
	locaisAtendimento: LocalAtendimento[];
	userSession: UsuarioSessao;
	clienteFilterCtrl: FormControl = new FormControl();
	contratoFilterCtrl: FormControl = new FormControl();
	localAtendimentoFilterCtrl: FormControl = new FormControl();

	protected _onDestroy = new Subject<void>();

	constructor(
		private _formBuilder: FormBuilder,
		private _snack: CustomSnackbarService,
		private _route: ActivatedRoute,
		private _userService: UserService,
		private _localEnvioNFFaturamentoService: LocalEnvioNFFaturamentoService,
		private _localEnvioNFFaturamentoVinculadoService: LocalEnvioNFFaturamentoVinculadoService,
		private _clienteService: ClienteService,
		private _contratoService: ContratoService,
		private _location: Location,
		private _geolocalizacaoService: GeolocalizacaoService,
		private _localAtendimentoSvc: LocalAtendimentoService
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngOnInit(): void {		
		this.isLoading = true;
		this.codLocalEnvioNFFaturamento = +this._route.snapshot.paramMap.get('codLocalEnvioNFFaturamento');
		this.isAddMode = !this.codLocalEnvioNFFaturamento;

		this.formLocalEnvioNF = this._formBuilder.group({
			codLocalEnvioNFFaturamento: [{
				value: undefined,
				disabled: true
			}, Validators.required],
			codCliente: [undefined, Validators.required],
			codContrato: [undefined, Validators.required],
			cepFaturamento: ['', Validators.required],
			enderecoFaturamento: [undefined, Validators.required],
			complementoFaturamento: [undefined],
			numeroFaturamento: [this.localEnvioNFFaturamento?.numeroFaturamento],
			bairroFaturamento: [undefined, Validators.required],
			nomeCidadeFaturamento: [undefined, Validators.required],
			siglaUFFaturamento: [undefined, Validators.required],
			cnpjFaturamento: [undefined, Validators.required],
			inscricaoEstadualFaturamento: [undefined],
			responsavelFaturamento: [undefined, Validators.required],
			emailFaturamento: [undefined, Validators.required],
			foneFaturamento: [undefined],
			faxFaturamento: [undefined],
			razaoSocialFaturamento: [undefined, Validators.required],
			cepEnvioNF: ['', Validators.required],
			enderecoEnvioNF: [undefined, Validators.required],
			complementoEnvioNF: [undefined],
			numeroEnvioNF: [this.localEnvioNFFaturamento?.numeroEnvioNF],
			bairroEnvioNF: [undefined, Validators.required],
			nomeCidadeEnvioNF: [undefined, Validators.required],
			siglaUFEnvioNF: [undefined, Validators.required],
			cnpjEnvioNF: [undefined, Validators.required],
			inscricaoEstadualEnvioNF: [undefined],
			responsavelEnvioNF: [undefined, Validators.required],
			emailEnvioNF: [undefined, Validators.required],
			foneEnvioNF: [undefined],
			faxEnvioNF: [undefined],
			razaoSocialEnvioNF: [undefined, Validators.required]
		});

		this.formLocalEnvioNFVinculado = this._formBuilder.group({
			codPosto: [undefined]
		});

		if (!this.isAddMode)
			this.obterLocalEnvioNFFaturamento();

		this.obterClientes();
		this.obterContratos();
		this.registrarEmitters();
		this.isLoading = false;
	}

	salvar(): void {
		this.isAddMode ? this.criar() : this.atualizar();
	}

	atualizar(): void {
		const form: any = this.formLocalEnvioNF.getRawValue();

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
		const form = this.formLocalEnvioNF.getRawValue();

		let obj = {
			...this.localEnvioNFFaturamento,
			...form,
			...{
				codUsuarioCad: this.userSession.usuario.codUsuario,
				dataHoraCad: moment()
			}
		};

		this._localEnvioNFFaturamentoService.criar(obj).subscribe(() => {
			this._snack.exibirToast(`Local Envio NF Faturamento adicionado com sucesso!`, "success");
			this._location.back();
		});
	}

	async excluirLocal(local: any = null) {

		this._localEnvioNFFaturamentoVinculadoService
			.deletar(local.codLocalEnvioNFFaturamento, local.codPosto, local.codContrato)
			.subscribe(() => {
				this._snack.exibirToast('Local desvinculado', 'success');
				this.obterLocalEnvioNFFaturamento();

			});
	}

	salvarLocalEnvioNf(localEnvioNf: any) {

		let novoLocal: LocalEnvioNFFaturamentoVinculado = {
			codLocalEnvioNFFaturamento: this.codLocalEnvioNFFaturamento,
			codPosto: localEnvioNf.codPosto,
			codContrato: this.formLocalEnvioNF.controls['codContrato'].value,
			codUsuarioCad: this.userSession?.usuario?.codUsuario,
			dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
		}

		this._localEnvioNFFaturamentoVinculadoService
			.criar(novoLocal)
			.subscribe(() => {
				this._snack.exibirToast('Local vinculado', 'success');
				this.obterLocalEnvioNFFaturamento();

			});
	}

	private async obterLocalEnvioNFFaturamento() {
		this._localEnvioNFFaturamentoService.obterPorCodigo(this.codLocalEnvioNFFaturamento)
			.pipe(first())
			.subscribe(data => {

				this.registrarEmitters();
				this.obterClientes(data.cliente.nomeFantasia);
				this.obterContratos(data.contrato.nomeContrato);
				this.localEnvioNFFaturamento = data;
				this.formLocalEnvioNF.patchValue(data);
				this.formLocalEnvioNF.controls['codContrato'].setValue(data.contrato.codContrato)
				this.obterLocaisAtendimentos();
				this.obterLocaisEnvioNFFaturamentoVinculado(data.contrato.codContrato);
			});
	}

	async obterLocaisEnvioNFFaturamentoVinculado(codContrato: number) {

		this._localEnvioNFFaturamentoVinculadoService
			.obterPorParametros({ codLocalEnvioNFFaturamento: this.codLocalEnvioNFFaturamento, codContrato: codContrato })
			.subscribe(data => {
				this.localEnvioNFFaturamentoVinculado = data.items;
			});
	}

	private async obterClientes(filtro: string = '') {
		let params: ClienteParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeFantasia',
			sortDirection: 'asc',
			pageSize: 1000
		};

		const data = await this._clienteService
			.obterPorParametros(params)
			.toPromise();

		this.clientes = data.items;
	}

	private async obterContratos(filtro: string = '') {
		const codCliente = this.formLocalEnvioNF.controls['codCliente'].value;
		const params: ContratoParameters = {
			sortActive: 'nomeContrato',
			sortDirection: 'asc',
			codClientes: codCliente,
			filter: filtro,
			pageSize: 1000
		}
		const data = await this._contratoService.obterPorParametros(params).toPromise();
		this.contratos = data.items;
	}

	async obterLocaisAtendimentos(localFiltro: string = '') {

		let params: LocalAtendimentoParameters = {
			indAtivo: statusConst.ATIVO,
			codClientes: this.formLocalEnvioNF.controls['codCliente'].value,
			sortActive: 'nomeLocal',
			sortDirection: 'asc',
			pageSize: 1000,
			filter: localFiltro
		};

		const data = await this._localAtendimentoSvc
			.obterPorParametros(params)
			.toPromise();
		this.locaisAtendimento = Enumerable.from(data.items).orderBy(i => i.nomeLocal.trim()).toArray();
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

		this.formLocalEnvioNF.controls['codCliente'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(500),
				distinctUntilChanged()
			)
			.subscribe(() => {

				if (!this.isAddMode) {
					this.obterContratos(this.localEnvioNFFaturamento.contrato.nomeContrato);
					this.formLocalEnvioNF.controls['codCliente'].disable();
				}
				else {
					this.obterContratos();
				}
			});

		this.contratoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(500),
				distinctUntilChanged()
			)
			.subscribe((query) => {
				this.obterContratos(query);
			});

		this.localAtendimentoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterLocaisAtendimentos(this.localAtendimentoFilterCtrl.value);
			});

		this.formLocalEnvioNF.controls['cepFaturamento'].valueChanges
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
						this.formLocalEnvioNF.controls['enderecoFaturamento'].setValue(geo.endereco);
						this.formLocalEnvioNF.controls['bairroFaturamento'].setValue(geo.bairro);
						this.formLocalEnvioNF.controls['nomeCidadeFaturamento'].setValue(geo.cidade);
						this.formLocalEnvioNF.controls['siglaUFFaturamento'].setValue(geo.estado);
					}, e => {
						this._snack.exibirToast(e.error.errorMessage || 'Erro ao consultar as coordenadas', 'error');
					});
			});

		this.formLocalEnvioNF.controls['cepEnvioNF'].valueChanges
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
						this.formLocalEnvioNF.controls['enderecoEnvioNF'].setValue(geo.endereco);
						this.formLocalEnvioNF.controls['bairroEnvioNF'].setValue(geo.bairro);
						this.formLocalEnvioNF.controls['nomeCidadeEnvioNF'].setValue(geo.cidade);
						this.formLocalEnvioNF.controls['siglaUFEnvioNF'].setValue(geo.estado);
					}, e => {
						this._snack.exibirToast(e.error.errorMessage || 'Erro ao consultar as coordenadas', 'error');
					});
			});
	}

	copiarDadosLocal() {
		this.formLocalEnvioNF.controls['cepEnvioNF'].setValue(this.formLocalEnvioNF.controls['cepFaturamento'].value);
		this.formLocalEnvioNF.controls['enderecoEnvioNF'].setValue(this.formLocalEnvioNF.controls['enderecoFaturamento'].value);
		this.formLocalEnvioNF.controls['complementoEnvioNF'].setValue(this.formLocalEnvioNF.controls['complementoFaturamento'].value);
		this.formLocalEnvioNF.controls['numeroEnvioNF'].setValue(this.formLocalEnvioNF.controls['numeroFaturamento'].value);
		this.formLocalEnvioNF.controls['bairroEnvioNF'].setValue(this.formLocalEnvioNF.controls['bairroFaturamento'].value);
		this.formLocalEnvioNF.controls['nomeCidadeEnvioNF'].setValue(this.formLocalEnvioNF.controls['nomeCidadeFaturamento'].value);
		this.formLocalEnvioNF.controls['siglaUFEnvioNF'].setValue(this.formLocalEnvioNF.controls['siglaUFFaturamento'].value);
		this.formLocalEnvioNF.controls['cnpjEnvioNF'].setValue(this.formLocalEnvioNF.controls['cnpjFaturamento'].value);
		this.formLocalEnvioNF.controls['inscricaoEstadualEnvioNF'].setValue(this.formLocalEnvioNF.controls['inscricaoEstadualFaturamento'].value);
		this.formLocalEnvioNF.controls['responsavelEnvioNF'].setValue(this.formLocalEnvioNF.controls['responsavelFaturamento'].value);
		this.formLocalEnvioNF.controls['emailEnvioNF'].setValue(this.formLocalEnvioNF.controls['emailFaturamento'].value);
		this.formLocalEnvioNF.controls['foneEnvioNF'].setValue(this.formLocalEnvioNF.controls['foneFaturamento'].value);
		this.formLocalEnvioNF.controls['faxEnvioNF'].setValue(this.formLocalEnvioNF.controls['faxFaturamento'].value);
		this.formLocalEnvioNF.controls['razaoSocialEnvioNF'].setValue(this.formLocalEnvioNF.controls['razaoSocialFaturamento'].value);

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
