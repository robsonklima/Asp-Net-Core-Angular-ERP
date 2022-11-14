import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TicketService } from 'app/core/services/ticket.service';
import { TicketAnexo } from 'app/core/types/ticket-anexo.types';
import { Ticket } from 'app/core/types/ticket.types';
import { TicketAnexoFormDialogComponent } from './ticket-anexo-form-dialog/ticket-anexo-form-dialog.component';

@Component({
  selector: 'app-ticket-anexos',
  templateUrl: './ticket-anexos.component.html'
})
export class TicketAnexosComponent implements OnInit {
  @Input() ticket: Ticket;
  isLoading: boolean;

  constructor(
    private _dialog: MatDialog,
    private _ticketService: TicketService
  ) { }

  ngOnInit(): void {

  }

  abrirAnexosForm() {
    const dialogRef = this._dialog.open(TicketAnexoFormDialogComponent, {
		  data: {
        ticket: this.ticket
		  },
		  width: '680px'
		});
	
		dialogRef.afterClosed().subscribe(async (anexo: TicketAnexo) => {
		  if (anexo)
			  this.ticket = await this._ticketService.obterPorCodigo(this.ticket.codTicket).toPromise();
		});
  }
}
