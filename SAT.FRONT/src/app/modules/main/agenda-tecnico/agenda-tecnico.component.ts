import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { setOptions, localePtBR, Notifications, MbscEventcalendarOptions } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { AgendaTecnico, Coordenada, MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico, OrdemServicoFilterEnum, OrdemServicoIncludeEnum, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import { Tecnico, TecnicoData } from 'app/core/types/tecnico.types';
import moment, { Moment } from 'moment';
import Enumerable from 'linq';
import { HaversineService } from 'app/core/services/haversine.service';
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
import { AgendaTecnicoValidatorDialogComponent } from './agenda-tecnico-validator-dialog/agenda-tecnico-validator-dialog.component';

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
  tecnicos: TecnicoData;
  events: MbscAgendaTecnicoCalendarEvent[] = [];
  chamados: OrdemServico[] = [];
  resources = [];
  weekStart = moment().clone().startOf('isoWeek').format('yyyy-MM-DD HH:mm:ss');
  weekEnd = moment().clone().startOf('isoWeek').add(7, 'days').format('yyyy-MM-DD HH:mm:ss');

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
      if (this.hasOverlap(args, inst))
      {
        this._notify.toast({
          message: 'Os atendimentos não podem se sobrepor.'
        });
        return false;
      }
      else if (this.invalidInsert(args))
      {
        this._notify.toast({
          message: 'O atendimento não pode ser agendado para antes da linha do tempo.'
        });
        return false;
      }

      this.createNewEvent(args, inst);
    },
    onEventUpdate: (args, inst) =>
    {
      if (this.hasOverlap(args, inst))
      {
        this._notify.toast({
          message: 'Os atendimentos não podem se sobrepor.'
        });
        return false;
      }
      else if (this.hasChangedResource(args))
      {
        return this.updateResourceChange(args);
      }
      else if (this.invalidMove(args))
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
    private _haversineSvc: HaversineService,
    private _cdr: ChangeDetectorRef,
    private _agendaTecnicoSvc: AgendaTecnicoService,
    protected _userSvc: UserService,
    public _dialog: MatDialog,
    private _validator: AgendaTecnicoValidator
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

  private validateEvents(): void
  {
    var now = moment();
    Enumerable.from(this.events).where(e => e.ordemServico != null).forEach(e =>
    {
      if (moment(e.end) < now)
        e.color = this.getStatusColor(e.ordemServico.statusServico?.codStatusServico);
    });
    this._cdr.detectChanges();
  }

  private async carregaTecnicosEChamadosTransferidos(prompt: boolean = false)
  {
    if (prompt) this.loading = true;

    this.tecnicos = await this._tecnicoSvc.obterPorParametros({
      indAtivo: 1,
      codPerfil: RoleEnum.FILIAL_TECNICO_DE_CAMPO,
      codFiliais: this.getFiliais(),
      sortActive: 'nome',
      sortDirection: 'asc',
      codTecnicos: this.filter?.parametros?.codTecnicos
    }).toPromise();

    this.resources = this.tecnicos.items.map(tecnico =>
    {
      return {
        id: tecnico.codTecnico,
        name: tecnico.nome.toUpperCase(),
        indFerias: this.isOnVacation(tecnico),
        img: `https://sat.perto.com.br/DiretorioE/AppTecnicos/Fotos/${tecnico.usuario.codUsuario}.jpg`,
      }
    });

    this.chamados = (await this._osSvc.obterPorParametros({
      codFiliais: this.getFiliais(),
      include: OrdemServicoIncludeEnum.OS_AGENDA,
      filterType: OrdemServicoFilterEnum.FILTER_AGENDA,
      inicioPeriodoAgenda: this.weekStart,
      fimPeriodoAgenda: this.weekEnd
    }).toPromise()).items;

    const intervalos = await this._agendaTecnicoSvc.obterPorParametros({
      tipo: "INTERVALO",
      codFiliais: this.getFiliais(),
      data: moment().toISOString()
    }).toPromise();

    this.carregaDados(this.chamados, intervalos.items).then(() => { this.loading = false; });
  }

  private carregaPontos()
  {
    this.tecnicos.items.map(tecnico =>
    {
      this.events = this.events.concat(this.carregaPonto(tecnico));
    });
  }

  private async changeWeek(args, inst)
  {
    this.weekStart = moment(args.date).format('yyyy-MM-DD HH:mm:ss');
    this.weekEnd = moment(args.date).add(7, 'days').format('yyyy-MM-DD HH:mm:ss');
    await this.carregaTecnicosEChamadosTransferidos();
  }

  private isOnVacation(t: Tecnico): boolean
  {
    if (!t.indFerias)
      return false;

    // if (moment(t.dtFeriasInicio) >= moment() && moment(t.dtFeriasFim) <= moment())
    //   return true;

    return true;
  }

  private async carregaDados(chamados: OrdemServico[], intervalos: AgendaTecnico[])
  {
    this.events = [];
    // await this.carregaIntervalos(intervalos);
    await this.carregaOSs(chamados);
    this.carregaPontos();
  }

  private carregaOSs(chamados: OrdemServico[])
  {
    Enumerable.from(chamados)
      .where(os => os.tecnico != null)
      .groupBy(os => os.codTecnico)
      .forEach(async osPorTecnico =>
      {
        var codTecnico: number = osPorTecnico.key();
        var mediaTecnico = osPorTecnico.firstOrDefault().tecnico.mediaTempoAtendMin;
        mediaTecnico = mediaTecnico > 60 ? mediaTecnico : 60;

        for (const os of osPorTecnico)
        {
          var agenda = Enumerable.from(os.agendaTecnico)
            .firstOrDefault(i => i.codTecnico == codTecnico);

          var evento = os.agendaTecnico.length && agenda != null ?
            this.exibeEventoOSExistente(agenda, os) : (await this.criaNovoEventoOS(os, mediaTecnico, codTecnico));

          this.events = this.events.concat(evento);
        }
      });

    this.validateEvents();
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
      color: this.getInterventionColor(os.tipoIntervencao?.codTipoIntervencao),
      editable: true,
      resource: ag.codTecnico
    }

    return evento;
  }

  private async criaNovoEventoOS(os: OrdemServico, mediaTecnico: number, codTecnico: number)
  {
    var ultimoEvento = Enumerable.from(this.events)
      .where(i => i.resource == codTecnico && i.title != 'INTERVALO')
      .orderByDescending(i => i.end)
      .firstOrDefault();

    var deslocamento = this.calculaDeslocamentoEmMinutos(os, ultimoEvento?.ordemServico);
    var start = moment(ultimoEvento != null ? ultimoEvento.end : this.inicioExpediente()).add(deslocamento, 'minutes');

    // se começa durante a sugestão de intervalo ou deopis das 18h
    if (start.isBetween(this.inicioIntervalo(start), this.fimIntervalo(start)))
      start = moment(this.fimIntervalo(start)).add(deslocamento, 'minutes');
    else if (start.hour() >= this.fimExpediente().hour())
    {
      start = moment(this.inicioExpediente(start)).add(1, 'day').add(deslocamento, 'minutes');
      if (start.isBetween(this.inicioIntervalo(start), this.fimIntervalo(start)))
        start = moment(this.fimIntervalo(start)).add(deslocamento, 'minutes');
    }

    // se termina durante a sugestao de intervalo
    var end = moment(start).add(mediaTecnico, 'minutes');
    if (end.isBetween(this.inicioIntervalo(end), this.fimIntervalo(end)))
    {
      start = moment(this.fimIntervalo(end)).add(deslocamento, 'minutes');
      end = moment(start).add(mediaTecnico, 'minutes');
    }

    var evento: MbscAgendaTecnicoCalendarEvent =
    {
      start: start,
      end: end,
      ordemServico: os,
      title: os.localAtendimento?.nomeLocal.toUpperCase(),
      color: this.getInterventionColor(os.tipoIntervencao?.codTipoIntervencao),
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

  private async carregaIntervalos(intervalos: AgendaTecnico[])
  {
    this.events = this.events.concat(Enumerable.from(this.tecnicos.items).select(tecnico =>
    {
      var intervalo = Enumerable.from(intervalos).firstOrDefault(i => i.codTecnico == tecnico.codTecnico);
      return intervalo == null ? this.criaNovoIntervalo(tecnico) : this.exibeIntervaloExistente(intervalo);
    }).toArray());
  }

  private criaNovoIntervalo(tecnico: Tecnico): MbscAgendaTecnicoCalendarEvent
  {
    var start = this.inicioIntervalo();
    var end = this.fimIntervalo();

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
            end: moment(p.dataHoraRegistro).add(1, 'minutes'),
            title: "PONTO",
            color: '#FFFF00',
            editable: true,
            resource: tecnico.codTecnico,
          });
      });
    return pontos;
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
      resource: intervalo.codTecnico,
    }

    return evento;
  }

  private calculaDeslocamentoEmMinutos(os: OrdemServico, osAnterior: OrdemServico): number
  {
    var origem: Coordenada = new Coordenada();
    var destino: Coordenada = new Coordenada();

    // se ele já estava atendendo algum chamado, parte das coordenadas deste chamado
    if (osAnterior != null)
      origem.cordenadas = [osAnterior.localAtendimento?.latitude, osAnterior.localAtendimento?.longitude];
    // Se o técnico não possui nada agendado, parte do endereoç deste
    else
      origem.cordenadas = [os.tecnico?.latitude, os.tecnico?.longitude];

    destino.cordenadas = [os.localAtendimento?.latitude, os.localAtendimento?.longitude];

    return this._haversineSvc.getDistanceInMinutesPerKm(origem, destino, 50);
  }

  private getInterventionColor(tipoIntervencao: number): string
  {
    switch (tipoIntervencao)
    {
      case 1: //alteracao engenharia
        return "#067A52";
      case 2: //corretiva
        return "#3FC283";
      case 4: //preventiva
        return "#87E9A9";
      default:
        return "#D7F4D2";
    }
  }

  private getStatusColor(statusOS: number): string
  {
    switch (statusOS)
    {
      case 1: //aberto
        return "#ff4c4c";
      case 8: //transferido
        return "#ff4c4c";
      case 3: //fechado
        return "#7f7fff";
    }
  }

  private hasOverlap(args, inst)
  {
    var ev = args.event;
    var events = inst.getEvents(ev.start, ev.end).filter(e => e.resource == ev.resource && e.id != ev.id);
    return events.length > 0;
  }

  private hasChangedResource(args)
  {
    return args.event.resource != args.oldEvent.resource;
  }

  private async updateResourceChange(args): Promise<boolean>
  {
    var ev = args.event;

    if (this.isTechnicianInterval(args))
    {
      this._notify.toast({ message: 'Não é possível transferir um intervalo.' });
      return false;
    }
    else
    {
      ev.ordemServico.codTecnico = ev.resource;
      await this._osSvc.atualizar(ev.ordemServico).toPromise().then(() =>
      {
        return this.updateEvent(args);
      }).catch(() =>
      {
        return false;
      })
    }
  }

  private isTechnicianInterval(args)
  {
    return args.event.title === "INTERVALO";
  }

  private invalidMove(args)
  {
    //não pode mover evento posterior a linha do tempo para antes da linha do tempo
    var now = moment();
    return moment(args.oldEvent.start) > now && moment(args.event.start) < now;
  }

  private invalidInsert(args)
  {
    //não pode inserir evento anterior à linha do tempo
    var now = moment();
    return moment(args.event.start) < now;
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

    await this._agendaTecnicoSvc.atualizar(agenda).toPromise().then(() =>
    {
      this._notify.toast({ message: 'Agendamento atualizado com sucesso.' });
      this.updateEventColor(args)
      return true;
    }).catch(() =>
    {
      this._notify.toast({ message: 'Não foi possível atualizar o agendamento.' });
      return false;
    })
  }

  private updateEventColor(args)
  {
    if (args.event.ordemServico?.codOS > 0)
    {
      var event = Enumerable.from(this.events).firstOrDefault(e => e.codAgendaTecnico == args.event.codAgendaTecnico);
      event.color = this.getEventColor(args);
      this._cdr.detectChanges();
    }
  }

  private getEventColor(args)
  {
    return moment(args.event.end) > moment() ?
      this.getInterventionColor(args.event.ordemServico?.tipoIntervencao?.codTipoIntervencao)
      : this.getStatusColor(args.event.ordemServico?.statusServico?.codStatusServico);
  }

  private async checkForWarnings(ev)
  {
    var isFromSameRegion = (await this._validator.isRegiaoAtendimentoValida(ev.ordemServico, ev.resource));

    if (!isFromSameRegion)
    {
      this._dialog.open(AgendaTecnicoValidatorDialogComponent, {
        data: {
          message: `Você transferiu o chamado ${ev.ordemServico.codOS} para um técnico com a região diferente do chamado.`,
        }
      });
      return;
    }
  }

  private async createNewEvent(args, inst)
  {
    var ev = args.event;
    ev.color = this.getEventColor(args);

    this.checkForWarnings(ev);

    var agendaTecnico: AgendaTecnico =
    {
      inicio: moment(ev.start).format('yyyy-MM-DD HH:mm:ss'),
      fim: moment(ev.end).format('yyyy-MM-DD HH:mm:ss'),
      codOS: ev.ordemServico.codOS,
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

      await this._osSvc.atualizar(os).toPromise().then(() =>
      {
        this._notify.toast({ message: 'Atendimento agendado com sucesso.' });
        return true;
      }).catch(() =>
      {
        this._notify.toast({ message: 'Não foi possível fazer o agendamento.' });
        this.deleteEvent(args, inst);
        return false;
      })
    }
    else
    {
      this._notify.toast({ message: 'Não foi possível fazer o agendamento.' });
      this.deleteEvent(args, inst);
      return false;
    }
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

  public async deleteEvent(args, inst)
  {
    if (args.event.ordemServico?.codAgendaTecnico)
    {
      await this._agendaTecnicoSvc.deletar(args.event.ordemServico?.codAgendaTecnico).toPromise().then(() =>
      {
        inst.removeEvent(args.event);
        return;
      })
    }
    inst.removeEvent(args.event);
  }

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

  private inicioIntervalo(reference: Moment = moment())
  {
    return moment(reference).set({ hour: 12, minute: 0, second: 0, millisecond: 0 });
  }

  private fimIntervalo(reference: Moment = moment())
  {
    return moment(reference).set({ hour: 13, minute: 0, second: 0, millisecond: 0 });
  }

  private inicioExpediente(reference: Moment = moment())
  {
    return moment(reference).set({ hour: 8, minute: 0, second: 0, millisecond: 0 });
  }

  private fimExpediente(reference: Moment = moment())
  {
    return moment(reference).set({ hour: 18, minute: 0, second: 0, millisecond: 0 });
  }

  private getFiliais(): string
  {
    return this.filter?.parametros?.codFiliais;
  }

  private async realocarAgendamento(args)
  {
    var now = moment();
    var initialTime = moment(args.date).format('yyyy-MM-DD HH:mm:ss');
    var codTecnico = args.resource;

    if (moment(args.date) < now) return;

    var agendamentosTecnico = (await this._osSvc.obterPorParametros({
      codFiliais: this.getFiliais(),
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
          color: this.getInterventionColor(i.tipoIntervencao?.codTipoIntervencao),
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
}