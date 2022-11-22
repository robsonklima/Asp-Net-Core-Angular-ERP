import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { NotificacaoService } from 'app/core/services/notificacao.service';
import { TicketAnexoService } from 'app/core/services/ticket-anexo.service';
import { statusConst } from 'app/core/types/status-types';
import { TicketAnexo } from 'app/core/types/ticket-anexo.types';
import { Ticket } from 'app/core/types/ticket.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { MessagesComponent } from 'app/layout/common/messages/messages.component';
import moment from 'moment';
import { Observable, ReplaySubject } from 'rxjs';

@Component({
  selector: 'app-ticket-anexo-form-dialog',
  templateUrl: './ticket-anexo-form-dialog.component.html'
})
export class TicketAnexoFormDialogComponent implements OnInit {
  ticket: Ticket;
  ticketAnexo: TicketAnexo;
  userSession: UserSession;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    public _dialogRef: MatDialogRef<MessagesComponent>,
    private _userService: UserService,
    private _snack: CustomSnackbarService,
    private _ticketAnexoService: TicketAnexoService,
    private _notificacaoService: NotificacaoService
  ) {
    this.ticket = data?.ticket;
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.ticketAnexo = {
      codTicket: this.ticket.codTicket,
      indAtivo: statusConst.ATIVO,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
      base64: '',
      nome: '',
      tipo: '',
      tamanho: 0
    }
  }

  async onFileSelected(event) {
    await this.convertFile(event.target.files[0]).subscribe(base64 => {
      this.ticketAnexo.base64 = base64;
      this.ticketAnexo.nome = this.getFileName(base64);
      this.ticketAnexo.tamanho = this.getFileSize(base64);
      this.ticketAnexo.tipo = this.getExtension(base64.substring(0, 5));

      if (!this.ticketAnexo.tipo)
        return this._snack.exibirToast('Arquivo não suportado', 'error');
      
      if (this.ticketAnexo.tamanho > 2000000)
        return this._snack.exibirToast('O tamanho máximo para anexos é de 2mb', 'error');

      this.salvar();
      this.enviarNotificacoes();
    });
  }

  enviarNotificacoes() {
    let destinatario = '';

    if (this.userSession.usuario.codUsuario == this.ticket.codUsuarioCad)
      destinatario = this.ticket?.atendimentos[0]?.codUsuarioCad;
    else 
      destinatario = this.ticket.codUsuarioCad;

    if (destinatario)
      this._notificacaoService.criar({
        codUsuarioCad: this.userSession.usuario.codUsuario,
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        titulo: `Arquivo anexado no ticket #${this.ticket.codTicket}`,
        descricao: `Arquivo anexado por ${this.userSession.usuario.codUsuario}`,
        codUsuario: destinatario,
        indAtivo: statusConst.ATIVO
      }).toPromise();
  }

  convertFile(file: File): Observable<string> {
    const result = new ReplaySubject<string>(1);
    const reader = new FileReader();
    reader.readAsBinaryString(file);
    reader.onload = (event) => result.next(btoa(event.target.result.toString()));
    return result;
  }

  getFileName(base64: string): string {
    return moment().valueOf() + '.' + this.getExtension(base64.substring(0, 5))
  }

  getFileSize(base64: string): number {
    const decoded = atob(base64);
    return decoded.length;
  }

  salvar() {
    this._ticketAnexoService.criar(this.ticketAnexo).subscribe(() => {
      this._snack.exibirToast('Arquivo anexado com sucesso', 'success');
      this._dialogRef.close(true);
    });
  }

  getExtension(str: string) {
    switch (str.toUpperCase())
    {
      case "IVBOR":
        return "png";
      case "/9J/4":
        return "jpg";
      case "AAAAF":
        return "mp4";
      case "JVBER":
        return "pdf";
      case "AAABA":
        return "ico";
      case "UMFYI":
        return "rar";
      case "E1XYD":
        return "rtf";
      case "U1PKC":
        return "txt";
      case "MQOWM":
      case "77U/M":
        return "srt";
      case "0M8R4":
        return "xls";
      case "UESDB":
        return "xlsx";
      default:
        return '';
    }
  }
}
