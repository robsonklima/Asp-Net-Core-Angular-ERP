import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { ClienteService } from 'app/core/services/cliente.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { FilialService } from 'app/core/services/filial.service';
import { OrdemServicoSTNOrigemService } from 'app/core/services/ordem-servico-stn-origem.service';
import { OrdemServicoSTNOrigem, OrdemServicoSTNOrigemParameters } from 'app/core/types/ordem-servico-stn-origem.types';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Equipamento, EquipamentoFilterEnum, EquipamentoParameters } from 'app/core/types/equipamento.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { IFilterBase } from 'app/core/types/filtro.types';
import { statusConst } from 'app/core/types/status-types';
import { UserService } from 'app/core/user/user.service';
import { Usuario, UsuarioParameters } from 'app/core/types/usuario.types';
import { UsuarioService } from 'app/core/services/usuario.service';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs/internal/Subject';
import { StatusServicoSTN, StatusServicoSTNParameters } from 'app/core/types/status-servico-stn.types';
import { StatusServicoSTNService } from 'app/core/services/status-servico-stn.service';

@Component({
	selector: 'app-ordem-servico-stn-filtro',
	templateUrl: './ordem-servico-stn-filtro.component.html'
})
export class OrdemServicoSTNFiltroComponent extends FilterBase implements OnInit, IFilterBase {
	@Input() sidenav: MatSidenav;
	clientes: Cliente[] = [];
	filiais: Filial[] = [];
	equips: Equipamento[] = [];
	origens: OrdemServicoSTNOrigem[] = [];
	usuarios: Usuario[] = [];
	status: StatusServicoSTN [] = [];
	clientesFiltro: FormControl = new FormControl();
	equipsFiltro: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(
		protected _userService: UserService,
		protected _formBuilder: FormBuilder,
		private _clienteService: ClienteService,
		private _filialService: FilialService,
		private _equipamentoService: EquipamentoService,
		private _osSTNOrigemService: OrdemServicoSTNOrigemService,
		private _statusServicoSTNService: StatusServicoSTNService,
		private _usuarioService: UsuarioService
	) {
		super(_userService, _formBuilder, 'ordem-servico-stn');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
		this.registrarEmitters();
	}

	loadData(): void {
		this.obterClientes();
		this.obterFiliais();
		this.obterEquipamentos();
		this.obterOSOrigens();
		this.obterUsuarios();
		this.obterStatus();
		this.aoSelecionarCliente();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codClientes: [undefined],
			codFiliais: [undefined],
			codEquips: [undefined],
			codOrigemChamadoSTNs: [undefined],
			codUsuarios: [undefined],
			codStatusServicoSTNs: [undefined],
		});
		this.form.patchValue(this.filter?.parametros);
	}

	registrarEmitters() {
		this.clientesFiltro.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterClientes(this.clientesFiltro.value);
			});

		this.form.controls['codClientes'].valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterEquipamentos(this.clientesFiltro.value);
			});

		this.equipsFiltro.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterEquipamentos(this.equipsFiltro.value);
			});
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

	async obterFiliais(filtro: string = '') {
		let params: FilialParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'nomeFilial',
			sortDirection: 'asc',
			pageSize: 100
		};

		const data = await this._filialService.obterPorParametros(params).toPromise();
		this.filiais = data.items;
	}

	async obterStatus(filtro: string = '') {
		let params: StatusServicoSTNParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'descStatusServicoSTN',
			sortDirection: 'asc',
			pageSize: 100
		};

		const data = await this._statusServicoSTNService.obterPorParametros(params).toPromise();
		this.status = data.items;
	}

	async obterEquipamentos(filtro: string = '') {
		let params: EquipamentoParameters = {
			filter: filtro,
			filterType: EquipamentoFilterEnum.FILTER_CHAMADOS,
			codClientes: this.form.controls['codClientes'].value?.join(','),
			sortActive: 'nomeEquip',
			sortDirection: 'asc',
			pageSize: 100
		};

		const data = await this._equipamentoService.obterPorParametros(params).toPromise();
		this.equips = data.items;
	}

	async obterOSOrigens(filtro: string = '') {
		let params: OrdemServicoSTNOrigemParameters = {
			filter: filtro,
			indAtivo: statusConst.ATIVO,
			sortActive: 'descOrigemChamadoSTN',
			sortDirection: 'asc',
			pageSize: 100
		};

		const data = await this._osSTNOrigemService.obterPorParametros(params).toPromise();
		this.origens = data.items;
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

	aoSelecionarCliente() {
		this.form.controls['codClientes']
			.valueChanges
			.subscribe(() => {
				if (this.form.controls['codClientes'].value && this.form.controls['codClientes'].value != '') {
					this.obterEquipamentos();
					this.form.controls['codEquips'].enable();
				}
				else {
					this.form.controls['codEquips'].setValue(null);
				}
			});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	  }

}
