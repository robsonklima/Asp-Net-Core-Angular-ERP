import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { TicketAtendimentoService } from 'app/core/services/ticket-atendimento.service';
import { TicketService } from 'app/core/services/ticket.service';
import { Ticket, TicketAtendimento, ticketStatusConst } from 'app/core/types/ticket.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import moment from 'moment';

@Component({
	selector: 'app-ticket-detalhe',
	templateUrl: './ticket-detalhe.component.html'
})
export class TicketDetalheComponent implements OnInit {
	codTicket: number;
	ticket: Ticket;
	isLoading: boolean = true;
	userSession: UserSession;

	constructor(
		private _dialog: MatDialog,
		private _ticketService: TicketService,
		private _snack: CustomSnackbarService,
		private _location: Location,
		private _route: ActivatedRoute,
		private _userService: UserService,
		private _ticketAtendimentoService: TicketAtendimentoService
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.codTicket = +this._route.snapshot.paramMap.get('codTicket');

		if (this.codTicket)
			this.ticket = await this._ticketService.obterPorCodigo(this.codTicket).toPromise();

		this.isLoading = false;
	}

	remover() {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: `Deseja remover este ticket?`,
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
			if (confirmacao)
			{
				this.isLoading = true;
				this._ticketService
					.deletar(this.ticket.codTicket)
					.subscribe(() => {
						this._snack.exibirToast(`Registro removido com sucesso`, 'success');
						this._location.back();
					}, (e) => {
						this._snack.exibirToast(e.message || e.error.message, 'error');
						this.isLoading = false;
					});
			}
		});
	}

	async vincularDesvincular() {
		if (!this.ticket.codUsuarioAtendente || this.ticket.codUsuarioAtendente != this.userSession.usuario.codUsuario) {
			this.ticket.codUsuarioAtendente = this.userSession.usuario.codUsuario;
			this.ticket.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');
			this.ticket.codStatus = ticketStatusConst.EM_ATENDIMENTO;

			const atendimento: TicketAtendimento = {
				codStatus: +ticketStatusConst.EM_ATENDIMENTO,
				descricao: 'Em Atendimento',
				codUsuarioCad: this.userSession.usuario.codUsuario,
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codTicket: this.ticket.codTicket
			};

			await this._ticketAtendimentoService.criar(atendimento).subscribe();
		} else {
			this.ticket.codUsuarioAtendente = null;
			this.ticket.codStatus = ticketStatusConst.AGUARDANDO;

			const atendimento: TicketAtendimento = {
				codStatus: +ticketStatusConst.AGUARDANDO,
				descricao: 'Atendimento Pausado',
				codUsuarioCad: this.userSession.usuario.codUsuario,
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codTicket: this.ticket.codTicket
			};

			await this._ticketAtendimentoService.criar(atendimento).subscribe();
		}

		await this._ticketService.atualizar(this.ticket).subscribe();

		setTimeout(() => {
			this.ngOnInit();
		}, 1000);
	}
}
