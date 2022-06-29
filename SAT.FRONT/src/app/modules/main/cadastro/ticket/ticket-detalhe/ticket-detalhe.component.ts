import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { MatSidenav } from '@angular/material/sidenav';
import { StatusServico } from 'app/core/types/status-servico.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { OrdemServicoHistorico } from 'app/core/types/ordem-servico-historico.types';
import { fuseAnimations } from '@fuse/animations';
import { DispBBBloqueioOS } from 'app/core/types/DispBBBloqueioOS.types';
import { TicketService } from 'app/core/services/ticket.service';

@Component({
	selector: 'app-ticket-detalhe',
	templateUrl: './ticket-detalhe.component.html',
	animations: fuseAnimations,
	encapsulation: ViewEncapsulation.None,
})
export class TicketDetalheComponent implements AfterViewInit {
	@ViewChild('sidenav') sidenav: MatSidenav;
	codTicket: number;
	os: OrdemServico;
	statusServico: StatusServico;
	perfis: any;
	userSession: UsuarioSessao;
	qtdFotos: number = 0;
	qtdLaudos: number = 0;
	ultimoAgendamento: string;
	histAgendamento: string = 'Agendamentos: \n';
	isLoading: boolean = false;
	historico: OrdemServicoHistorico[] = [];
	dispBBBloqueioOS: DispBBBloqueioOS[] = [];

	constructor(
		private _route: ActivatedRoute,
		private _userService: UserService,
		private _ticketService: TicketService,
		private _cdr: ChangeDetectorRef	) {
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
			
		this._ticketService.obterPorCodigo(this.codTicket);

		this.isLoading = false;
	}
}