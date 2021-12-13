import { AfterViewInit, Component, LOCALE_ID, ViewChild, ViewEncapsulation } from '@angular/core';
import { setOptions, localePtBR, Notifications, MbscEventcalendarOptions, formatDate, MbscPopup, MbscPopupOptions } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { AgendaTecnico, AgendaTecnicoTypeEnum, MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import moment from 'moment';
import Enumerable from 'linq';
import { Subject } from 'rxjs';
import { MatSidenav } from '@angular/material/sidenav';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { MatDialog } from '@angular/material/dialog';
import { RoteiroMapaComponent } from './roteiro-mapa/roteiro-mapa.component';
import { UserService } from 'app/core/user/user.service';
import { RoleEnum, UserSession } from 'app/core/user/user.types';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { AgendaTecnicoRealocacaoDialogComponent } from './agenda-tecnico-realocacao-dialog/agenda-tecnico-realocacao-dialog.component';
import { AgendaTecnicoValidator } from './agenda-tecnico.validator';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { fuseAnimations } from '@fuse/animations';
import localePt from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';

registerLocaleData(localePt, 'pt');

setOptions({
  locale: localePtBR,
  theme: 'ios',
  themeVariant: 'light',
  clickToCreate: true,
  dragToCreate: true,
  dragToMove: true,
  dragToResize: true
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
  loading: boolean;
  userSession: UserSession;
  tecnicos: Tecnico[] = [];
  events: MbscAgendaTecnicoCalendarEvent[] = [];
  agendaTecnicos: AgendaTecnico[] = [];
  resources = [];
  weekStart = moment().clone().startOf('isoWeek').format('yyyy-MM-DD HH:mm:ss');
  weekEnd = moment().clone().startOf('isoWeek').add(7, 'days').format('yyyy-MM-DD HH:mm:ss');
  snackConfigInfo: MatSnackBarConfig = { duration: 2000, panelClass: 'info' };

  lastFilialFilter: any;
  lastTecnicosFilter: any;
  lastPAFilter: any;

  @ViewChild('popup', { static: false })
  tooltip!: MbscPopup;

  popupOptions: MbscPopupOptions = {
    display: 'anchored',
    touchUi: false,
    showOverlay: false,
    contentPadding: false,
    closeOnOverlayClick: false,
    width: 350
  };

  currentEvent: any;
  interventionType = '';
  info = '';
  time = '';
  anchor: HTMLElement | undefined;
  timer: any;

  calendarOptions: MbscEventcalendarOptions = {
    view: {
      timeline: {
        type: 'week',
        allDay: false,
        startDay: 1,
        startTime: '07:00',
        endTime: '24:00',
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
        this._notify.toast({
          message: 'Os atendimentos não podem se sobrepor.',
          color: 'danger'
        });
        return false;
      }
      else if (this._validator.invalidInsert(args))
      {
        this._notify.toast({
          message: 'O atendimento não pode ser agendado para antes da linha do tempo.',
          color: 'danger'
        });
        return false;
      }

      this.validateNewEvent(args, inst);
    },
    onEventUpdate: (args, inst) =>
    {
      if (this._validator.hasOverlap(args, inst))
      {
        this._notify.toast({
          message: 'Os atendimentos não podem se sobrepor.',
          color: 'danger'
        });
        return false;
      }
      else if (this._validator.hasChangedResource(args))
      {
        if (this._validator.isTechnicianInterval(args.event))
        {
          this._notify.toast({
            message: 'Não é possível transferir um intervalo.',
            color: 'danger'
          });
          return false;
        }
        else return this.updateResourceChange(args.event);
      }
      else if (this._validator.invalidMove(args))
      {
        this._notify.toast({
          message: 'O atendimento não pode ser agendado para antes da linha do tempo.',
          color: 'danger'
        });
        return false;
      }
      return this.updateEvent(args);
    },
    onCellDoubleClick: (args, inst) =>
    {
      this.realocarAgendamento(args);
    },
    onSelectedDateChange: (args, inst) =>
    {
      this.changeWeek(args, inst)
    },
    onEventHoverIn: (args, inst) =>
    {
      this.showOSInfo(args, inst);
    },
    onEventHoverOut: () =>
    {
      if (!this.timer)
      {
        this.timer = setTimeout(() =>
        {
          this.tooltip.close();
        }, 200);
      }
    },
    onEventClick: () =>
    {
      this.tooltip.open();
    }
  };

  @ViewChild('sidenavChamados') sidenavChamados: MatSidenav;
  @ViewChild('sidenav') sidenavAgenda: MatSidenav;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _notify: Notifications,
    private _tecnicoSvc: TecnicoService,
    private _osSvc: OrdemServicoService,
    private _agendaTecnicoSvc: AgendaTecnicoService,
    protected _userSvc: UserService,
    public _dialog: MatDialog,
    private _validator: AgendaTecnicoValidator,
    private _snack: MatSnackBar
  )
  {
    super(_userSvc, 'agenda-tecnico')
    this.obterDados();
  }

  registerEmitters(): void
  {
    this.sidenavAgenda.openedStart.subscribe(() =>
    {
      this.lastFilialFilter = this.filter?.parametros?.codFiliais;
      this.lastTecnicosFilter = this.filter?.parametros?.codTecnicos;
      this.lastPAFilter = this.filter?.parametros?.pas;
    });

    this.sidenavAgenda.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();

      // só aplica o filtro se este de fato mudou. TODO -> //valueChanged
      if (this.lastFilialFilter != this.filter?.parametros?.codFiliais ||
        this.lastTecnicosFilter != this.filter?.parametros?.codTecnicos ||
        this.lastPAFilter != this.filter?.parametros?.pas)
      {
        this.obterDados();
      }
    });
  }

  ngAfterViewInit(): void 
  {
    this.registerEmitters();
  }

  private async obterDados(showLoading: boolean = true)
  {
    this.loading = showLoading;

    this.tecnicos = (await this._tecnicoSvc.obterPorParametros({
      indAtivo: 1,
      codPerfil: RoleEnum.FILIAL_TECNICO_DE_CAMPO,
      codFiliais: this.filter?.parametros?.codFiliais,
      pas: this.filter?.parametros?.pas,
      sortActive: 'nome',
      sortDirection: 'asc',
      codTecnicos: this.filter?.parametros?.codTecnicos
    }).toPromise()).items;


    var codTecnicos =
      Enumerable.from(this.tecnicos).select(i => i.codTecnico).toJoinedString(",");

    this.agendaTecnicos = (await this._agendaTecnicoSvc.obterAgendaTecnico({
      codFiliais: this.filter?.parametros?.codFiliais,
      codTecnicos: codTecnicos,
      inicioPeriodoAgenda: this.weekStart,
      fimPeriodoAgenda: this.weekEnd,
      sortActive: 'nome',
      sortDirection: 'asc',
    }).toPromise());

    await this.carregaAgenda();
    this.exibePontosDoDia();

    this.loading = false;
  }

  carregaAgenda()
  {
    this.resources = this.tecnicos.map(tecnico =>
    {
      return {
        id: tecnico.codTecnico,
        name: tecnico.nome.toUpperCase(),
        indFerias: this._validator.isOnVacation(tecnico),
        img: `https://sat.perto.com.br/DiretorioE/AppTecnicos/Fotos/${tecnico.usuario.codUsuario}.jpg`,
      }
    });

    this.events = [];
    this.agendaTecnicos.forEach(ag =>
    {
      this.events = this.events.concat(
        {
          codAgendaTecnico: ag.codAgendaTecnico,
          codOS: ag.codOS,
          agendaTecnico: ag,
          start: moment(ag.inicio),
          end: moment(ag.fim),
          title: ag.titulo,
          color: ag.cor,
          editable: ag.indAgendamento == 0 ? true : false,
          resource: ag.codTecnico,
          ordemServico: ag.ordemServico
        });
    });
  }

  private async checkForWarnings(ev, args, inst)
  {
    var isFromSameRegion = (await this._validator.isTecnicoDaRegiaoDoChamado(ev.ordemServico, ev.resource));
    var isAgendado = ev.indAgendamento == 1;

    if (!isFromSameRegion)
    {
      var message: string = `Você transferiu o chamado ${ev.ordemServico.codOS} para um técnico com a região diferente do chamado.`;
      await this._snack.open(message, null, this.snackConfigInfo).afterDismissed().toPromise();
    }

    if (isAgendado)
    {
      var message: string = `Você transferiu um chamado com agendamento marcado. Ele será realocado automaticamente.`;
      await this._snack.open(message, null, this.snackConfigInfo).afterDismissed().toPromise();
    }

    this.createExternalEvent(ev, args, inst);
  }

  /**  */

  /** Mobiscroll */

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

  private async changeWeek(args, inst)
  {
    this.weekStart = moment(args.date).format('yyyy-MM-DD HH:mm:ss');
    this.weekEnd = moment(args.date).add(7, 'days').format('yyyy-MM-DD HH:mm:ss');
    await this.obterDados(false);
  }

  private async validateNewEvent(args, inst)
  {
    var ev = args.event;
    this.sidenavChamados.close();
    this.checkForWarnings(ev, args, inst);
  }

  private async createExternalEvent(ev, args, inst)
  {
    var agendaTecnico: AgendaTecnico =
    {
      inicio: moment(ev.start).format('yyyy-MM-DD HH:mm:ss'),
      fim: moment(ev.end).format('yyyy-MM-DD HH:mm:ss'),
      codOS: ev.ordemServico.codOS,
      codTecnico: ev.resource,
      titulo: ev.ordemServico.localAtendimento.nomeLocal.toUpperCase(),
      cor: this._validator.getTypeColor(AgendaTecnicoTypeEnum.OS),
      tipo: AgendaTecnicoTypeEnum.OS,
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

    var ag = (await this._agendaTecnicoSvc.criar(agendaTecnico).toPromise());

    if (ag != null)
    {
      if (ag.indAgendamento == 1) this.obterDados(false);

      var os = ev.ordemServico;

      os.codTecnico = ag.codTecnico;
      os.dataHoraTransf = ag.dataHoraManut;
      os.codUsuarioManut = ag.codUsuarioCad;
      os.codStatusServico = StatusServicoEnum.TRANSFERIDO;
      os.statusServico.codStatusServico = StatusServicoEnum.TRANSFERIDO;
      ev.codAgendaTecnico = ag.codAgendaTecnico;

      var updatedOS = (await this._osSvc.atualizar(os).toPromise());

      if (updatedOS)
      {
        this._notify.toast({
          message: 'Atendimento agendado com sucesso.',
          color: 'success'
        });
        return true;
      }
      else
      {
        this._notify.toast({
          message: 'Não foi possível fazer o agendamento.',
          color: 'danger'
        });
        this.deleteEvent(args, inst);
        return false;
      }
    }
    else
    {
      this._notify.toast({
        message: 'Não foi possível fazer o agendamento.',
        color: 'danger'
      });
      this.deleteEvent(args, inst);
      return false;
    }
  }

  private async updateResourceChange(args)
  {
    var ev = args.event;
    var os = args.event.ordemServico;
    os.codTecnico = ev.resource;

    this._osSvc.atualizar(os).toPromise().then(() =>
    {
      return this.updateEvent(args);
    }).catch(() =>
    {
      return false;
    });
  }

  private async updateEvent(args)
  {
    var ev = args.event;

    var agenda: AgendaTecnico = ev.agendaTecnico;
    agenda.codTecnico = ev.resource;
    agenda.inicio = moment(ev.start).format('yyyy-MM-DD HH:mm:ss');
    agenda.fim = moment(ev.end).format('yyyy-MM-DD HH:mm:ss');
    agenda.dataHoraManut = moment().format('yyyy-MM-DD HH:mm:ss');
    agenda.codUsuarioManut = this.userSession.usuario.codUsuario;

    if (moment(ev.end) > moment() && ev.agendaTecnico.tipo == AgendaTecnicoTypeEnum.OS)
    {
      agenda.cor = this._validator.getTypeColor(AgendaTecnicoTypeEnum.OS);
      var event = Enumerable.from(this.events).firstOrDefault(e => e.codAgendaTecnico == args.event.codAgendaTecnico);
      event.color = agenda.cor;
    }

    this._agendaTecnicoSvc.atualizar(agenda).toPromise().then(() =>
    {
      this._notify.toast({
        message: 'Agendamento atualizado com sucesso.',
        color: 'success'
      });
      return true;
    }).catch(() =>
    {
      this._notify.toast({
        message: 'Não foi possível atualizar o agendamento.',
        color: 'danger'
      });
      return false;
    })
  }

  public async deleteEvent(args, inst)
  {
    var os = args.event.ordemServico;
    var ag = os?.codAgendaTecnico;

    if (ag)
    {
      ag.indAtivo = 0;
      this._agendaTecnicoSvc.atualizar(ag).toPromise().then(() =>
      {
        inst.removeEvent(args.event);
        return;
      })
    }
    inst.removeEvent(args.event);
  }

  private async exibePontosDoDia()
  {
    this.tecnicos.map(tecnico =>
    {
      this.events = this.events.concat(this.carregaPonto(tecnico));
    });
  }

  private carregaPonto(tecnico: Tecnico): MbscAgendaTecnicoCalendarEvent[]
  {
    var pontos: MbscAgendaTecnicoCalendarEvent[] = [];

    Enumerable.from(tecnico.usuario.pontosUsuario)
      .where(p => p.indAtivo == 1)
      .forEach(p =>
      {
        pontos.push(
          {
            start: moment(p.dataHoraRegistro),
            end: moment(p.dataHoraRegistro).add(0, 'minutes'),
            title: "PONTO",
            color: '#C8C8C8',
            editable: false,
            resource: tecnico.codTecnico
          });
      });
    return pontos;
  }

  private async realocarAgendamento(args)
  {
    var now = moment();
    var initialTime = moment(args.date).format('yyyy-MM-DD HH:mm:ss');
    var codTecnico = args.resource;

    if (moment(args.date) < now) return;

    var agendamentosTecnico = (await this._agendaTecnicoSvc.obterPorParametros({
      sortActive: 'nome',
      sortDirection: 'asc',
      tipo: AgendaTecnicoTypeEnum.OS,
      codTecnico: codTecnico
    }).toPromise());

    var atendimentosTecnico: MbscAgendaTecnicoCalendarEvent[] = [];

    Enumerable.from(agendamentosTecnico.items).where(i => i.indAgendamento == 0).forEach(i =>
    {
      atendimentosTecnico.push(
        {
          codAgendaTecnico: i.codAgendaTecnico,
          start: i.inicio,
          end: i.fim,
          codOS: i.codOS,
          title: i.titulo,
          color: this._validator.getTypeColor(AgendaTecnicoTypeEnum.OS),
          editable: true,
          resource: i.codTecnico
        });
    });

    if (!atendimentosTecnico.length) return;

    var dialog = this._dialog.open(AgendaTecnicoRealocacaoDialogComponent, {
      data:
      {
        agendamentos: atendimentosTecnico,
        initialTime: initialTime,
        codTecnico: codTecnico
      }
    });

    dialog.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
        this.obterDados(false);
    });
  }

  /** */

  /** Mapa */
  public abrirMapa(codTecnico: number): void
  {
    const resource = Enumerable.from(this.resources)
      .firstOrDefault(r => r.id === codTecnico);

    const chamados = Enumerable.from(this.events)
      .where(e => e.resource === codTecnico && e.agendaTecnico.tipo == AgendaTecnicoTypeEnum.OS)
      .select(i => i.ordemServico)
      .toArray();

    this._dialog.open(RoteiroMapaComponent, {
      width: '960px',
      height: '640px',
      data: {
        resource: resource,
        chamados: chamados
      }
    });
  }

  /**  */

  private showOSInfo(args, inst)
  {
    const event: any = args.event;
    if (event.ordemServico == null) return;

    const time = formatDate('HH:mm', new Date(event.start)) + ' - ' + formatDate('HH:mm', new Date(event.end));
    this.currentEvent = event;
    this.interventionType = event.ordemServico?.tipoIntervencao?.nomTipoIntervencao;
    this.info = event.title + ', ' + event.ordemServico?.codOS;
    this.time = time;
    clearTimeout(this.timer);
    this.timer = null;
    this.anchor = args.domEvent.target;
    this.tooltip.open();
  }

  public countResourceEvents(resource: any): number
  {
    return Enumerable.from(this.events)
      .where(i => i.resource == resource.id && i.agendaTecnico.tipo ==
        AgendaTecnicoTypeEnum.OS && i.ordemServico.codStatusServico != StatusServicoEnum.FECHADO).count();
  }
}