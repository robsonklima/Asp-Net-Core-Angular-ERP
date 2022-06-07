import { CidadeService } from 'app/core/services/cidade.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { UnidadeFederativa, UnidadeFederativaParameters } from 'app/core/types/unidade-federativa.types';
import { Cidade, CidadeParameters } from 'app/core/types/cidade.types';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { LocalAtendimento, LocalAtendimentoParameters } from './../../../../../core/types/local-atendimento.types';
import { Regiao } from 'app/core/types/regiao.types';
import { FilialService } from 'app/core/services/filial.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { TipoEquipamento, TipoEquipamentoParameters } from 'app/core/types/tipo-equipamento.types';
import { statusConst } from '../../../../../core/types/status-types';
import { UserService } from '../../../../../core/user/user.service';
import { ClienteService } from '../../../../../core/services/cliente.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { Cliente, ClienteParameters } from '../../../../../core/types/cliente.types';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { ContratoService } from 'app/core/services/contrato.service';
import { TipoContrato, TipoContratoParameters } from 'app/core/types/tipo-contrato.types';
import { TipoContratoService } from 'app/core/services/tipo-contrato.service';
import { GrupoEquipamento, GrupoEquipamentoParameters } from 'app/core/types/grupo-equipamento.types';
import { TipoEquipamentoService } from 'app/core/services/tipo-equipamento.service';
import { GrupoEquipamentoService } from 'app/core/services/grupo-equipamento.service';
import { Equipamento, EquipamentoParameters } from 'app/core/types/equipamento.types';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { RegiaoAutorizadaParameters } from 'app/core/types/regiao-autorizada.types';
import Enumerable from 'linq';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { AutorizadaService } from 'app/core/services/autorizada.service';


