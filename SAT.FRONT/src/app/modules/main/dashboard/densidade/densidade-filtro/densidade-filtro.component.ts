import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Regiao } from 'app/core/types/regiao.types';
import { FilialService } from 'app/core/services/filial.service';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { statusConst } from '../../../../../core/types/status-types';
import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { RegiaoAutorizadaParameters } from 'app/core/types/regiao-autorizada.types';
import Enumerable from 'linq';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { takeUntil, debounceTime, distinctUntilChanged, filter } from 'rxjs/operators';
import { ClienteService } from 'app/core/services/cliente.service';
import { FilialEnum } from 'app/core/enums/FilialEnum';
import { Equipamento, EquipamentoFilterEnum } from 'app/core/types/equipamento.types';
import { EquipamentoService } from 'app/core/services/equipamento.service';


@Component({
	selector: 'app-densidade-filtro',
	templateUrl: './densidade-filtro.component.html'
})
export class DensidadeFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	regiaoFilterCtrl: FormControl = new FormControl();
	autorizadaFilterCtrl: FormControl = new FormControl();
	equipamentoCtrl: FormControl = new FormControl();
	filialFilterCtrl: FormControl = new FormControl();
	clienteFilterCtrl: FormControl = new FormControl();
	filiais: Filial[] = [];
	regioes: Regiao[] = [];
	clientes: Cliente[] = [];
	autorizadas: Autorizada[] = [];
	equipamentos: Equipamento[] = [];

	protected _onDestroy = new Subject<void>();

	constructor(
		private _filialService: FilialService,
		private _clienteService: ClienteService,
		private _regiaoAutorizadaService: RegiaoAutorizadaService,
		private _autorizadaService: AutorizadaService,
		private _equipamentoService: EquipamentoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'dashboard-densidade');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterFiliais();
		this.obterRegioesAutorizadas();
		this.obterAutorizadas();

		this.registrarEmmiters();

	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codFiliais: [undefined],
			codRegioes: [undefined],
			codAutorizadas: [undefined],
			codClientes: [undefined],
			codEquips: [undefined],
			exibirTecnicos: [undefined]
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

	async obterFiliais(nomeFilial: string = '') {
		let params: FilialParameters = {
			filter: nomeFilial,
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeFilial',
			sortDirection: 'asc'
		};

		const data = await this._filialService
			.obterPorParametros(params)
			.toPromise();

		this.filiais = data.items.filter(f => f.codFilial != FilialEnum.DSR);
	}

	async obterRegioesAutorizadas(filialFilter: any = '') {
		let params: RegiaoAutorizadaParameters = {
			indAtivo: statusConst.ATIVO,
			codFiliais: this.form.controls['codFiliais'].value,
			filter: filialFilter,
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
			codFiliais: this.form.controls['codFiliais'].value,
			filter: filialFilter,
			pageSize: 1000
		};

		const data = await this._autorizadaService
			.obterPorParametros(params)
			.toPromise();

		this.autorizadas = Enumerable.from(data.items).orderBy(i => i.nomeFantasia).toArray();
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

	registrarEmmiters() {
		this.filialFilterCtrl
			.valueChanges
			.subscribe(() => {
				if ((this.form.controls['codFiliais'].value && this.form.controls['codFiliais'].value != '')) {

					this.obterRegioesAutorizadas();
					this.obterAutorizadas();
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

		this.regiaoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterRegioesAutorizadas(this.regiaoFilterCtrl.value);
			});

		this.autorizadaFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterAutorizadas(this.autorizadaFilterCtrl.value);
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

		if (this.userSession?.usuario?.codFilial) {
			this.form.controls['codFilial'].setValue([this.userSession.usuario.codFilial]);
			this.form.controls['codFilial'].disable();
		}
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}