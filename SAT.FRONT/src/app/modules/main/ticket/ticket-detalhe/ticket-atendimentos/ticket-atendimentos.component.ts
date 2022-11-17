import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TicketService } from 'app/core/services/ticket.service';
import { Ticket, TicketAtendimento } from 'app/core/types/ticket.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { TicketAtendimentoFormDialogComponent } from './ticket-atendimento-form-dialog/ticket-atendimento-form-dialog.component';

@Component({
	selector: 'app-ticket-atendimentos',
	templateUrl: './ticket-atendimentos.component.html'
})
export class TicketAtendimentosComponent implements OnInit, OnChanges {
	@Input() ticket: Ticket;
	@Output() ticketEvent = new EventEmitter<Ticket>();
	userSession: UserSession;
	isLoading: boolean;

	constructor(
		private _dialog: MatDialog,
		private _userService: UserService,
		private _ticketService: TicketService
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngOnInit(): void {}

	ngOnChanges(changes: SimpleChanges) {
		this.ticket = changes.ticket.currentValue;
	}

	abrirAtendimentoForm(ticketAtendimento: TicketAtendimento = null) {
		const dialogRef = this._dialog.open(TicketAtendimentoFormDialogComponent, {
			data: {
				ticket: this.ticket,
				ticketAtendimento: ticketAtendimento
			},
			width: '680px'
		});

		dialogRef.afterClosed().subscribe(async (conf: boolean) => {
			if (conf) {
				this.ticket = await this._ticketService.obterPorCodigo(this.ticket.codTicket).toPromise();
				this.ticketEvent.emit(this.ticket);
			}
		});
	}
}
