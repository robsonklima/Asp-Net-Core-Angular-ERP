import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatSidenav } from '@angular/material/sidenav';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { fuseAnimations } from '@fuse/animations';
import { TicketService } from 'app/core/services/ticket.service';
import { Ticket, TicketAtendimento, TicketAtendimentoData, TicketAtendimentoParameters } from 'app/core/types/ticket.types';
import moment from 'moment';
import { TicketAtendimentoService } from 'app/core/services/ticket-atendimento.service';

@Component({
	selector: 'app-ticket-detalhe',
	templateUrl: './ticket-detalhe.component.html',
	animations: fuseAnimations,
	encapsulation: ViewEncapsulation.None,
})
export class TicketDetalheComponent implements AfterViewInit {
	@ViewChild('sidenav') sidenav: MatSidenav;
	codTicket: number;
	ticket: Ticket;
	userSession: UsuarioSessao;
	isLoading: boolean = false;
	ticketAtendimento: TicketAtendimentoData;
	statusAguardando: number = 4;

	constructor(
		private _route: ActivatedRoute,
		private _userService: UserService,
		private _ticketService: TicketService,
		private _ticketAtendimentoService: TicketAtendimentoService,
		private _cdr: ChangeDetectorRef	
		) 
	{
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngAfterViewInit(): void {
		this.codTicket = +this._route.snapshot.paramMap.get('codTicket');

		if (this.codTicket) {
			this.obterDados();
		}

		this._cdr.detectChanges();

	}

	private async obterDados() {
		this.isLoading = true;
			
		this.ticket = await this._ticketService.obterPorCodigo(this.codTicket).toPromise();

		this.ticketAtendimento = await this._ticketAtendimentoService.obterPorParametros({codTicket: this.codTicket	}).toPromise();

		console.log(this.ticketAtendimento);

		this.isLoading = false;
	}

	async iniciarAtendimento(){
		let tick: Ticket = {
			codTicket: this.codTicket,		
			codStatus: 1,			
			usuarioManut: this.userSession?.usuario?.codUsuario,
			dataManut: moment().toDate()
		}	

		await this._ticketService.atualizar(
			{
				...this.ticket,
				...tick
			}

			).subscribe(r => {
			
		});
		
	}
}