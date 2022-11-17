import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { TicketAnexoService } from 'app/core/services/ticket-anexo.service';
import { statusConst } from 'app/core/types/status-types';
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
  form: FormGroup;
  ticket: Ticket;
  userSession: UserSession;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    public _dialogRef: MatDialogRef<MessagesComponent>,
    private _formBuilder: FormBuilder,
    private _userService: UserService,
    private _snack: CustomSnackbarService,
    private _ticketAnexoService: TicketAnexoService
  ) {
    this.ticket = data?.ticket;
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.criarForm();
  }

  criarForm() {
    this.form = this._formBuilder.group({
      base64: [''],
      codTicket: this.ticket.codTicket,
      indAtivo: statusConst.ATIVO,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss')
    });
  }

  onFileSelected(event) {
    this.convertFile(event.target.files[0]).subscribe(base64 => {
      this.form.controls['base64'].setValue(base64);

      this.salvar();
    });
  }

  convertFile(file : File) : Observable<string> {
    const result = new ReplaySubject<string>(1);
    const reader = new FileReader();
    reader.readAsBinaryString(file);
    reader.onload = (event) => result.next(btoa(event.target.result.toString()));
    return result;
  }

  salvar() {
    this._ticketAnexoService.criar(this.form.getRawValue()).subscribe(() => {
      this._snack.exibirToast('Arquivo anexado com sucesso', 'success');
      this._dialogRef.close(true);
    });
  }
}
