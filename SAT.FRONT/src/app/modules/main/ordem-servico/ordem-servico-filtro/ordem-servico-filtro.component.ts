import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { ClienteService } from 'app/core/services/cliente.service';
import { FilialService } from 'app/core/services/filial.service';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { TipoIntervencaoService } from 'app/core/services/tipo-intervencao.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { PontoEstrategicoEnum } from 'app/core/types/equipamento-contrato.types';
import { Filial } from 'app/core/types/filial.types';
import { RegiaoAutorizadaParameters } from 'app/core/types/regiao-autorizada.types';
import { Regiao } from 'app/core/types/regiao.types';
import { StatusServico, StatusServicoParameters } from 'app/core/types/status-servico.types';
import { TipoIntervencao } from 'app/core/types/tipo-intervencao.types';
import { UserService } from 'app/core/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { Tecnico, TecnicoParameters } from 'app/core/types/tecnico.types';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { GrupoEquipamento } from 'app/core/types/grupo-equipamento.types';
import { TipoEquipamento } from 'app/core/types/tipo-equipamento.types';
import { Equipamento, EquipamentoFilterEnum } from 'app/core/types/equipamento.types';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { FilterBase } from 'app/core/filters/filter-base';
import { IFilterBase } from 'app/core/types/filtro.types';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { LocalAtendimento, LocalAtendimentoParameters } from 'app/core/types/local-atendimento.types';
import { statusConst } from 'app/core/types/status-types';
import { MatChipInputEvent } from '@angular/material/chips';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Usuario, UsuarioParameters } from 'app/core/types/usuario.types';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { UsuarioService } from 'app/core/services/usuario.service';
import Enumerable from 'linq';

