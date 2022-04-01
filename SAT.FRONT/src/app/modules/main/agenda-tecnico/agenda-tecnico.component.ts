import { AfterViewInit, Component, LOCALE_ID, ViewChild, ViewEncapsulation } from '@angular/core';
import { setOptions, localePtBR, MbscEventcalendarOptions, formatDate, MbscPopup, MbscPopupOptions, Notifications } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { AgendaTecnico, AgendaTecnicoParameters, AgendaTecnicoTipoEnum, MbscAgendaTecnicoCalendarEvent, ViewAgendaTecnicoEvento, ViewAgendaTecnicoRecurso } from 'app/core/types/agenda-tecnico.types';
import moment from 'moment';
import Enumerable from 'linq';
import { interval, Subject } from 'rxjs';
import { MatSidenav } from '@angular/material/sidenav';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { MatDialog } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { AgendaTecnicoValidator } from './agenda-tecnico.validator';
import { fuseAnimations } from '@fuse/animations';
import localePt from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import _ from 'lodash';
import { takeUntil } from 'rxjs/operators';
registerLocaleData(localePt, 'pt');

setOptions({
  locale: localePtBR,
  theme: 'ios',
  themeVariant: 'light',
  clickToCreate: true,
  dragToCreate: true,
  dragToMove: true,
  dragToResize: true,
});

