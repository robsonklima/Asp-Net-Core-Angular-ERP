import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { ClienteService } from 'app/core/services/cliente.service';
import { ContratoEquipamentoService } from 'app/core/services/contrato-equipamento.service';
import { ContratoSLAService } from 'app/core/services/contrato-sla.service';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { FilialService } from 'app/core/services/filial.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { AcordoNivelServico } from 'app/core/types/acordo-nivel-servico.types';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { ContratoEquipamentoParameters } from 'app/core/types/contrato-equipamento.types';
import { ContratoSLAParameters } from 'app/core/types/contrato-sla.types';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { EquipamentoContrato, PontoEstrategicoEnum } from 'app/core/types/equipamento-contrato.types';
import { Equipamento } from 'app/core/types/equipamento.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { LocalAtendimento, LocalAtendimentoParameters } from 'app/core/types/local-atendimento.types';
import { RegiaoAutorizadaParameters } from 'app/core/types/regiao-autorizada.types';
import { Regiao } from 'app/core/types/regiao.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, first, map, takeUntil, tap } from 'rxjs/operators';

@Component({
	selector: 'app-equipamento-contrato-form',
	templateUrl: './equipamento-contrato-form.component.html'
})
export class EquipamentoContratoFormComponent implements OnInit, OnDestroy {
	codEquipContrato: number;
	codContrato: number;
	equipamento: EquipamentoContrato;
	isAddMode: boolean;
	form: FormGroup;
	equipamentoContrato: EquipamentoContrato;
	clientes: Cliente[] = [];
	contratos: Contrato[] = [];
	modelos: Equipamento[] = [];
	slas: AcordoNivelServico[] = [];
	autorizadas: Autorizada[] = [];
	regioes: Regiao[] = [];
	filiais: Filial[] = [];
	locais: LocalAtendimento[] = [];
	locaisFiltro: FormControl = new FormControl();
	pontosEstrategicos: any[] = []
	userSession: UsuarioSessao;
	protected _onDestroy = new Subject<void>();

	constructor(
		private _formBuilder: FormBuilder,
		private _equipamentoContratoService: EquipamentoContratoService,
		private _contratoService: ContratoService,
		private _route: ActivatedRoute,
		private _snack: CustomSnackbarService,
		public _location: Location,
		private _clienteService: ClienteService,
		private _contratoEquipamentoService: ContratoEquipamentoService,
		private _contratoSLAService: ContratoSLAService,
		private _autorizadaService: AutorizadaService,
		private _regiaoAutorizadaService: RegiaoAutorizadaService,
		private _localAtendimentoService: LocalAtendimentoService,
		private _filialService: FilialService,
		private _userService: UserService
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.codEquipContrato = +this._route.snapshot.paramMap.get('codEquipContrato');
		this.codContrato = +this._route.snapshot.paramMap.get('codContrato');

		this.isAddMode = !this.codEquipContrato;
		this.inicializarForm();
		this.obterClientes();
		this.obterFiliais();
		this.obterPontosEstrategicos();

		this.form.controls['codCliente'].valueChanges.subscribe(async () => {
			this.obterContratos();
		});

		this.form.controls['codContrato'].valueChanges.subscribe(async () => {
			this.obterModelos();
		});

		this.form.controls['codEquip'].valueChanges.subscribe(async () => {
			this.obterSLAs();
		});

		this.form.controls['codFilial'].valueChanges.subscribe(async () => {
			this.obterAutorizadas();
		});

		this.form.controls['codAutorizada'].valueChanges.subscribe(async () => {
			this.obterRegioes();
		});

		this.form.controls['codRegiao'].valueChanges.subscribe(async () => {
			this.obterLocais();
		});

		this.locaisFiltro.valueChanges.pipe(
			filter(query => !!query),
			debounceTime(700),
			delay(500),
			takeUntil(this._onDestroy),
			map(async query => { this.obterLocais(query) })
		).subscribe(() => { });

		if (!this.isAddMode) {
			this._equipamentoContratoService.obterPorCodigo(this.codEquipContrato)
				.pipe(first())
				.subscribe(data => {
					this.form.patchValue(data);
					this.equipamentoContrato = data;
				});
		}
	}

	private inicializarForm() {
		this.form = this._formBuilder.group({
			codEquipContrato: [
				{
					value: undefined,
					disabled: true
				}, Validators.required
			],
			numSerie: [undefined, Validators.required],
			codEquip: [undefined, [Validators.required]],
			codCliente: [undefined, Validators.required],
			codSLA: [undefined, Validators.required],
			codContrato: [undefined, Validators.required],
			codPosto: [undefined, Validators.required],
			codFilial: [undefined, Validators.required],
			codRegiao: [undefined, Validators.required],
			codAutorizada: [undefined, Validators.required],
			distanciaPatRes: [undefined],
			dataInicGarantia: [undefined],
			dataFimGarantia: [undefined],
			dataAtivacao: [undefined],
			dataDesativacao: [undefined],
			indReceita: [undefined],
			indRepasse: [undefined],
			indRepasseIndividual: [undefined],
			indInstalacao: [undefined],
			indAtivo: [undefined],
			indRAcesso: [undefined],
			indRHorario: [undefined],
			indSemat: [undefined],
			indVerao: [undefined],
			indPAE: [undefined],
			pontoEstrategico: [undefined],
			numSerieCliente: [undefined],
		});
	}

