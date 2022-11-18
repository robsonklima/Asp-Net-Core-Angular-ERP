import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { TicketAnexoService } from 'app/core/services/ticket-anexo.service';
import { TicketService } from 'app/core/services/ticket.service';
import { TicketAnexo } from 'app/core/types/ticket-anexo.types';
import { Ticket, ticketStatusConst } from 'app/core/types/ticket.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
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
    private _ticketService: TicketService,
    private _ticketAnexoService: TicketAnexoService,
    private _snack: CustomSnackbarService
  ) { }

  ngOnInit(): void {

  }

  obterTipo(nomeArquivo: string): string {
    if (!nomeArquivo)
      return '';

    return nomeArquivo.split('.')[1].toUpperCase();
  }

  abrirAnexosForm() {
    const dialogRef = this._dialog.open(TicketAnexoFormDialogComponent, {
      data: {
        ticket: this.ticket
      },
      width: '680px'
    });

    dialogRef.afterClosed().subscribe(async (conf: boolean) => {
      if (conf)
        this.ticket = await this._ticketService.obterPorCodigo(this.ticket.codTicket).toPromise();
    });
  }

  async deletar(codigo: number) {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja excluir este ticket?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe(async (conf: boolean) => {
      if (conf) {
        await this._ticketAnexoService.deletar(codigo).toPromise();
        this.ticket = await this._ticketService.obterPorCodigo(this.ticket.codTicket).toPromise();
        this._snack.exibirToast('Anexo removido com sucesso', 'success');
      }
    });
  }

  download(anexo: TicketAnexo) {
    const downloadLink = document.createElement('a');
    const fileName = anexo.nome;
    downloadLink.href = 'data:application/octet-stream;base64,' + anexo.base64;
    downloadLink.download = fileName;
    downloadLink.click();
  }

  isEditavel(): boolean {
		return this.ticket?.codStatus == ticketStatusConst.AGUARDANDO || 
			     this.ticket?.codStatus == ticketStatusConst.EM_ATENDIMENTO;
	}
}