@Component({
  selector: 'app-agenda-tecnico',
  templateUrl: './agenda-tecnico.component.html',
  styleUrls: ['./agenda-tecnico.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class AgendaTecnicoComponent extends Filterable implements AfterViewInit, IFilterable
{
  userSession: UserSession;
  events: MbscAgendaTecnicoCalendarEvent[] = [];
  agendaTecnicos: ViewAgendaTecnicoEvento[] = [];
  recursos: ViewAgendaTecnicoRecurso[] = [];
  resources: any = [];
  inicio: string;
  fim: string;
  @ViewChild('popup', { static: false })
  tooltip!: MbscPopup;
  currentEvent: any;
  intervencao = '';
  info = '';
  time = '';
  status = '';
  dataHoraLimiteAtendimento = '';
  selectResource: any;
  anchor: HTMLElement | undefined;
  timer: any;
  popupOptions: MbscPopupOptions;
  calendarOptions: MbscEventcalendarOptions;
  loading: boolean;

  @ViewChild('sidenavChamados') sidenavChamados: MatSidenav;
  @ViewChild('sidenav') sidenavAgenda: MatSidenav;
  @ViewChild('sidenavAjuda') sidenavAjuda: MatSidenav;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _notify: Notifications,
    private _agendaTecnicoSvc: AgendaTecnicoService,
    protected _userSvc: UserService,
    public _dialog: MatDialog,
    private _validator: AgendaTecnicoValidator,
    private _snack: CustomSnackbarService,
    private _ordemServicoSvc: OrdemServicoService
  )
  {
    super(_userSvc, 'agenda-tecnico');
  }

  ngAfterViewInit(): void
  {
    this.carregarOpcoesCalendario();
    this.carregarOpcoesPopup();
    this.registerEmitters();
  }

  carregarOpcoesPopup()
  {
    this.popupOptions = {
      display: 'anchored',
      touchUi: false,
      showOverlay: false,
      contentPadding: false,
      closeOnOverlayClick: false,
      width: 350
    }
  }

  carregarOpcoesCalendario()
  {
    this.calendarOptions = {
      view: {
        timeline: {
          type: 'week',
          startDay: moment().day(),
          endDay: moment().day() + 6,
          size: 1,
          allDay: true,
          rowHeight: 'equal',
          startTime: '07:00',
          endTime: '20:00',
        },
      },
      dragToMove: true,
      externalDrop: true,
      dragToResize: false,
      dragToCreate: false,
      clickToCreate: false,
      showEventTooltip: false,
      onEventCreate: (args, inst) =>
      {
        if (this._validator.hasOverlap(args, inst))
        {
          this._snack.exibirToast("Os atendimentos não podem se sobrepor.", "error");
          return false;
        }
        else if (this._validator.invalidInsert(args))
        {
          this._snack.exibirToast("O atendimento não pode ser agendado para antes da linha do tempo.", "error");
          return false;
        }
  
        this.sidenavChamados.close();
        this.criarAgendaETranferirChamado(args.event);
      },
      onEventUpdate: (args, inst) =>
      {
        if (this._validator.hasOverlap(args, inst))
        {
          this._snack.exibirToast("Os atendimentos não podem se sobrepor.", "error");
          return false;
        }
        else if (this._validator.cantChangeInterval(args))
        {
          this._snack.exibirToast("Não é possível transferir um intervalo.", "error");
          return false;
        }
        else if (this._validator.invalidMove(args))
        {
          this._snack.exibirToast("O atendimento não pode ser agendado para antes da linha do tempo.", "error");
          return false;
        }
        else
        {
          this.atualizarAgenda(args.event);
          return true;
        }
      },
      onCellDoubleClick: (args, inst) =>
      {
        
      },
      onEventHoverIn: (args, inst) =>
      {
        this.mostrarInformacoesEvento(args, inst);
      },
      onEventHoverOut: () =>
      {
        this.esconderInformacoesEvento();
      },
      onPageLoading: (event: any) => {
        this.inicio =  moment(event.firstDay).format('yyyy-MM-DD HH:mm:ss');
        this.fim = moment(event.lastDay).format('yyyy-MM-DD HH:mm:ss');

        this.obterDados();
      }
    }
  }

  registerEmitters(): void
  {
    interval(5 * 60 * 1000)
			.pipe(
				takeUntil(this._onDestroy)
			)
			.subscribe(() => {
				this.obterDados();
			});


    this.sidenavAgenda.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.obterDados();
    });
  }

  loadFilter(): void
  {
    super.loadFilter();
  }

  public async obterDados()
  {
    this.loading = true;

    const agendaTecnicoParams: AgendaTecnicoParameters = {
      ...{
        inicio: this.inicio,
        fim: this.fim,
        sortActive: 'nome',
        sortDirection: 'asc'
      },
      ...this.filter?.parametros,
      ...{ codFilial: this.filter?.parametros?.codFilial || this.userSession.usuario?.codFilial }
    };

    this._agendaTecnicoSvc.obterPorParametros(agendaTecnicoParams).toPromise().then(recursos => {
      this.limparListas();
      this.recursos = recursos;
      this.agendaTecnicos = _.flatMapDeep(recursos, (r) => r.eventos);
      this.events = this.events.concat(Enumerable.from(this.agendaTecnicos).select(agenda =>
      {
        return {
          codAgendaTecnico: agenda.codAgendaTecnico,
          codOS: agenda.codOS,
          agendaTecnico: agenda,
          start: moment(agenda.inicio),
          end: moment(agenda.fim),
          title: agenda.titulo,
          color: agenda.cor,
          editable: agenda.editavel,
          resource: agenda.codTecnico,
        }
      }).toArray());

      this.resources = Enumerable.from(this.recursos).select(r =>
      {
        return {
          id: r.id,
          name: r.nome,
          indFerias: false,
          contato: r.fonePerto,
          qtdChamadosTransferidos: r.qtdChamadosTransferidos,
          qtdChamadosAtendidos: r.qtdChamadosAtendidos,
          //descricao: r.clientes,
          img: `https://sat.perto.com.br/DiretorioE/AppTecnicos/Fotos/${r.codUsuario}.jpg`,
        }
      }).toArray();

      this.loading = false;
    });
  }

  private async atualizarAgenda(ev)
  {
    this.loading = true;

    var agenda: AgendaTecnico = ev.agendaTecnico;
    agenda.codTecnico = ev.resource;
    agenda.inicio = moment(ev.start).format('YYYY-MM-DD HH:mm:ss');
    agenda.fim = moment(ev.end).format('YYYY-MM-DD HH:mm:ss');
    agenda.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss');
    agenda.codUsuarioManut = this.userSession.usuario.codUsuario;

    var event = Enumerable.from(this.events).firstOrDefault(e => e.codAgendaTecnico == agenda.codAgendaTecnico);
    if (agenda.tipo == AgendaTecnicoTipoEnum.OS && moment(ev.end) > moment())
    {
      agenda.cor = this._validator.getTypeColor(AgendaTecnicoTipoEnum.OS);
      event.color = agenda.cor;
    }

    var ag = await this._agendaTecnicoSvc.atualizar(agenda).toPromise();

    if (ag != null)
    {
      this._snack.exibirToast("Evento atualizado com sucesso", "success");
      this.loading = false;
    }
    else
    {
      this._snack.exibirToast("Não foi possível atualizar o evento", "error");
    }
  }

  private async criarAgendaETranferirChamado(ev)
  {
    this.loading = true;

    var agendaTecnico: AgendaTecnico =
    {
      inicio: moment(ev.start).format('yyyy-MM-DD HH:mm:ss'),
      fim: moment(ev.end).format('yyyy-MM-DD HH:mm:ss'),
      codOS: ev.ordemServico.codOS,
      codTecnico: ev.resource,
      titulo: ev.ordemServico.localAtendimento.nomeLocal.toUpperCase(),
      tipo: AgendaTecnicoTipoEnum.OS,
      indAgendamento: ev.indAgendamento,
      indAtivo: 1,
      codUsuarioCad: this.userSession.usuario.codUsuario,
      dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss')
    };

    if (ev.indAgendamento == 1)
    {
      agendaTecnico.inicio = moment(ev.dataAgendamento).format('yyyy-MM-DD HH:mm:ss');
      agendaTecnico.fim = moment(ev.dataAgendamento).add(1, 'hour').format('yyyy-MM-DD HH:mm:ss');
      agendaTecnico.cor = this._validator.agendamentoColor();
      agendaTecnico.indAgendamento = 1;
    }

    const os = await this._ordemServicoSvc.obterPorCodigo(agendaTecnico.codOS).toPromise();
    os.dataHoraManut = moment().format('yyyy-MM-DD HH:mm:ss');
    os.codTecnico = agendaTecnico.codTecnico;
    os.codUsuarioManut = this.userSession.usuario.codUsuario,
    os.codStatusServico = StatusServicoEnum.TRANSFERIDO;
    await this._ordemServicoSvc.atualizar(os).toPromise();

    await this._agendaTecnicoSvc.criar(agendaTecnico).toPromise().then(async () => {
      this._notify.toast({
        message: 'Registro incluído com sucesso'
      });

      this.obterDados();
      this.loading = false;
    });
  }

  public mostrarAjuda()
  {
    this.sidenavAjuda.open();
  }

  public esconderInformacoesEvento()
  {
    if (!this.timer)
    {
      this.timer = setTimeout(() =>
      {
        this.tooltip.close();
      }, 200);
    }
  }

  private mostrarInformacoesEvento(args, inst)
  {    
    const event: any = args.event;
    const agenda: ViewAgendaTecnicoEvento = event.agendaTecnico;
    this.info = `${agenda.titulo} ${agenda.codOS || ''}`;
    const time = (event.agendaTecnico?.tipo == AgendaTecnicoTipoEnum.PONTO || 
      AgendaTecnicoTipoEnum.FIM_EXPEDIENTE) ? formatDate('HH:mm', new Date(event.start)) : 
      formatDate('HH:mm', new Date(event.start)) + ' - ' + formatDate('HH:mm', new Date(event.end));
    this.currentEvent = event;
    this.time = time;
    this.status = agenda.nomeStatusServico;
    this.intervencao = agenda.nomTipoIntervencao;
    clearTimeout(this.timer);
    this.timer = null;
    this.selectResource = null;
    this.anchor = args.domEvent.target;
    this.tooltip.open();
  }

  public mostrarAcoesRecurso(resource)
  {
    if (resource.indFerias) return;

    var target = (window.event as any).target;
    this.selectResource = resource;
    this.info = resource.name;
    clearTimeout(this.timer);
    this.anchor = target;

    this.intervencao = null;
    this.time = null;
    this.status = null;
    this.currentEvent = null;
    this.timer = null;

    this.tooltip.open();
  }

  private limparListas() {
    this.recursos = [];
    this.resources = [];
    this.events = [];
  }

  public mouseEnter(): void
  {
    if (this.timer)
    {
      clearTimeout(this.timer);
      this.timer = null;
    }
  }

  public mouseLeave(): void
  {
    this.timer = setTimeout(() =>
    {
      this.tooltip.close();
    }, 200);
  }
}