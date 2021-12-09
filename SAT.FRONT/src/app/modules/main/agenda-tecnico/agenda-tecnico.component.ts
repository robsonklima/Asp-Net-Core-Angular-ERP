import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { setOptions, localePtBR, Notifications, MbscEventcalendarOptions } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { AgendaTecnico, MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico, OrdemServicoFilterEnum, OrdemServicoIncludeEnum, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
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
import { AgendaTecnicoFormatter } from './agenda-tecnico.formatter';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

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
})

export class AgendaTecnicoComponent extends Filterable implements AfterViewInit, IFilterable
{
  loading: boolean;
  userSession: UserSession;
  tecnicos: Tecnico[] = [];
  events: MbscAgendaTecnicoCalendarEvent[] = [];
  intervalos: AgendaTecnico[] = [];
  chamados: OrdemServico[] = [];
  resources = [];
  weekStart = moment().clone().startOf('isoWeek').format('yyyy-MM-DD HH:mm:ss');
  weekEnd = moment().clone().startOf('isoWeek').add(7, 'days').format('yyyy-MM-DD HH:mm:ss');
  snackConfigInfo: MatSnackBarConfig = { duration: 2000, panelClass: 'info' };

  calendarOptions: MbscEventcalendarOptions = {
    view: {
      timeline: {
        type: 'week',
        allDay: false,
        startDay: 1,
        startTime: '07:00',
        endTime: '24:00',
      }
    },
    dragToMove: true,
    externalDrop: true,
    dragToResize: false,
    dragToCreate: false,
    clickToCreate: false,
    onEventCreate: (args, inst) =>
    {
      if (this._validator.hasOverlap(args, inst))
      {
        this._notify.toast({
          message: 'Os atendimentos não podem se sobrepor.'
        });
        return false;
      }
      else if (this._validator.invalidInsert(args))
      {
        this._notify.toast({
          message: 'O atendimento não pode ser agendado para antes da linha do tempo.'
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
          message: 'Os atendimentos não podem se sobrepor.'
        });
        return false;
      }
      else if (this._validator.hasChangedResource(args))
      {
        return this.updateResourceChange(args);
      }
      else if (this._validator.invalidMove(args))
      {
        this._notify.toast({
          message: 'O atendimento não pode ser agendado para antes da linha do tempo.'
        });
        return false;
      }
      return this.updateEvent(args);
    },
    onEventClick: (args, inst) =>
    {
      this.showOSInfo(args);
    },
    onCellDoubleClick: (args, inst) =>
    {
      this.realocarAgendamento(args);
    },
    onSelectedDateChange: (args, inst) =>
    {
      this.changeWeek(args, inst)
    }
  };