@Component({
	selector: 'app-equipamento-contrato-filtro',
	templateUrl: './equipamento-contrato-filtro.component.html'
})
export class EquipamentoContratoFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	clientes: Cliente[] = [];
	clienteFilterCtrl: FormControl = new FormControl();
	contratos: Contrato[] = [];
	contratoFilterCtrl: FormControl = new FormControl();
	equipamentos: Equipamento[] = [];
	locaisAtendimento: LocalAtendimento[] = [];
	ufs: UnidadeFederativa[] = [];
	cidades: Cidade[] = [];
	filiais: Filial[] = [];
	regioes: Regiao[] = [];
	autorizadas: Autorizada[] = [];
	tipoEquipamentos: TipoEquipamento[] = [];
	grupoEquipamentos: GrupoEquipamento[] = [];
	tipoContratos: TipoContrato[] = [];
	equipamentoFilterCtrl: FormControl = new FormControl();
	tipoContratoFilterCtrl: FormControl = new FormControl();
	tipoEquipamentoFilterCtrl: FormControl = new FormControl();
	grupoEquipamentoFilterCtrl: FormControl = new FormControl();
	codClientesSelected: string;
	protected _onDestroy = new Subject<void>();

	constructor(

		private _clienteService: ClienteService,
		private _contratoService: ContratoService,
		private _filialService: FilialService,
		private _equipamentoService: EquipamentoService,
		private _tipoEquipamentoService: TipoEquipamentoService,
		private _grupoEquipamentoService: GrupoEquipamentoService,
		private _tipoContratoService: TipoContratoService,
		private _regiaoAutorizadaService: RegiaoAutorizadaService,
		private _localAtendimentoSvc: LocalAtendimentoService,
		private _unidadeFederativaService: UnidadeFederativaService,
		private _cidadeService: CidadeService,
		private _autorizadaService: AutorizadaService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'equipamento-contrato');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterClientes();
		this.obterTipoContratos();
		this.obterEquipamentos();
		this.obterLocaisAtendimentos();
		this.obterCidades(); 
		this.obterUfs(); 
		this.obterFiliais();
		this.obterRegioesAutorizadas(this.form.controls['codFiliais'].value);
		this.obterAutorizadas(this.form.controls['codFiliais'].value);
		this.obterTipoEquipamentos();
		this.obterGrupoEquipamentos();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codClientes: [undefined],
			indAtivo: [undefined],
			numSerie: [undefined],
			codTipoContratos: [undefined],
			codEquips: [undefined],
			codPostos: [undefined],
			codFiliais: [undefined],
			codRegioes: [undefined],
			codAutorizadas: [undefined],
			codTipoEquips: [undefined],
			codGrupoEquips: [undefined],
			codContratos: [undefined],
			codCidades: [undefined],
			codUfs: [undefined]
		});
		this.form.patchValue(this.filter?.parametros);
	}

	async obterClientes(filtro: string = '') {
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

	async obterTipoContratos(filtro: string = '') {
		let params: TipoContratoParameters = {
			filter: filtro,
			sortActive: 'nomeTipoContrato',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._tipoContratoService
			.obterPorParametros(params)
			.toPromise();
		this.tipoContratos = data.items;
	}

	async obterEquipamentos(filtro: string = '') {
		let params: EquipamentoParameters = {
			filter: filtro,
			sortActive: 'nomeEquip',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._equipamentoService
			.obterPorParametros(params)
			.toPromise();
		this.equipamentos = data.items;
	}

	async obterTipoEquipamentos(filtro: string = '') {
		let params: TipoEquipamentoParameters = {
			filter: filtro,
			sortActive: 'nomeTipoEquip',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._tipoEquipamentoService
			.obterPorParametros(params)
			.toPromise();
		this.tipoEquipamentos = data.items;
	}

	async obterGrupoEquipamentos(filtro: string = '') {
		let params: GrupoEquipamentoParameters = {
			filter: filtro,
			sortActive: 'nomeGrupoEquip',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._grupoEquipamentoService
			.obterPorParametros(params)
			.toPromise();
		this.grupoEquipamentos = data.items;
	}

	async obterContratos(filtro: string = '') {
		let params: ContratoParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			codClientes: this.form.controls['codClientes'].value.join(','),
			sortActive: 'nomeContrato',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._contratoService
			.obterPorParametros(params)
			.toPromise();
		this.contratos = data.items;
	}

	async obterFiliais() {
		let params: FilialParameters = {
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeFilial',
			sortDirection: 'asc'
		};

		const data = await this._filialService
			.obterPorParametros(params)
			.toPromise();

		this.filiais = data.items;
	}

	async obterRegioesAutorizadas(filialFilter: any = '') {
		let params: RegiaoAutorizadaParameters = {
			indAtivo: statusConst.ATIVO,
			codFiliais: filialFilter,
			pageSize: 1000
		};

		const data = await this._regiaoAutorizadaService
			.obterPorParametros(params)
			.toPromise();

		this.regioes = Enumerable.from(data.items).where(ra => ra.regiao?.indAtivo == 1).select(ra => ra.regiao).distinct(r => r.codRegiao).orderBy(i => i.nomeRegiao).toArray();
	}

	async obterAutorizadas(filialFilter: any = '') {
		let params: AutorizadaParameters = {
			indAtivo: statusConst.ATIVO,
			codFiliais: filialFilter,
			pageSize: 1000
		};

		const data = await this._autorizadaService
			.obterPorParametros(params)
			.toPromise();

		this.autorizadas = Enumerable.from(data.items).orderBy(i => i.nomeFantasia).toArray();
	}

	async obterCidades(ufFilter: any = '') {
		let params: CidadeParameters = {
			indAtivo: statusConst.ATIVO,
			codUF: ufFilter,
			pageSize: 500
		};

		const data = await this._cidadeService
			.obterPorParametros(params)
			.toPromise();

		this.cidades = Enumerable.from(data.items).orderBy(i => i.nomeCidade).toArray();
	}

	async obterUfs() {
		let params: UnidadeFederativaParameters = {
			pageSize: 1000
		};

		const data = await this._unidadeFederativaService
			.obterPorParametros(params)
			.toPromise();

		this.ufs = Enumerable.from(data.items).orderBy(i => i.nomeUF).toArray();
	}

	async obterLocaisAtendimentos() {
		let params: LocalAtendimentoParameters = {
			indAtivo: statusConst.ATIVO,
			codFiliais: this.form.controls['codFiliais'].value,
			codClientes: this.form.controls['codClientes'].value,
			codRegioes: this.form.controls['codRegioes'].value,
			codAutorizada: this.form.controls['codAutorizadas'].value,
			sortActive: 'nomeLocal',
			sortDirection: 'asc',
			pageSize: 1000
		};

		const data = await this._localAtendimentoSvc
			.obterPorParametros(params)
			.toPromise();

		this.locaisAtendimento = Enumerable.from(data.items).orderBy(i => i.nomeLocal.trim()).toArray();
	}

	private registrarEmitters() {
		this.clienteFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterClientes(this.clienteFilterCtrl.value);
			});

		this.form.controls['codClientes'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterLocaisAtendimentos();
				this.obterContratos(this.clienteFilterCtrl.value);
			});

		this.form.controls['codUfs'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterCidades(this.form.controls['codUfs'].value);
			});

		this.form.controls['codFiliais'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterLocaisAtendimentos();
				this.obterAutorizadas(this.form.controls['codFiliais'].value);
				this.obterRegioesAutorizadas(this.form.controls['codFiliais'].value);
			});

		this.contratoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterContratos(this.contratoFilterCtrl.value);
			});

		this.tipoContratoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterTipoContratos(this.tipoContratoFilterCtrl.value);
			});

		this.equipamentoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterEquipamentos(this.equipamentoFilterCtrl.value);
			});
	}

	limpar() {
		super.limpar();

		if (this.userSession?.usuario?.codFilial) {
			this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
			this.form.controls['codFiliais'].disable();
		}
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}