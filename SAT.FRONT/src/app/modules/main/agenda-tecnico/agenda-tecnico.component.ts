import { AfterViewInit, Component, LOCALE_ID, ViewChild, ViewEncapsulation } from '@angular/core';
import { setOptions, localePtBR, MbscEventcalendarOptions, formatDate, MbscPopup, MbscPopupOptions, Notifications } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { AgendaTecnico, AgendaTecnicoTypeEnum, MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { StatusServicoEnum } from 'app/core/types/ordem-servico.types';
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
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { AgendaTecnicoAjudaDialogComponent } from './agenda-tecnico-ajuda-dialog/agenda-tecnico-ajuda-dialog.component';
import { AgendaTecnicoOrdenacaoDialogComponent } from './agenda-tecnico-ordenacao-dialog/agenda-tecnico-ordenacao-dialog.component';

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

  snackConfigInfo: MatSnackBarConfig = { duration: 4000, panelClass: 'info', verticalPosition: 'top', horizontalPosition: 'right' };
  snackConfigDanger: MatSnackBarConfig = { duration: 2000, panelClass: 'danger', verticalPosition: 'top', horizontalPosition: 'right' };
  snackConfigSuccess: MatSnackBarConfig = { duration: 2000, panelClass: 'success', verticalPosition: 'top', horizontalPosition: 'right' };

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
  status = '';
  selectResource: any;
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
        rowHeight: 'equal'
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
        this._snack.open('Os atendimentos não podem se sobrepor.', null, this.snackConfigDanger).afterDismissed().toPromise();
        return false;
      }
      else if (this._validator.invalidInsert(args))
      {
        this._snack.open('O atendimento não pode ser agendado para antes da linha do tempo.', null, this.snackConfigDanger).afterDismissed().toPromise();
        return false;
      }

      this.validateNewEvent(args, inst);
    },
    onEventUpdate: (args, inst) =>
    {
      if (this._validator.hasOverlap(args, inst))
      {
        this._snack.open('Os atendimentos não podem se sobrepor.', null, this.snackConfigDanger).afterDismissed().toPromise();
        return false;
      }
      else if (this._validator.cantChangeInterval(args))
      {
        this._snack.open('Não é possível transferir um intervalo.', null, this.snackConfigDanger).afterDismissed().toPromise();
        return false;
      }
      else if (this._validator.hasChangedResource(args))
      {
        this.updateResourceChange(args.event);
        return true;
      }
      else if (this._validator.invalidMove(args))
      {
        this._snack.open('O atendimento não pode ser agendado para antes da linha do tempo.', null, this.snackConfigDanger).afterDismissed().toPromise();
        return false;
      }
      else 
      {
        this.updateEvent(args.event);
        return true;
      }
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
      this.showEventInfo(args, inst);
    },
    onEventHoverOut: () =>
    {
      this.hideEventInfo();
    }
  };

  @ViewChild('sidenavChamados') sidenavChamados: MatSidenav;
  @ViewChild('sidenav') sidenavAgenda: MatSidenav;
  protected _onDestroy = new Subject<void>();

  constructor (
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
    this.carregaDados();
  }

  registerEmitters(): void
  {
    this.sidenavAgenda.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.carregaDados();
    });
  }

  ngAfterViewInit(): void 
  {
    this.registerEmitters();
  }

  loadFilter(): void
  {
    super.loadFilter();

    // Filtro obrigatorio de filial quando o usuario esta vinculado a uma filial
    if (this.userSession?.usuario?.codFilial)
      this.filter.parametros.codFiliais = this.userSession.usuario.codFilial;
    else if (!this.userSession?.usuario?.codFilial && !this.filter.parametros.codFiliais)
      this.filter.parametros.codFiliais = 4;
  }

  private async carregaDados(showLoading: boolean = true)
  {
    this.loading = showLoading;
    await this.obterDados().then(() =>
    {
      this.loading = false;
    });
  }

  private async obterDados()
  {
    this.tecnicos = (await this._tecnicoSvc.obterPorParametros({
      indAtivo: 1,
      codPerfil: RoleEnum.FILIAL_TECNICO_DE_CAMPO,
      codFiliais: this.filter?.parametros?.codFiliais,
      pas: this.filter?.parametros?.pas,
      codTecnicos: this.filter?.parametros?.codTecnicos,
      codRegioes: this.filter?.parametros?.codRegioes,
      sortActive: 'nome',
      sortDirection: 'asc'
    }).toPromise()).items;

    this.events = [];
    this.resources = Enumerable.from(this.tecnicos).select(tecnico =>
    {
      return {
        id: tecnico.codTecnico,
        name: tecnico.nome.toUpperCase(),
        indFerias: this._validator.isOnVacation(tecnico),
        img: `https://sat.perto.com.br/DiretorioE/AppTecnicos/Fotos/${tecnico.usuario.codUsuario}.jpg`,
      }
    }).toArray();

    if (!Enumerable.from(this.resources).any()) return;

    Enumerable.from(this.tecnicos).forEach(t => 
    {
      this._agendaTecnicoSvc.obterAgendaTecnico({
        codFiliais: this.filter?.parametros?.codFiliais,
        codTecnico: t.codTecnico,
        codUsuario: t.usuario.codUsuario,
        inicioPeriodoAgenda: this.weekStart,
        fimPeriodoAgenda: this.weekEnd,
        sortActive: 'nome',
        sortDirection: 'asc'
      }).toPromise().then(agendamentos =>
      {
        this.agendaTecnicos = this.agendaTecnicos.concat(agendamentos);
        this.carregaAgendaTecnico(agendamentos);
      });
    });
  }

  private atualizaLinhaTecnico(resourceId: number)
  {
    var tecnico = Enumerable.from(this.tecnicos).firstOrDefault(i => i.codTecnico == resourceId);

    this.events = Enumerable.from(this.events)
      .where(i => i.resource != resourceId).toArray();

    this._agendaTecnicoSvc.obterAgendaTecnico({
      codFiliais: this.filter?.parametros?.codFiliais,
      codTecnico: tecnico.codTecnico,
      codUsuario: tecnico.usuario.codUsuario,
      inicioPeriodoAgenda: this.weekStart,
      fimPeriodoAgenda: this.weekEnd,
      sortActive: 'nome',
      sortDirection: 'asc'
    }).toPromise().then(agendamentos =>
    {
      this.agendaTecnicos = this.agendaTecnicos.concat(agendamentos);
      this.carregaAgendaTecnico(agendamentos);
    });
  }

  carregaAgendaTecnico(agendamentos: AgendaTecnico[])
  {
    this.events = this.events.concat(Enumerable.from(agendamentos).select(ag =>
    {
      return {
        codAgendaTecnico: ag.codAgendaTecnico,
        codOS: ag.codOS,
        agendaTecnico: ag,
        start: moment(ag.inicio),
        end: moment(ag.fim),
        title: ag.titulo,
        color: ag.cor,
        editable: ag.indAgendamento == 1 || ag.tipo == AgendaTecnicoTypeEnum.PONTO ? false : true,
        resource: ag.codTecnico,
        ordemServico: ag.ordemServico
      }
    }).toArray());
  }

  private async checkForWarnings(ev, args, inst)
  {
    var isFromSameRegion = (await this._validator.isTecnicoDaRegiaoDoChamado(ev.ordemServico, ev.resource));
    var isAgendado = ev.indAgendamento == 1;
    var hasTecnicoMaisProximo = isAgendado ? null : (await this._validator.isTecnicoOMaisProximo(ev.ordemServico, this.tecnicos, this.events, ev.resource))

    if (!isFromSameRegion)
    {
      var message: string = `Você transferiu o chamado ${ev.ordemServico?.codOS} para um técnico com a região diferente do chamado.`;
      await this._snack.open(message, null, this.snackConfigInfo).afterDismissed().toPromise();
    }

    if (isAgendado)
    {
      var message: string = `Você transferiu um chamado com agendamento marcado. Ele será realocado automaticamente.`;
      await this._snack.open(message, null, this.snackConfigInfo).afterDismissed().toPromise();
    }

    if (hasTecnicoMaisProximo != null)
    {
      const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
        data: {
          titulo: 'Aviso',
          message: hasTecnicoMaisProximo.message,
          buttonText: {
            ok: 'Sim',
            cancel: 'Não'
          }
        }
      });

      dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
      {
        if (confirmacao)
        {
          ev.resource = hasTecnicoMaisProximo.codTecnicoMinDistancia;
          ev.start = moment(hasTecnicoMaisProximo.ultimoAtendimentoTecnico.end).add(hasTecnicoMaisProximo.minDistancia, 'minute').format('yyyy-MM-DD HH:mm:ss');
          ev.end = moment(hasTecnicoMaisProximo.ultimoAtendimentoTecnico.end).add(hasTecnicoMaisProximo.minDistancia, 'minute').add(1, 'hour').format('yyyy-MM-DD HH:mm:ss');
          this.createExternalEvent(ev, args, inst).then(() => this.atualizaLinhaTecnico(ev.resource));
          return;
        }
        else
        {
          this.createExternalEvent(ev, args, inst);
        }
      });
    }
    else
    {
      this.createExternalEvent(ev, args, inst);
    }
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
    await this.carregaDados(false);
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
      if (ag.indAgendamento == 1) this.atualizaLinhaTecnico(ev.resource);

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
        await this._snack.open('Atendimento agendado com sucesso.', null, this.snackConfigSuccess).afterDismissed().toPromise();
        return true;
      }
      else
      {
        await this._snack.open('Não foi possível fazer o agendamento', null, this.snackConfigDanger).afterDismissed().toPromise();
        this.deleteEvent(args, inst);
        return false;
      }
    }
    else
    {
      await this._snack.open('Não foi possível fazer o agendamento', null, this.snackConfigDanger).afterDismissed().toPromise();

      this.deleteEvent(args, inst);
      return false;
    }
  }

  private async updateResourceChange(event)
  {
    var os = event.ordemServico;
    os.codTecnico = event.resource;
    this._osSvc.atualizar(os).subscribe(() =>
    {
      this.updateEvent(event);
    });
  }

  private async updateEvent(ev)
  {
    var agenda: AgendaTecnico = ev.agendaTecnico;
    agenda.codTecnico = ev.resource;
    agenda.inicio = moment(ev.start).format('yyyy-MM-DD HH:mm:ss');
    agenda.fim = moment(ev.end).format('yyyy-MM-DD HH:mm:ss');
    agenda.dataHoraManut = moment().format('yyyy-MM-DD HH:mm:ss');
    agenda.codUsuarioManut = this.userSession.usuario.codUsuario;

    var event = Enumerable.from(this.events).firstOrDefault(e => e.codAgendaTecnico == agenda.codAgendaTecnico);
    if (agenda.tipo == AgendaTecnicoTypeEnum.OS && moment(ev.end) > moment())
    {
      agenda.cor = this._validator.getTypeColor(AgendaTecnicoTypeEnum.OS);
      event.color = agenda.cor;
    }

    var ag = await this._agendaTecnicoSvc.atualizar(agenda).toPromise();
    if (ag != null)
    {
      await this._snack.open('Agendamento atualizado com sucesso.', null, this.snackConfigSuccess).afterDismissed().toPromise();
      event.agendaTecnico = ag;
      var message = this._validator.validaDistanciaEntreEventos(event, this.events);
      if (message)
        await this._snack.open(message, null, this.snackConfigInfo).afterDismissed().toPromise();
    }
    else
    {
      await this._snack.open('Não foi possível fazer o agendamento.', null, this.snackConfigDanger).afterDismissed().toPromise();
    }
  }

  public async deleteEvent(args, inst)
  {
    var ag = args.event?.agendaTecnico;

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
        this.atualizaLinhaTecnico(codTecnico);
    });
  }

  /** */

  /** Mapa */
  public abrirMapa(codTecnico: number): void
  {
    const codUsuario = Enumerable.from(this.tecnicos)
      .firstOrDefault(r => r.codTecnico === codTecnico).usuario.codUsuario;

    const chamados = Enumerable.from(this.events)
      .where(e => e.resource === codTecnico && e.agendaTecnico.tipo == AgendaTecnicoTypeEnum.OS && e.ordemServico.codStatusServico != StatusServicoEnum.FECHADO)
      .select(i => i.ordemServico)
      .toArray();

    this._dialog.open(RoteiroMapaComponent, {
      width: '960px',
      height: '640px',
      data: {
        codUsuario: codUsuario,
        chamados: chamados
      }
    });
  }

  public showHelp()
  {
    this._dialog.open(AgendaTecnicoAjudaDialogComponent);
  }

  /**  */

  public hideEventInfo()
  {
    if (!this.timer)
    {
      this.timer = setTimeout(() =>
      {
        this.tooltip.close();
      }, 200);
    }
  }

  private showEventInfo(args, inst)
  {
    const event: any = args.event;
    const time = event.agendaTecnico?.tipo == AgendaTecnicoTypeEnum.PONTO ? formatDate('HH:mm', new Date(event.start)) : formatDate('HH:mm', new Date(event.start)) + ' - ' + formatDate('HH:mm', new Date(event.end));
    this.currentEvent = event;
    this.interventionType = event.ordemServico?.tipoIntervencao?.nomTipoIntervencao;
    this.info = event.ordemServico != null ? (event.title + '- ' + event.ordemServico?.codOS) : event.agendaTecnico?.tipo == AgendaTecnicoTypeEnum.PONTO ? "PONTO" : "INTERVALO";
    this.time = time;
    this.status = event.ordemServico?.statusServico?.nomeStatusServico;
    clearTimeout(this.timer);
    this.timer = null;
    this.selectResource = null;
    this.anchor = args.domEvent.target;
    this.tooltip.open();
  }

  public showResourceAction(resource)
  {
    var target = (window.event as any).target;
    this.selectResource = resource;
    this.info = resource.name;
    clearTimeout(this.timer);
    this.anchor = target;

    this.interventionType = null;
    this.time = null;
    this.status = null;
    this.currentEvent = null;
    this.timer = null;

    this.tooltip.open();
  }

  public ordenarChamados(resourceID: number)
  {
    var tecnico = Enumerable.from(this.tecnicos).firstOrDefault(i => i.codTecnico == resourceID);

    const dialogRef = this._dialog.open(AgendaTecnicoOrdenacaoDialogComponent, {
      data: {
        tecnico: tecnico,
        weekStart: this.weekStart,
        weekEnd: this.weekEnd
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
        this.atualizaLinhaTecnico(resourceID);
    });
  }

  public countResourceEvents(resource: any): number
  {
    return Enumerable.from(this.events)
      .where(i => i.resource == resource.id && i.agendaTecnico?.tipo ==
        AgendaTecnicoTypeEnum.OS && i.ordemServico.codStatusServico != StatusServicoEnum.FECHADO).count();
  }

  public countPontoEvents(resource: any): number
  {
    return Enumerable.from(this.events)
      .where(i => i.resource == resource.id && i.agendaTecnico?.tipo ==
        AgendaTecnicoTypeEnum.PONTO).count();
  }
}