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
	tipoEquipamentos: TipoEquipamento[] = [];
	grupoEquipamentos: GrupoEquipamento[] = [];
	tipoContratos: TipoContrato[] = [];
	tipoContratoFilterCtrl: FormControl = new FormControl();
	tipoEquipamentoFilterCtrl: FormControl = new FormControl();
	grupoEquipamentoFilterCtrl: FormControl = new FormControl();
	codClientesSelected: string;
	protected _onDestroy = new Subject<void>();

	constructor(

		private _clienteService: ClienteService,
		private _contratoService: ContratoService,
		private _tipoEquipamentoService: TipoEquipamentoService,
		private _grupoEquipamentoService: GrupoEquipamentoService,
		private _tipoContratoService: TipoContratoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'equipamento-contrato');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	loadData(): void {
		this.obterClientes();
		this.obterTipoContratos();
		this.obterTipoEquipamentos();
		this.obterGrupoEquipamentos();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codClientes: [undefined],
			indAtivo: [undefined],
			codTipoContratos: [undefined],
			codTipoEquips: [undefined],
			codGrupoEquips: [undefined],
			codContratos: [undefined]
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
			codClientes: this.codClientesSelected,
			sortActive: 'nomeContrato',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._contratoService
			.obterPorParametros(params)
			.toPromise();
		this.contratos = data.items;
	}

	limpar() {
		super.limpar();

		if (this.userSession?.usuario?.codFilial) {
			this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
			this.form.controls['codFiliais'].disable();
		}
	}

	private registrarEmitters() {
		this.clienteFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.codClientesSelected = this.form.controls['codClientes'].value.join(',');
				this.obterContratos();
				this.obterClientes(this.clienteFilterCtrl.value);
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

		this.tipoEquipamentoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterTipoEquipamentos(this.tipoEquipamentoFilterCtrl.value);
			});

		this.grupoEquipamentoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterGrupoEquipamentos(this.grupoEquipamentoFilterCtrl.value);
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}