@Component({
	selector: 'app-ordem-servico-filtro',
	templateUrl: './ordem-servico-filtro.component.html'
})
export class OrdemServicoFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	@Input() sidenav: MatSidenav;
	filiais: Filial[] = [];
	clientes: Cliente[] = [];
	regioes: Regiao[] = [];
	autorizadas: Autorizada[] = [];
	statusServicos: StatusServico[] = [];
	tiposIntervencao: TipoIntervencao[] = [];
	gruposEquip: GrupoEquipamento[] = [];
	tipoEquip: TipoEquipamento[] = [];
	equipamentos: Equipamento[] = [];
	tecnicos: Tecnico[] = [];
	usuarios: Usuario[] = [];
	locaisAtendimento: LocalAtendimento[] = [];
	pas: number[] = [];
	validaCliente: boolean = this._userService.isCustomer;
	pontosEstrategicos: any;
	clienteFilterCtrl: FormControl = new FormControl();
	filialFilterCtrl: FormControl = new FormControl();
	regiaoFilterCtrl: FormControl = new FormControl();
	autorizadaFilterCtrl: FormControl = new FormControl();
	localAtendimentoFilterCtrl: FormControl = new FormControl();
	statusServicoFilterCtrl: FormControl = new FormControl();
	tipoIntervencaoFilterCtrl: FormControl = new FormControl();
	tecnicoFilterCtrl: FormControl = new FormControl();
	tecnicoSTNFilterCtrl: FormControl = new FormControl();
	equipamentoCtrl: FormControl = new FormControl();

	readonly separatorKeysCodes = [ENTER, COMMA] as const;
	protected _onDestroy = new Subject<void>();

	constructor(
		private _filialService: FilialService,
		private _tipoIntervencaoService: TipoIntervencaoService,
		private _equipamentoService: EquipamentoService,
		private _statusServicoService: StatusServicoService,
		private _clienteService: ClienteService,
		private _regiaoAutorizadaService: RegiaoAutorizadaService,
		private _autorizadaService: AutorizadaService,
		private _tecnicoService: TecnicoService,
		protected _userService: UserService,
		private _localAtendimentoSvc: LocalAtendimentoService,
		private _usuarioService: UsuarioService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'ordem-servico');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	loadData(): void {
		this.pontosEstrategicos = PontoEstrategicoEnum;
		this.chamadosPerto = this.filter?.parametros['codOSs'] ? this.filter?.parametros['codOSs']?.split(',') : [];

		this.obterFiliais();
		this.obterClientes();
		this.obterTiposIntervencao();
		this.obterStatusServicos();
		this.obterUsuarios();
		this.registrarEmitters();

		this.aoSelecionarFilial();
		this.aoSelecionarCliente();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codFiliais: [undefined],
			codRegioes: [undefined],
			codTecnicos: [undefined],
			codAutorizadas: [undefined],
			codTiposIntervencao: [undefined],
			codClientes: [undefined],
			codStatusServicos: [undefined],
			codOSs: [undefined],
			numOSCliente: [undefined],
			numOSQuarteirizada: [undefined],
			dataAberturaInicio: [undefined],
			dataAberturaFim: [undefined],
			dataCancelamentoInicio: [undefined],
			dataCancelamentoFim: [undefined],
			dataFechamentoInicio: [undefined],
			dataFechamentoFim: [undefined],
			pas: [undefined],
			codPostos: [undefined],
			pontosEstrategicos: [undefined],
			codEquipamentos: [undefined],
			dataHoraSolucaoInicio: [undefined],
			dataHoraSolucaoFim: [undefined],
			numSerie: [undefined],
			defeito: [undefined],
			solucao: [undefined],
			codUsuariosSTN: [undefined],
		});

		this.form.patchValue(this.filter?.parametros);
	}

	add(event: MatChipInputEvent): void {
		const value = (event.value || '').trim();
		if (value) {
			this.chamadosPerto.push(value);
		}
		event.chipInput!.clear();

		this.form.controls['codOSs'].setValue(this.chamadosPerto?.join(','));
	}

	paste(event: ClipboardEvent): void {	
		event.preventDefault();
		event.clipboardData
			.getData('Text') 
			.split(/;|,|\n/)
			.forEach(value => {
				if (value.trim()) {
					this.chamadosPerto.push(value.trim());
				}
			})
		this.form.controls['codOSs'].setValue(this.chamadosPerto?.join(','));
	}

	remove(os: any): void {
		const index = this.chamadosPerto.indexOf(os);

		if (index >= 0) {
			this.chamadosPerto.splice(index, 1);
		}

		this.form.controls['codOSs'].setValue(this.chamadosPerto?.join(','));
	}

	async obterFiliais(nomeFilial: string = '') {
		const data = await this._filialService
			.obterPorParametros({})
			.toPromise();

		this.filiais = data.items;
	}

	async obterEquipamentos(filtro: string = '') {
		const data = await this._equipamentoService
			.obterPorParametros({
				codClientes: this.form.controls['codClientes'].value,
				filterType: EquipamentoFilterEnum.FILTER_CHAMADOS,
				filter: filtro
			})
			.toPromise();

		this.equipamentos = data.items;
	}

	async obterTiposIntervencao() {
		let params = {
			indAtivo: statusConst.ATIVO,
			filter: this.tipoIntervencaoFilterCtrl.value,
			sortActive: 'nomTipoIntervencao',
			sortDirection: 'asc'
		}

		const data = await this._tipoIntervencaoService
			.obterPorParametros(params)
			.toPromise();

		this.tiposIntervencao = data.items;
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

	async obterTecnicos(filialFilter: string) {
		let params: TecnicoParameters = {
			indAtivo: statusConst.ATIVO,
			filter: this.tecnicoFilterCtrl.value,
			sortActive: 'nome',
			sortDirection: 'asc',
			codPerfil: 35,
			codFiliais: filialFilter,
			pageSize: 1000
		};

		const data = await this._tecnicoService
			.obterPorParametros(params)
			.toPromise();

		this.tecnicos = data.items;
	}

	async obterUsuarios(filtro: string = '') {
		let params: UsuarioParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			codPerfis: PerfilEnum.FILIAL_SUPORTE_TÉCNICO_CAMPO.toString(),
			sortActive: 'nomeUsuario',
			sortDirection: 'asc',
			pageSize: 100
		};

		const data = await this._usuarioService.obterPorParametros(params).toPromise();
		this.usuarios = data.items;
	}

	aoSelecionarFilial() {
		this.form.controls['codFiliais']
			.valueChanges
			.subscribe(() => {
				if ((this.form.controls['codFiliais'].value && this.form.controls['codFiliais'].value != '')) {
					var filialFilter: any = this.form.controls['codFiliais'].value;

					this.obterTecnicos(filialFilter);
					this.obterRegioesAutorizadas(filialFilter);
					this.obterAutorizadas(filialFilter);

					this.form.controls['pas'].enable();
					this.form.controls['codRegioes'].enable();
					this.form.controls['codTecnicos'].enable();
					this.form.controls['codAutorizadas'].enable();
				}
				else {
					this.form.controls['pas'].disable();
					this.form.controls['codRegioes'].disable();
					this.form.controls['codTecnicos'].disable();
					this.form.controls['codAutorizadas'].disable();
				}
			});

		if (this.userSession.usuario.codFilial) {
			this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
			this.form.controls['codFiliais'].disable();
		}
		else {
			this.form.controls['codFiliais'].enable();
		}
	}

	aoSelecionarCliente() {
		if (
			this.form.controls['codClientes'].value &&
			this.form.controls['codClientes'].value != ''
		) {
			this.obterEquipamentos();
			this.obterLocaisAtendimentos();
			this.form.controls['codPostos'].enable();
			this.form.controls['codEquipamentos'].enable();
		}
		else {
			this.form.controls['codPostos'].disable();
			this.form.controls['codEquipamentos'].disable();
		}

		this.form.controls['codClientes']
			.valueChanges
			.subscribe(() => {
				if (this.form.controls['codClientes'].value && this.form.controls['codClientes'].value != '') {
					this.obterEquipamentos();
					this.obterLocaisAtendimentos();
					this.form.controls['codPostos'].enable();
					this.form.controls['codEquipamentos'].enable();
				}
				else {
					this.form.controls['codPostos'].setValue(null);
					this.form.controls['codPostos'].disable();

					this.form.controls['codEquipamentos'].setValue(null);
					this.form.controls['codEquipamentos'].disable();
				}
			});
	}

	async obterRegioesAutorizadas(filialFilter: any) {
		let params: RegiaoAutorizadaParameters = {
			indAtivo: statusConst.ATIVO,
			codFiliais: filialFilter,
			filter: this.regiaoFilterCtrl?.value,
			pageSize: 1000
		};

		const data = await this._regiaoAutorizadaService
			.obterPorParametros(params)
			.toPromise();

		this.regioes = Enumerable.from(data.items).where(ra => ra.regiao?.indAtivo == 1).select(ra => ra.regiao).distinct(r => r.codRegiao).orderBy(i => i.nomeRegiao).toArray();
		this.pas = Enumerable.from(data.items).select(ra => ra.pa).distinct(r => r).orderBy(i => i).toArray();
	}

	async obterAutorizadas(autorizadaFilter: any) {
		let params: AutorizadaParameters = {
			indAtivo: statusConst.ATIVO,
			codFiliais: autorizadaFilter,
			filter: this.autorizadaFilterCtrl.value,
			pageSize: 1000
		};

		const data = await this._autorizadaService
			.obterPorParametros(params)
			.toPromise();

		this.autorizadas = Enumerable.from(data.items).orderBy(i => i.nomeFantasia).toArray();
	}

	async obterLocaisAtendimentos(localFiltro: string = '') {
		var filialFilter = this.form.controls['codFiliais'].value;
		var clienteFilter = this.form.controls['codClientes'].value;
		var regiaoFilter = this.form.controls['codRegioes'].value;
		var autorizadaFilter = this.form.controls['codAutorizadas'].value;

		let params: LocalAtendimentoParameters = {
			indAtivo: statusConst.ATIVO,
			codFiliais: filialFilter,
			codClientes: clienteFilter,
			codRegioes: regiaoFilter,
			codAutorizada: autorizadaFilter,
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

	async obterStatusServicos() {
		let params: StatusServicoParameters = {
			indAtivo: statusConst.ATIVO,
			filter: this.statusServicoFilterCtrl.value,
			sortActive: 'nomeStatusServico',
			sortDirection: 'asc'
		}

		const data = await this._statusServicoService
			.obterPorParametros(params)
			.toPromise();

		this.statusServicos = data.items;
	}

	async registrarEmitters() {
		this.form.controls['codOSs'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				if(this.chamadosPerto == null){
					this.chamadosPerto = this.filter?.parametros['codOSs'] ? this.filter?.parametros['codOSs']?.split(',') : [];
				}
			});

		this.clienteFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterClientes(this.clienteFilterCtrl.value);
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

		this.filialFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterFiliais(this.filialFilterCtrl.value);
			});

		this.regiaoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterRegioesAutorizadas(this.form.controls['codFiliais'].value);
			});

		this.autorizadaFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterAutorizadas(this.form.controls['codFiliais'].value);
			});

		this.statusServicoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterStatusServicos();
			});

		this.tipoIntervencaoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterTiposIntervencao();
			});

		this.tecnicoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterTecnicos(this.form.controls['codFiliais'].value);
			});

		this.equipamentoCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterEquipamentos(this.equipamentoCtrl.value);
			});
	}

	limpar() {
		super.limpar();
		this.chamadosPerto = [];
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