	private async obterClientes() {
		const params: ClienteParameters = {
			sortActive: 'nomeFantasia',
			sortDirection: 'asc',
			indAtivo: statusConst.ATIVO,
			pageSize: 300
		}

		const data = await this._clienteService.obterPorParametros(params).toPromise();
		this.clientes = data.items;
	}

	private async obterContratos() {
		const codCliente = this.form.controls['codCliente'].value;

		const params: ContratoParameters = {
			sortActive: 'nomeContrato',
			sortDirection: 'asc',
			indAtivo: statusConst.ATIVO,
			codClientes: codCliente,
			pageSize: 1000
		}

		const data = await this._contratoService.obterPorParametros(params).toPromise();
		this.contratos = data.items;
	}

	private async obterModelos() {
		const codContrato = this.form.controls['codContrato'].value;

		const params: ContratoEquipamentoParameters = {
			pageSize: 100,
			codContrato: codContrato
		}

		const data = await this._contratoEquipamentoService.obterPorParametros(params).toPromise();
		this.modelos = data.items.map(ce => ce.equipamento);		
	}

	private async obterSLAs() {

		const params: ContratoSLAParameters = {
			pageSize: 1100,
			codContrato: this.form.controls['codContrato'].value
		}

		const data = await this._contratoSLAService.obterPorParametros(params).toPromise();
		this.slas = data.items.map(ce => ce.sla);
	}

	private async obterFiliais() {
		const params: FilialParameters = {
			sortActive: 'nomeFilial',
			sortDirection: 'asc',
			indAtivo: statusConst.ATIVO,
			pageSize: 100
		}

		const data = await this._filialService.obterPorParametros(params).toPromise();
		this.filiais = data.items;
	}

	private async obterRegioes() {
		const codAutorizada = this.form.controls['codAutorizada'].value;

		const params: RegiaoAutorizadaParameters = {
			codAutorizada: codAutorizada,
			indAtivo: statusConst.ATIVO,
			pageSize: 100
		}

		const data = await this._regiaoAutorizadaService.obterPorParametros(params).toPromise();
		this.regioes = data.items.map(ra => ra.regiao);
	}

	private async obterAutorizadas() {
		const codFilial = this.form.controls['codFilial'].value;

		const params: AutorizadaParameters = {
			sortActive: 'nomeFantasia',
			sortDirection: 'asc',
			indAtivo: statusConst.ATIVO,
			codFilial: codFilial,
			pageSize: 100
		}

		const data = await this._autorizadaService.obterPorParametros(params).toPromise();
		this.autorizadas = data.items;
	}

	private async obterLocais(filtro: string = '') {

		const params: LocalAtendimentoParameters = {
			sortActive: 'nomeLocal',
			sortDirection: 'asc',
			indAtivo: statusConst.ATIVO,
			filter: filtro,
			codCliente: this.form.controls['codCliente'].value,
			codFilial: this.form.controls['codFilial'].value,
			pageSize: 10000
		}

		const data = await this._localAtendimentoService.obterPorParametros(params).toPromise();
		
		this.locais = data.items.slice();
	}

	private obterPontosEstrategicos(): void {
		const data = Object.keys(PontoEstrategicoEnum).filter((element) => {
			return isNaN(Number(element));
		});

		data.forEach((tr, i) => {
			this.pontosEstrategicos.push({
				codPontoEstrategico: String(i),
				nomePontoEstrategico: tr
			})
		});
	}

	salvar(): void {
		this.isAddMode ? this.criar() : this.atualizar();
	}

	atualizar(): void {
		const form: any = this.form.getRawValue();


		let obj = {
			...this.equipamento,
			...form,
			...{
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				dataManutencao: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario.codUsuario,
				codUsuarioManutencao: this.userSession.usuario.codUsuario,
				indReceita: +form.indReceita,
				indRepasse: +form.indRepasse,
				indRepasseIndividual: +form.indRepasseIndividual,
				indInstalacao: +form.indInstalacao,
				indAtivo: +form.indAtivo,
				indRAcesso: +form.indRAcesso,
				indRHorario: +form.indRHorario,
				indSemat: +form.indSemat,
				indVerao: +form.indVerao,
				indPAE: +form.indPAE
			}
		};

		this._equipamentoContratoService.atualizar(obj).subscribe(() => {
			this._snack.exibirToast(`Equipamento ${obj.numSerie} atualizado com sucesso!`, "success");
			this._location.back();
		});
	}

	criar(): void {
		const form = this.form.getRawValue();

		let obj = {
			...this.equipamento,
			...form,
			...{
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario.codUsuario,
				indReceita: +form.indReceita,
				indRepasse: +form.indRepasse,
				indRepasseIndividual: +form.indRepasseIndividual,
				indInstalacao: +form.indInstalacao,
				indAtivo: +form.indAtivo,
				indRAcesso: +form.indRAcesso,
				indRHorario: +form.indRHorario,
				indSemat: +form.indSemat,
				indVerao: +form.indVerao,
				indPAE: +form.indPAE
			}
		};

		this._equipamentoContratoService.criar(obj).subscribe(() => {
			this._snack.exibirToast(`Equipamento ${obj.numSerie} inserido com sucesso!`, "success");
			this._location.back();
		});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
