import { Location } from '@angular/common';
import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { TicketAtendimentoService } from 'app/core/services/ticket-atendimento.service';
import { TicketClassificacaoService } from 'app/core/services/ticket-classificacao.service';
import { TicketModuloService } from 'app/core/services/ticket-modulo.service';
import { TicketPrioridadeService } from 'app/core/services/ticket-prioridade.service';
import { TicketStatusService } from 'app/core/services/ticket-status.service';
import { TicketService } from 'app/core/services/ticket.service';
import { Ticket, TicketAtendimento, TicketClassificacao, TicketModulo, TicketPrioridade, TicketStatus, ticketStatusConst } from 'app/core/types/ticket.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
	selector: 'app-ticket-form',
	templateUrl: './ticket-form.component.html'
})
export class TicketFormComponent implements OnInit, OnDestroy, OnChanges {
	@Input() ticket: Ticket;
	@Output() ticketEvent = new EventEmitter<Ticket>();
	isAddMode: boolean;
	isLoading: boolean = true;
	form: FormGroup;
	modulos: TicketModulo[] = [];
	status: TicketStatus[] = [];
	classificacoes: TicketClassificacao[] = [];
	prioridades: TicketPrioridade[] = [];
	userSession: UsuarioSessao;
	titulo: String;
	descricao: String;
	protected _onDestroy = new Subject<void>();

	constructor(
		private _formBuilder: FormBuilder,
		private _ticketService: TicketService,
		private _ticketModuloService: TicketModuloService,
		private _ticketStatusService: TicketStatusService,
		private _ticketAtendimentoService: TicketAtendimentoService,
		private _ticketPrioridadeService: TicketPrioridadeService,
		private _ticketClassificacaoService: TicketClassificacaoService,
		private _snack: CustomSnackbarService,
		public _location: Location,
		private _userService: UserService,
		private _router: Router
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.isAddMode = !this.ticket;
		this.inicializarForm();
		this.obterDados();
	}

	ngOnChanges(changes: SimpleChanges) {
		this.ticket = changes.ticket.currentValue;
		if (this.ticket && this.form) this.form.patchValue(this.ticket);
	}

	private inicializarForm() {
		this.form = this._formBuilder.group({
			codStatus: [
				{
					value: this.isAddMode ? ticketStatusConst.AGUARDANDO : undefined,
					disabled: this.isAddMode
				},
			],
			codModulo: [undefined, Validators.required],
			codClassificacao: [undefined, [Validators.required]],
			codPrioridade: [undefined, Validators.required],
			titulo: [undefined, Validators.required],
			descricao: [undefined, Validators.required],
		});
	}

	private async obterDados() {
		this.modulos = (await this._ticketModuloService
			.obterPorParametros({ sortActive: 'descricao', sortDirection: 'asc' })
			.toPromise()).items;

		this.status = (await this._ticketStatusService
			.obterPorParametros({ sortActive: 'descricao', sortDirection: 'asc' })
			.toPromise()).items;

		this.classificacoes = (await this._ticketClassificacaoService
			.obterPorParametros({ sortActive: 'descricao', sortDirection: 'asc' })
			.toPromise()).items;

		this.prioridades = (await this._ticketPrioridadeService
			.obterPorParametros({ sortActive: 'descricao', sortDirection: 'asc' })
			.toPromise()).items;

		if (!this.isAddMode)
		{
			this.form.patchValue(this.ticket);
		}

		this.isLoading = false;
	}

	salvar(): void {
		this.isAddMode ? this.criar() : this.atualizar();
	}

	private criar(): void {
		let obj = {
			...this.ticket,
			...this.form.getRawValue(),
			...{
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioCad: this.userSession.usuario.codUsuario,
				ordem: 9999
			}
		};

		this._ticketService.criar(obj).subscribe(async (t) => {
			this.ticketEvent.emit(t);

			const primeiroAtendimento: TicketAtendimento = {
				codStatus: +ticketStatusConst.AGUARDANDO,
				codUsuarioCad: this.userSession.usuario.codUsuario,
				dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
				descricao: 'Ticket Aberto',
				codTicket: t.codTicket
			};

			await this._ticketAtendimentoService.criar(primeiroAtendimento).subscribe(() => {
				this._snack.exibirToast(`Ticket ${t?.codTicket} inserido com sucesso!`, "success");
				this._router.navigate(['ticket/detalhe/' + t.codTicket]);
			});
		});
	}

	private atualizar(): void {
		let obj = {
			...this.ticket,
			...this.form.getRawValue(),
			...{
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario.codUsuario
			}
		};

		this._ticketService.atualizar(obj).subscribe((t) => {
			this.ticketEvent.emit(t);
			this._snack.exibirToast(`Ticket ${t?.codTicket} atualizado com sucesso!`, "success");
		});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