  @ViewChild('sidenavChamados') sidenavChamados: MatSidenav;
  @ViewChild('sidenav') sidenavAgenda: MatSidenav;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _notify: Notifications,
    private _tecnicoSvc: TecnicoService,
    private _osSvc: OrdemServicoService,
    private _cdr: ChangeDetectorRef,
    private _agendaTecnicoSvc: AgendaTecnicoService,
    protected _userSvc: UserService,
    public _dialog: MatDialog,
    private _validator: AgendaTecnicoValidator,
    private _formatter: AgendaTecnicoFormatter,
    private _snack: MatSnackBar
  )
  {
    super(_userSvc, 'agenda-tecnico')
    this.carregaTecnicosEChamadosTransferidos(true);
  }

  registerEmitters(): void
  {
    this.sidenavAgenda.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.carregaTecnicosEChamadosTransferidos(true);
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
    if (this.userSession?.usuario?.codFilial && this.filter)
      this.filter.parametros.codFiliais = this.userSession.usuario.codFilial
  }

  private async carregaTecnicosEChamadosTransferidos(prompt: boolean = false)
  {
    if (prompt) this.loading = true;

    this.tecnicos = (await this._tecnicoSvc.obterPorParametros({
      indAtivo: 1,
      codPerfil: RoleEnum.FILIAL_TECNICO_DE_CAMPO,
      codFiliais: this.filter?.parametros?.codFiliais,
      sortActive: 'nome',
      sortDirection: 'asc',
      codTecnicos: this.filter?.parametros?.codTecnicos
    }).toPromise()).items;

    this.resources = this.tecnicos.map(tecnico =>
    {
      return {
        id: tecnico.codTecnico,
        name: tecnico.nome.toUpperCase(),
        indFerias: this._validator.isOnVacation(tecnico),
        img: `https://sat.perto.com.br/DiretorioE/AppTecnicos/Fotos/${tecnico.usuario.codUsuario}.jpg`,
      }
    });

    this.chamados = (await this._osSvc.obterPorParametros({
      codFiliais: this.filter?.parametros?.codFiliais,
      include: OrdemServicoIncludeEnum.OS_AGENDA,
      filterType: OrdemServicoFilterEnum.FILTER_AGENDA,
      inicioPeriodoAgenda: this.weekStart,
      fimPeriodoAgenda: this.weekEnd
    }).toPromise()).items;

    this.intervalos = (await this._agendaTecnicoSvc.obterPorParametros({
      tipo: "INTERVALO",
      codFiliais: this.filter?.parametros?.codFiliais,
      inicioPeriodoAgenda: this.weekStart,
      fimPeriodoAgenda: this.weekEnd
    }).toPromise()).items;

    this.carregaCalendario()
      .then(() => { this.loading = false; });
  }

  private async carregaCalendario()
  {
    this.events = [];
    this.carregaPontos();
    await this.carregaIntervalos();
    await this.carregaOSs();
  }

  /** Atendimentos */

  private async carregaOSs()
  {
    var osPorTecnico = Enumerable.from(this.chamados)
      .where(os => os.tecnico != null)
      .groupBy(os => os.codTecnico);

    for (const t of osPorTecnico)
    {
      var codTecnico: number = t.key();
      var mediaTecnico = t.firstOrDefault().tecnico.mediaTempoAtendMin;
      mediaTecnico = mediaTecnico > 60 ? mediaTecnico : 60;

      for (const os of t)
      {
        var agenda = Enumerable.from(os.agendaTecnico)
          .firstOrDefault(i => i.codTecnico == codTecnico);

        var evento = os.agendaTecnico.length && agenda != null ?
          this.exibeEventoOSExistente(agenda, os) : (await this.criaNovoEventoOS(os, mediaTecnico, codTecnico));

        this.events = this.events.concat(evento);
      }
    }

    this.validateEvents();
  }

  private async criaNovoEventoOS(os: OrdemServico, mediaTecnico: number, codTecnico: number)
  {
    var ultimoEvento = Enumerable.from(this.events)
      .where(i => i.resource == codTecnico && i.title != 'INTERVALO')
      .orderByDescending(i => i.end)
      .firstOrDefault();

    var deslocamento = this._validator.calculaDeslocamentoEmMinutos(os, ultimoEvento?.ordemServico);
    var start = moment(ultimoEvento != null ? ultimoEvento.end : this._validator.inicioExpediente()).add(deslocamento, 'minutes');

    // se começa durante a sugestão de intervalo ou deopis das 18h
    if (start.isBetween(this._validator.inicioIntervalo(start), this._validator.fimIntervalo(start)))
      start = moment(this._validator.fimIntervalo(start)).add(deslocamento, 'minutes');
    else if (start.hour() >= this._validator.fimExpediente().hour())
    {
      start = moment(this._validator.inicioExpediente(start)).add(1, 'day').add(deslocamento, 'minutes');
      if (start.isBetween(this._validator.inicioIntervalo(start), this._validator.fimIntervalo(start)))
        start = moment(this._validator.fimIntervalo(start)).add(deslocamento, 'minutes');
    }

    // se termina durante a sugestao de intervalo
    var end = moment(start).add(mediaTecnico, 'minutes');
    if (end.isBetween(this._validator.inicioIntervalo(end), this._validator.fimIntervalo(end)))
    {
      start = moment(this._validator.fimIntervalo(end)).add(deslocamento, 'minutes');
      end = moment(start).add(mediaTecnico, 'minutes');
    }

    var evento: MbscAgendaTecnicoCalendarEvent =
    {
      start: start,
      end: end,
      ordemServico: os,
      title: os.localAtendimento?.nomeLocal.toUpperCase(),
      color: this._formatter.getInterventionColor(os.tipoIntervencao?.codTipoIntervencao),
      editable: true,
      resource: os.tecnico?.codTecnico,
    }

    var agendaTecnico: AgendaTecnico =
    {
      inicio: start.format('yyyy-MM-DD HH:mm:ss'),
      fim: end.format('yyyy-MM-DD HH:mm:ss'),
      codOS: os.codOS,
      codTecnico: os.codTecnico,
      ultimaAtualizacao: moment().format('yyyy-MM-DD HH:mm:ss'),
      tipo: "OS"
    }

    var ag = (await this._agendaTecnicoSvc.criar(agendaTecnico).toPromise());

    evento.codAgendaTecnico = ag?.codAgendaTecnico;
    return evento;
  }

  private exibeEventoOSExistente(ag: AgendaTecnico, os: OrdemServico): MbscAgendaTecnicoCalendarEvent
  {
    var evento: MbscAgendaTecnicoCalendarEvent =
    {
      codAgendaTecnico: ag.codAgendaTecnico,
      start: ag.inicio,
      end: ag.fim,
      ordemServico: os,
      title: os.localAtendimento?.nomeLocal.toUpperCase(),
      color: this._formatter.getInterventionColor(os.tipoIntervencao?.codTipoIntervencao),
      editable: true,
      resource: ag.codTecnico
    }

    return evento;
  }

  /**  */

  /** Ponto */

  private carregaPontos()
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

  /**  */

  /** Intervalos */

  private async carregaIntervalos()
  {
    for (const t of this.tecnicos)
    {
      var intervalo = Enumerable.from(this.intervalos).firstOrDefault(i => i.codTecnico == t.codTecnico);
      var evento = intervalo == null ? this.criaNovoIntervalo(t) : this.exibeIntervaloExistente(intervalo);
      this.events = this.events.concat(evento);
    }
  };

  private criaNovoIntervalo(tecnico: Tecnico): MbscAgendaTecnicoCalendarEvent
  {
    var start = this._validator.inicioIntervalo();
    var end = this._validator.fimIntervalo();

    var evento: MbscAgendaTecnicoCalendarEvent =
    {
      start: start,
      end: end,
      title: "INTERVALO",
      color: '#808080',
      editable: true,
      resource: tecnico.codTecnico,
    }

    var agendaTecnico: AgendaTecnico =
    {
      inicio: start.format('yyyy-MM-DD HH:mm:ss'),
      fim: end.format('yyyy-MM-DD HH:mm:ss'),
      codTecnico: tecnico.codTecnico,
      ultimaAtualizacao: moment().format('yyyy-MM-DD HH:mm:ss'),
      tipo: "INTERVALO"
    }

    this._agendaTecnicoSvc.criar(agendaTecnico).subscribe(agendamento =>
    {
      evento.codAgendaTecnico = agendamento.codAgendaTecnico;
    });

    return evento;
  }

  private exibeIntervaloExistente(intervalo: AgendaTecnico): MbscAgendaTecnicoCalendarEvent
  {
    var evento: MbscAgendaTecnicoCalendarEvent =
    {
      codAgendaTecnico: intervalo.codAgendaTecnico,
      start: intervalo.inicio,
      end: intervalo.fim,
      title: intervalo.tipo,
      color: '#808080',
      editable: true,
      resource: intervalo.codTecnico
    }

    return evento;
  }

  /**  */

  /** Validações */

  private validateEvents(): void
  {
    this._validator.validateEvents(this.events);
    this._cdr.detectChanges();
  }

  private async checkForWarnings(ev, args, inst)
  {
    var isFromSameRegion = (await this._validator.isTecnicoDaRegiaoDoChamado(ev.ordemServico, ev.resource));
    var tecMaisProximo = this._validator.isTecnicoOMaisProximo(ev.ordemServico, this.tecnicos, this.events, ev.resource);

    if (!isFromSameRegion)
    {
      var message: string = `Você transferiu o chamado ${ev.ordemServico.codOS} para um técnico com a região diferente do chamado.`;
      await this._snack.open(message, null, this.snackConfigInfo).afterDismissed().toPromise();
    }

    if (tecMaisProximo != null)
    {
      const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
        data: {
          titulo: 'Aviso',
          message: tecMaisProximo.message,
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
          var deslocamento = tecMaisProximo.minDistancia;
          ev.resource = tecMaisProximo.codTecnicoMinDistancia;
          ev.start = moment(tecMaisProximo.ultimoAtendimentoTecnico.end)
            .add(deslocamento, 'minutes')
            .format('yyyy-MM-DD HH:mm:ss');
          ev.end = moment(tecMaisProximo.ultimoAtendimentoTecnico.end)
            .add(deslocamento, 'minutes')
            .add(1, 'hour')
            .format('yyyy-MM-DD HH:mm:ss');
          this.createEvent(ev, args, inst).then(() => this.carregaTecnicosEChamadosTransferidos(true));
        }
        else
        {
          this.createEvent(ev, args, inst);
        }
      });
    }
    else this.createEvent(ev, args, inst);
  }

  /**  */

  /** Mobiscroll */

  private async changeWeek(args, inst)
  {
    this.weekStart = moment(args.date).format('yyyy-MM-DD HH:mm:ss');
    this.weekEnd = moment(args.date).add(7, 'days').format('yyyy-MM-DD HH:mm:ss');
    await this.carregaTecnicosEChamadosTransferidos();
  }

  private async validateNewEvent(args, inst)
  {
    var ev = args.event;
    ev.color = this._formatter.getEventColor(args);
    this.checkForWarnings(ev, args, inst);
  }

  private async createEvent(ev, args, inst)
  {
    var agendaTecnico: AgendaTecnico =
    {
      inicio: moment(ev.start).format('yyyy-MM-DD HH:mm:ss'),
      fim: moment(ev.end).format('yyyy-MM-DD HH:mm:ss'),
      codOS: ev.ordemServico?.codOS,
      codTecnico: ev.resource,
      ultimaAtualizacao: moment().format('yyyy-MM-DD HH:mm:ss'),
      tipo: "OS"
    }

    var ag = (await this._agendaTecnicoSvc.criar(agendaTecnico).toPromise());

    this.sidenavChamados.close();

    if (ag != null)
    {
      var os = (await this._osSvc.obterPorCodigo(ev.ordemServico.codOS).toPromise());

      os.codTecnico = ag.codTecnico;
      os.dataHoraTransf = ag.ultimaAtualizacao;
      os.codStatusServico = StatusServicoEnum.TRANSFERIDO;
      os.statusServico.codStatusServico = StatusServicoEnum.TRANSFERIDO;
      ev.codAgendaTecnico = ag.codAgendaTecnico;

      var os = (await this._osSvc.atualizar(os).toPromise());

      if (os)
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

    if (this._validator.isTechnicianInterval(args))
    {
      this._notify.toast({
        message: 'Não é possível transferir um intervalo.',
        color: 'danger'
      });
      return false;
    }
    else
    {
      ev.ordemServico.codTecnico = ev.resource;
      this._osSvc.atualizar(ev.ordemServico).toPromise().then(() =>
      {
        return this.updateEvent(args);
      }).catch(() =>
      {
        return false;
      })
    }
  }

  private async updateEvent(args)
  {
    var agenda: AgendaTecnico =
    {
      codAgendaTecnico: args.event.codAgendaTecnico,
      inicio: moment(args.event.start).format('yyyy-MM-DD HH:mm:ss'),
      fim: moment(args.event.end).format('yyyy-MM-DD HH:mm:ss'),
      codTecnico: args.event.resource,
      codOS: args.event.ordemServico?.codOS ?? 0,
      tipo: args.event.ordemServico != null ? "OS" : "INTERVALO",
      ultimaAtualizacao: moment().format('yyyy-MM-DD HH:mm:ss'),
    }

    this._agendaTecnicoSvc.atualizar(agenda).toPromise().then(() =>
    {
      this._notify.toast({
        message: 'Agendamento atualizado com sucesso.',
        color: 'success'
      });
      this.updateEventColor(args)
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

  private updateEventColor(args)
  {
    this._formatter.updateEventColor(this.events, args);
    this._cdr.detectChanges();
  }

  public async deleteEvent(args, inst)
  {
    var os = args.event.ordemServico;
    if (os?.codAgendaTecnico)
    {
      this._agendaTecnicoSvc.deletar(os?.codAgendaTecnico).toPromise().then(() =>
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

    var agendamentosTecnico = (await this._osSvc.obterPorParametros({
      codFiliais: this.filter?.parametros?.codFiliais,
      include: OrdemServicoIncludeEnum.OS_AGENDA,
      filterType: OrdemServicoFilterEnum.FILTER_AGENDA,
      codTecnico: codTecnico
    }).toPromise()).items;

    var atendimentosTecnico: MbscAgendaTecnicoCalendarEvent[] = [];

    Enumerable.from(agendamentosTecnico).forEach(i =>
    {
      var ag = Enumerable.from(i.agendaTecnico)
        .firstOrDefault(i => i.codTecnico == codTecnico);
      atendimentosTecnico.push(
        {
          codAgendaTecnico: ag.codAgendaTecnico,
          start: ag.inicio,
          end: ag.fim,
          ordemServico: i,
          title: i.localAtendimento?.nomeLocal.toUpperCase(),
          color: this._formatter.getInterventionColor(i.tipoIntervencao?.codTipoIntervencao),
          editable: true,
          resource: ag.codTecnico
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
        this.carregaTecnicosEChamadosTransferidos();
    });
  }

  private showOSInfo(args)
  {
    var os = args.event.ordemServico;

    if (os == null) return;

    var text = "";
    if (os.localAtendimento?.nomeLocal) text += os.localAtendimento?.nomeLocal + '\n';
    if (os.tipoIntervencao?.nomTipoIntervencao) text += 'Intervenção ' + os.tipoIntervencao?.nomTipoIntervencao + '\n';

    this._notify.alert(
      {
        title: 'OS ' + os.codOS.toString(),
        message: text.toUpperCase(),
        display: 'center',
        cssClass: 'os_info'
      }
    );
  }

  /** */

  /** Mapa */

  public abrirMapa(codTecnico: number): void
  {
    const resource = this.resources.filter(r => r.id === codTecnico).shift();
    const chamados = this.chamados.filter(os => os.codTecnico === codTecnico);

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
}