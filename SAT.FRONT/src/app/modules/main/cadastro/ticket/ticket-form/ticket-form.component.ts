import { Location } from '@angular/common';
import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { TicketModuloService } from 'app/core/services/ticket-modulo.service';
import { TicketStatusService } from 'app/core/services/ticket-status.service';
import { TicketService } from 'app/core/services/ticket.service';
import { Ticket, TicketClassificacao, TicketModulo, TicketModuloParameters, TicketPrioridade, TicketStatus } from 'app/core/types/ticket.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-ticket-form',
  templateUrl: './ticket-form.component.html'
  
})
export class TicketFormComponent implements OnInit, OnDestroy {
  codTicket: number;
  codModulo: number;
  codStatus: number;
//  codClassificacao: number;
//  codPrioridade: number;
  isAddMode: boolean;
  isLoading: boolean = false;
  form: FormGroup;
  tickets: Ticket;
  modulos: TicketModulo;
  status: TicketStatus;
  classificacoes: TicketClassificacao[] = [];
  prioridades: TicketPrioridade[] = [];
  userSession: UsuarioSessao;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _ticketService: TicketService,
    private _ticketModuloService: TicketModuloService,
    private _ticketStatusService: TicketStatusService,
  //  private _ticketPrioridadeService: TicketPrioridadeService,
  //  private _ticketClassificacaoService: TicketClassificacaoService,
    private _route: ActivatedRoute,
    private _snack: CustomSnackbarService,
    public _location: Location,
    private _userService: UserService,
    private _cdr: ChangeDetectorRef	
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codTicket = +this._route.snapshot.paramMap.get('codTicket');
    this.isAddMode = !this.codTicket;
    
    this.inicializarForm();
    this.obterDados();

  }  
  private inicializarForm() {
    this.form = this._formBuilder.group({
      codTicket: [
        {
          value: undefined,
          disabled: true
        },
      ],
      modulos: [undefined, Validators.required],
      classificacoes: [undefined, [Validators.required]],
      status: [undefined, Validators.required],
      prioridades: [undefined, Validators.required],
      titulo: [undefined],
      descricao: [undefined],
      
    });
  }
	private async obterDados() {
		this.isLoading = true;
			
		this.tickets = await this._ticketService.obterPorCodigo(this.codTicket).toPromise();
    this.modulos = await this._ticketModuloService.obterPorParametros(this.modulos).toPromise();
//    this.modulos = await this._ticketModuloService.obterPorParametros(this.codModulo).toPromise();
    this.status = await this._ticketStatusService.obterPorParametros(this.status).toPromise();
//    this.status = await this._ticketStatusService.obterPorParametros(this.codStatus).toPromise();    

  //  this.prioridades = await this._ticketPrioridadeService.obterPorCodigo(this.codPrioridade).toPromise();
  //  this.classificacoes = await this._ticketClassificacaoService.obterPorCodigo(this.codClassificacao).toPromise();

		this.isLoading = false;
	}




  salvar(): void {
    this.criar();
  }

    criar(): void {
    const form = this.form.getRawValue();

    let obj = {
      ...this.tickets,
      ...form,
      ...{
        datacadastro: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuario: this.userSession.usuario.codUsuario        
      }
    };

    this._ticketService.criar(obj).subscribe(() => {
      this._snack.exibirToast(`Ticket ${obj.numSerie} inserido com sucesso!`, "success");
      this._location.back();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
