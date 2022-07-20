import { UserService } from '../../../../../core/user/user.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { MatSidenav } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { FilterBase } from '../../../../../core/filters/filter-base';
import { IFilterBase } from '../../../../../core/types/filtro.types';
import { Ticket, TicketClassificacao, TicketModulo, TicketPrioridade, TicketStatus } from 'app/core/types/ticket.types';
import { TicketModuloService } from 'app/core/services/ticket-modulo.service';
import { TicketStatusService } from 'app/core/services/ticket-status.service';
import { TicketPrioridadeService } from 'app/core/services/ticket-prioridade.service';
import { TicketClassificacaoService } from 'app/core/services/ticket-classificacao.service';



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
		private _ticketPrioridadeService: TicketPrioridadeService,
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
	  this.modulos = (await this._ticketModuloService.obterPorParametros(null).toPromise()).items
	  this.status = (await this._ticketStatusService.obterPorParametros(null).toPromise()).items
	  this.prioridades = (await this._ticketPrioridadeService.obterPorParametros(null).toPromise()).items
	  this.classificacoes = (await this._ticketClassificacaoService.obterPorParametros(null).toPromise()).items
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