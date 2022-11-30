import { Ticket, TicketAtendimento, TicketClassificacao, TicketModulo, TicketPrioridade, TicketStatus, ticketStatusConst } from 'app/core/types/ticket.types';
import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges, ViewEncapsulation } from '@angular/core';
import { debounceTime, delay, filter, takeUntil, tap } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { TicketAtendimentoService } from 'app/core/services/ticket-atendimento.service';
import { TicketClassificacaoService } from 'app/core/services/ticket-classificacao.service';
import { TicketModuloService } from 'app/core/services/ticket-modulo.service';
import { TicketPrioridadeService } from 'app/core/services/ticket-prioridade.service';
import { TicketStatusService } from 'app/core/services/ticket-status.service';
import { TicketService } from 'app/core/services/ticket.service';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Subject } from 'rxjs';
import moment from 'moment';
import _ from 'lodash';

@Component({
	selector: 'app-ticket-form',
	templateUrl: './ticket-form.component.html',
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class TicketFormComponent implements OnInit, OnDestroy, OnChanges {
	@Input() ticket: Ticket;
	@Output() ticketEvent = new EventEmitter<Ticket>();
	tickets: Ticket[] = [];
	ticketsFiltrados: Ticket[] = [];
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
	termos: any[] = [];
	quillModules: any = {
		toolbar: [
			['bold', 'italic', 'underline', 'strike'],
			['blockquote', 'code-block'],
			[{ 'header': 1 }, { 'header': 2 }],
			[{ 'list': 'ordered'}, { 'list': 'bullet' }],
			[{ 'script': 'sub'}, { 'script': 'super' }],
			[{ 'indent': '-1'}, { 'indent': '+1' }],
			[{ 'size': ['small', false, 'large', 'huge'] }],
			[{ 'align': [] }],
			['clean'],
			['link', 'image'] 
		]
	};
	orientacoes: string[] = [
		`Ao se referir a um recurso do sistema, tente colar a url de acesso para que possamos entender onde você estava no momento do problema`,
		`Informe o módulo durante a criação da sua solicitação, assim saberemos em qual parte do sistema você está enfrentando problemas`,
		`Você pode anexar imagens na descrição do ticket, formatar o texto e destacar os trechos que entende serem importantes`,
		`Se precisar de auxílio sobre um chamado, não esqueça de mencionar o número para que possamos localizá-lo rapidamente`,
		`Quando se referir a relatórios ou reportings, tente colar o link do mesmo para que possamos localizar rapidamente`,
		`É muito comum esquecermos de algo durante a criação do ticket. No entanto, você pode editá-lo a qualquer momento`,
		`É possível acompanhar o trabalho da nossa equipe clicando no link "Gráficos" na parte superior da página`,
		`Você pode anexar arquivos no seu ticket. Após salvá-lo, o sistema habilita algumas opções adicionais`,
		`Você pode acompanhar a sua solicitação ao clicar no link "Lista" na parte superior desta página`,
		`Você receberá uma notificação sempre que seu ticket for atualizado pela nossa equipe`,
		`Tente fornecer o máximo de informações sobre o problema. Se possível, anexe imagens`,
		`A classificação do ticket é importante para identficarmos o tipo da solicitação`,
	];
	orientacao: string;
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
		private _userService: UserService,
		private _router: Router,
		public _location: Location
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	async ngOnInit() {
		this.isAddMode = !this.ticket;
		this.inicializarForm();
		await this.obterDados();
		await this.obterTickets();
		this.obterTermos();
		this.orientacao = this.orientacoes[Math.floor(Math.random() * this.orientacoes.length)];
		this.registrarEmmiters();
		this.isLoading = false;
	}

	ngOnChanges(changes: SimpleChanges) {
		this.ticket = changes.ticket.currentValue;
		
		if (this.ticket && this.form) this.form.patchValue(this.ticket);
	}

	private inicializarForm() {
		this.form = this._formBuilder.group({
			codStatus: [{
				value: this.isAddMode ? ticketStatusConst.AGUARDANDO : undefined,
				disabled: this.isAddMode
			},],
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

	private async obterTickets() {
		const data = await this._ticketService
			.obterPorParametros({
				sortActive: 'CodTicket',
				sortDirection: 'desc',
				codStatus: ticketStatusConst.CONCLUIDO
			})
			.toPromise();

		this.tickets = data.items;
	}

	private obterTermos() {
		this.tickets.forEach(ticket => {
			const palavrasTitulo = ticket.titulo.split(' ');
			const palavrasDescricao = ticket.descricao.split(' ');
			
			palavrasTitulo.filter(p => p.length > 3).forEach(palavra => {
				this.termos.push({
					codTicket: ticket.codTicket,
					termo: palavra
				})
			});

			palavrasDescricao.filter(p => p.length > 3).forEach(palavra => {
				this.termos.push({
					codTicket: ticket.codTicket,
					termo: palavra
				})
			});
		});
	}

	registrarEmmiters() {
		this.form.controls['titulo'].valueChanges.pipe(
		  filter(v => !!v),
		  tap(() => { }),
		  debounceTime(700),
		  delay(500),
		  takeUntil(this._onDestroy)
		).subscribe((text) => {
			this.filtrarTickets(text);
		});
	}

	filtrarTickets(filtro: string) {
		this.ticketsFiltrados = [];
		let codTickets: number[] = [];
		const termosPesquisados = filtro.split(' ');

		termosPesquisados.forEach(p => {
			this.termos.forEach(t => {
				if (p.toLowerCase() == t.termo.toLowerCase()) {
					codTickets.push(t.codTicket);
				}
			});
		});

		codTickets = _.uniq(codTickets);
		codTickets.forEach(cod => {
			this.ticketsFiltrados.push(_.find(this.tickets, { codTicket: cod }));
		});
	}

	salvar(): void {
		this.isAddMode ? this.criar() : this.atualizar();
	}

	private criar(): void {
		this.isLoading = true;

		let obj = {
			...this.ticket,
			...this.form.getRawValue(),
			...{
				indAtivo: statusConst.ATIVO,
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
				this.isLoading = false;
			}, () => { 
				this.isLoading = false;
			});
		});
	}

	private atualizar(): void {
		this.isLoading = true;

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
			this.isLoading = false;
		}, () => { 
			this.isLoading = false;
		});
	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
