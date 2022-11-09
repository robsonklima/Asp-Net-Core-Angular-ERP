import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { TicketClassificacaoService } from 'app/core/services/ticket-classificacao.service';
import { TicketModuloService } from 'app/core/services/ticket-modulo.service';
import { TicketStatusService } from 'app/core/services/ticket-status.service';
import { TicketClassificacao, TicketModulo, TicketPrioridade, TicketStatus } from 'app/core/types/ticket.types';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { UserService } from '../../../../../core/user/user.service';

@Component({
	selector: 'app-ticket-filtro',
	templateUrl: './ticket-filtro.component.html'
})
export class TicketFiltroComponent extends FilterBase implements OnInit, IFilterBase {

@Input() sidenav: MatSidenav;
	modulos: TicketModulo[];
	status: TicketStatus[];
	classificacoes: TicketClassificacao[];
	prioridades: TicketPrioridade[];
	protected _onDestroy = new Subject<void>();

	constructor(
		private _ticketModuloService: TicketModuloService,
		private _ticketStatusService: TicketStatusService,
		private _ticketClassificacaoService: TicketClassificacaoService,
		protected _userService: UserService,
		protected _formBuilder: FormBuilder
	) {
		super(_userService, _formBuilder, 'ticket');
	}

	ngOnInit(): void {
		this.obterDados();
		this.createForm();
		this.loadData();
		
	}
	async obterDados() {
	  this.modulos = (await this._ticketModuloService.obterPorParametros({}).toPromise()).items
	  this.status = (await this._ticketStatusService.obterPorParametros({}).toPromise()).items
	  this.classificacoes = (await this._ticketClassificacaoService.obterPorParametros({}).toPromise()).items
	}

	async loadData() {

		this.registrarEmitters();
	}

	createForm(): void {
		this.form = this._formBuilder.group({
			codModulo: [undefined],
			codStatus: [undefined],
			codPrioridade: [undefined],
			codClassificacao: [undefined],

		});
		this.form.patchValue(this.filter?.parametros);
	}

	private registrarEmitters() {

	}

	limpar() {
		super.limpar();
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}