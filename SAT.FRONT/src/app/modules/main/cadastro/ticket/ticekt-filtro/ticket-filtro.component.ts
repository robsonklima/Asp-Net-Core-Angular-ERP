import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { TicketClassificacao, TicketModulo, TicketModuloParameters, TicketPrioridade, TicketStatus, TicketStatusParameters } from 'app/core/types/ticket.types';
import { Usuario, UsuarioParameters } from 'app/core/types/usuario.types';
import { TicketModuloService } from 'app/core/services/ticket-modulo.service';
import { TicketStatusService } from 'app/core/services/ticket-status.service';


@Component({
	selector: 'app-ticket-filtro',
	templateUrl: './ticket-filtro.component.html'
})
export class TicketFiltroComponent extends FilterBase implements OnInit, IFilterBase {

	@Input() sidenav: MatSidenav;

	modulos: TicketModulo[] = [];
	modulosFilterCtrl: FormControl = new FormControl();
	status: TicketStatus[] = [];
	statusFilterCtrl: FormControl = new FormControl();
	classificacoes: TicketClassificacao[] = [];
	classificacoesFilterCtrl: FormControl = new FormControl();
	prioridades: TicketPrioridade[] = [];
	prioridadesFilterCtrl: FormControl = new FormControl();
	usuarios: Usuario[] = [];
	usuariosFilterCtrl: FormControl = new FormControl();
	protected _onDestroy = new Subject<void>();

	constructor(

		private _ticketModuloService: TicketModuloService,
		private _ticketStatusService: TicketStatusService,
		// private _ticketPrioridadeService: TicketPrioridadeService,
		// private _ticketClassificacoesService: TicketClassisifacaoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'ticket');
	}

	ngOnInit(): void {
		this.createForm();
		this.loadData();
	}

	async loadData() {
		this.obterModulos();
		this.obterStatus();
		// this.obterClassificacoes();
		// this.obterPrioridades();
		this.obterUsuarios();
		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codModulo: [undefined],
			codStatus: [undefined],
			codPrioridade: [undefined],
			codClassificacao: [undefined],
			codUsuario: [undefined],
		});
		this.form.patchValue(this.filter?.parametros);
	}

	async obterModulos(filtro: string = '') {
		let params: TicketModuloParameters = {
			filter: filtro,
			sortActive: 'descricao',
			sortDirection: 'asc',
			pageSize: 1000,
			codModulo: 0
		};
		const data = await this._ticketModuloService
			.obterPorParametros(params)
			.toPromise();
		this.modulos = data.items;
	}

	async obterStatus(filtro: string = '') {
		let params: TicketStatusParameters = {
			filter: filtro,
			sortActive: 'descricao',
			sortDirection: 'asc',
			pageSize: 1000,
			codStatus: 0
		};
		const data = await this._ticketStatusService
			.obterPorParametros(params)
			.toPromise();
		this.status = data.items;
	}

	async obterUsuarios(filtro: string = '') {
		let params: UsuarioParameters = {
			filter: filtro,
			sortActive: 'nomeUsuario',
			sortDirection: 'asc',
			pageSize: 1000
		};
		const data = await this._userService
			.obterPorParametros(params)
			.toPromise();
		this.usuarios = data.items;
	}

	// async obterPrioridades(filtro: string = '') {
	// 	let params: TicketPrioridadeParameters = {
	// 		filter: filtro,
	// 		sortActive: 'descricao',
	// 		sortDirection: 'asc',
	// 		pageSize: 1000
	// 	};
	// 	const data = await this._ticketPrioridadesService
	// 		.obterPorParametros(params)
	// 		.toPromise();
	// 	this.prioridades = data.items;
	// }

	// async obterClassificacoes(filtro: string = '') {
	// 	let params: TicketClassificacoesParameters = {
	// 		filter: filtro,
	// 		sortActive: 'descricao',
	// 		sortDirection: 'asc',
	// 		pageSize: 1000
	// 	};
	// 	const data = await this._ticketClassificacoesService
	// 		.obterPorParametros(params)
	// 		.toPromise();
	// 	this.classificacoes = data.items;
	// }

	private registrarEmitters() {
		this.modulosFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterModulos(this.modulosFilterCtrl.value);
			});

		this.statusFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterStatus(this.statusFilterCtrl.value);
			});

			this.usuariosFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(700),
				distinctUntilChanged()
			)
			.subscribe(() => {
				this.obterUsuarios(this.usuariosFilterCtrl.value);
			});

		// this.classificacoesCtrl.valueChanges
		// 	.pipe(
		// 		takeUntil(this._onDestroy),
		// 		debounceTime(700),
		// 		distinctUntilChanged()
		// 	)
		// 	.subscribe(() => {
		// 		this.obterClassificacoes(this.classificacoesFilterCtrl.value);
		// 	});

	// 	this.prioridadesFilterCtrl.valueChanges
	// 		.pipe(
	// 			takeUntil(this._onDestroy),
	// 			debounceTime(700),
	// 			distinctUntilChanged()
	// 		)
	// 		.subscribe(() => {
	// 			this.obterPrioridades(this.prioridadesFilterCtrl.value);
	// 		});
	}

	limpar() {
		super.limpar();

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}