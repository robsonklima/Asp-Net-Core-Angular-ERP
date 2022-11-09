import { Location } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { NotificacaoService } from 'app/core/services/notificacao.service';
import { TicketAtendimentoService } from 'app/core/services/ticket-atendimento.service';
import { TicketStatusService } from 'app/core/services/ticket-status.service';
import { TicketService } from 'app/core/services/ticket.service';
import { Notificacao } from 'app/core/types/notificacao.types';
import { statusConst } from 'app/core/types/status-types';
import { Ticket, TicketAtendimento, TicketStatus } from 'app/core/types/ticket.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { TicketFormComponent } from '../ticket-form/ticket-form.component';

@Component({
  selector: 'app-ticket-atendimento-form-dialog',
  templateUrl: './ticket-atendimento-form-dialog.component.html'
})
export class TicketAtendimentoFormDialogComponent implements OnInit {
  ticket: Ticket;
  ticketAtendimento: TicketAtendimento;
  status: TicketStatus[] = [];
  form: FormGroup;
  isLoading: boolean;
  userSession: UserSession;

  constructor(
    private _ticketStatusService: TicketStatusService,
    private _ticketAtendimentoService: TicketAtendimentoService,
    private _notificacaoService: NotificacaoService,
    private _ticketService: TicketService,
    @Inject(MAT_DIALOG_DATA) protected data: any,
    protected dialogRef: MatDialogRef<TicketFormComponent>,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _formBuilder: FormBuilder,
    private _userService: UserService
  ) {
    if (data)
      this.ticket = data?.ticket;
      this.ticketAtendimento = data?.ticketAtendimento;

    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.criarForm();
    this.obterDados();
  }

  criarForm() {
    this.form = this._formBuilder.group({
      descricao: [undefined, Validators.required],
      codStatus: [undefined, Validators.required],
    });
  }

  private async obterDados() {
		this.status = (await this._ticketStatusService
			.obterPorParametros({ sortActive: 'descricao', sortDirection: 'asc' })
			.toPromise()).items;

		if (this.ticketAtendimento) {
			this.form.patchValue(this.ticketAtendimento);
		}

		this.isLoading = false;
	}

  salvar() {
    this.ticketAtendimento ? this.atualizar() : this.criar();
  }

  private async atualizar() {
		let obj: TicketAtendimento = {
			...this.ticketAtendimento,
			...this.form.getRawValue(),
			...{
        codTicket: this.ticket.codTicket,
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario.codUsuario
			}
		};

		this._ticketAtendimentoService.atualizar(obj).subscribe((t) => {
			this._snack.exibirToast(`Ticket ${t?.codTicket} atualizado com sucesso!`, "success");
			this.dialogRef.close(t);
		});
  }

  private async criar() {
    this.ticket = await this.atualizarTicket();
    this.ticketAtendimento = await this.criarAtendimento();
    await this.enviarNotificacaoAoUsuario();
    this._snack.exibirToast('Atendimento criado com sucesso', 'success');
    this.dialogRef.close(this.ticket);
  }

  private async criarAtendimento(): Promise<TicketAtendimento> {
    let obj: TicketAtendimento = {
			...this.form.getRawValue(),
			...{
        codTicket: this.ticket.codTicket,
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario.codUsuario,
			}
		};

    return this._ticketAtendimentoService.criar(obj).toPromise();
  }

  private async atualizarTicket(): Promise<Ticket> {
    return this._ticketService.atualizar({
      ...this.ticket,
      ...{
        codStatus: this.form.controls['codStatus'].value,
        codUsuarioManut: this.userSession.usuario.codUsuario,
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss')
      }
    }).toPromise()
  }

  private async enviarNotificacaoAoUsuario(): Promise<Notificacao> {
    const notificacao: Notificacao = {
      indAtivo: statusConst.ATIVO,
      dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
      codUsuario: this.ticket.codUsuarioCad,
      titulo: 'Atualização do seu ticket',
      descricao: `Ticket #${ this.ticket.codTicket } atualizado por ${ this.userSession.usuario.nomeUsuario }. 
        Relato do atendente: ${ this.ticketAtendimento.descricao }`
    }

    return this._notificacaoService.criar(notificacao).toPromise();
  }
}
