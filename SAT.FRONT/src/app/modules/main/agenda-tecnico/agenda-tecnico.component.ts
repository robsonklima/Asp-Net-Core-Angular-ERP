import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { setOptions, localePtBR, Notifications, MbscEventcalendarOptions } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { AgendaTecnico, Coordenada, MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico, OrdemServicoFilterEnum, OrdemServicoIncludeEnum, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import moment, { Moment } from 'moment';
import Enumerable from 'linq';
import { HaversineService } from 'app/core/services/haversine.service';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';
import { fromEvent, interval, Subject } from 'rxjs';
import { MatSidenav } from '@angular/material/sidenav';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { MatDialog } from '@angular/material/dialog';
import { RoteiroMapaComponent } from './roteiro-mapa/roteiro-mapa.component';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { AgendaTecnicoRealocacaoDialogComponent } from './agenda-tecnico-realocacao-dialog/agenda-tecnico-realocacao-dialog.component';

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
  chamados: OrdemServico[] = [];
  resources = [];
  externalEvents: MbscAgendaTecnicoCalendarEvent = [];
  externalEventsFiltered: MbscAgendaTecnicoCalendarEvent = [];

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
    }
  };

  @ViewChild('sidenavChamados') sidenavChamados: MatSidenav;
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _notify: Notifications,
    private _tecnicoSvc: TecnicoService,
    private _osSvc: OrdemServicoService,
    private _haversineSvc: HaversineService,
    private _cdr: ChangeDetectorRef,
    private _agendaTecnicoSvc: AgendaTecnicoService,
    protected _userSvc: UserService,
    public _dialog: MatDialog
  )
  {
    super(_userSvc, 'agenda-tecnico')
  }

  registerEmitters(): void
  {
    this.sidenav.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.carregaChamadosAbertos();
      this.carregaTecnicosEChamadosTransferidos(true);
    });

    interval(10 * 60 * 1000)
      .pipe(
        startWith(0),
        takeUntil(this._onDestroy)
      )
      .subscribe((x) =>
      {
        if (!this.sidenavChamados.opened)
        {
          this.carregaTecnicosEChamadosTransferidos(!x);
          this.carregaChamadosAbertos();
        }
      });

    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) =>
      {
        return event.target.value;
      })
      , debounceTime(700)
      , distinctUntilChanged()
    ).subscribe((text: string) =>
    {
      this.filtrarChamadosAbertos(text);
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

    const tecnicos = await this._tecnicoSvc.obterPorParametros({
      indAtivo: 1,
      codPerfil: 35,
      codFiliais: this.getFiliais(),
      sortActive: 'nome',
      sortDirection: 'asc',
      codTecnicos: this.filter?.parametros?.codTecnicos
    }).toPromise();

    this.resources = tecnicos.items.map(tecnico =>
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
      sortActive: 'dataHoraTransf',
      sortDirection: 'asc'
    }).toPromise()).items;

    const intervalos = await this._agendaTecnicoSvc.obterPorParametros({
      tipo: "INTERVALO",
      codFiliais: this.getFiliais(),
      data: moment().toISOString()
    }).toPromise();

    this.carregaDados(this.chamados, tecnicos.items, intervalos.items).then(() => { this.loading = false; });
  }

  private isOnVacation(t: Tecnico): boolean
  {
    if (!t.indFerias)
      return false;

    // if (moment(t.dtFeriasInicio) >= moment() && moment(t.dtFeriasFim) <= moment())
    //   return true;

    return true;
  }

  private async carregaDados(chamados: OrdemServico[], tecnicos: Tecnico[], intervalos: AgendaTecnico[])
  {
    this.events = [];
    await this.carregaIntervalos(tecnicos, intervalos);
    await this.carregaOSs(chamados);
  }

  private carregaOSs(chamados: OrdemServico[])
  {
    this.events = this.events.concat(Enumerable.from(chamados)
      .where(os => os.tecnico != null)
      .groupBy(os => os.codTecnico)
      .selectMany(osPorTecnico =>
      {
        var codTecnico: number = osPorTecnico.key();
        var mediaTecnico = osPorTecnico.firstOrDefault().tecnico.mediaTempoAtendMin;
        mediaTecnico = mediaTecnico > 60 ? mediaTecnico : 60;
        var ultimoEvento: MbscAgendaTecnicoCalendarEvent;

        return (Enumerable.from(osPorTecnico)
          .orderBy(os => os.dataHoraTransf)
          .toArray()
          .map(os =>
          {
            var agenda = Enumerable.from(os.agendaTecnico)
              .firstOrDefault(i => i.codTecnico == codTecnico);

            var evento = os.agendaTecnico && agenda != null ?
              this.exibeEventoOSExistente(agenda, os) : this.criaNovoEventoOS(os, mediaTecnico, ultimoEvento);

            ultimoEvento = evento;
            return evento;
          }))

      }).toArray());

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

  private async criaNovoEventoOS(os: OrdemServico, mediaTecnico: number, ultimoEvento: MbscAgendaTecnicoCalendarEvent): Promise<MbscAgendaTecnicoCalendarEvent>
  {
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

    if (ag)
    {
      evento.codAgendaTecnico = ag?.codAgendaTecnico;
      return evento;
    }

    return null;
  }

  private async carregaIntervalos(tecnicos: Tecnico[], intervalos: AgendaTecnico[])
  {
    this.events = this.events.concat(Enumerable.from(tecnicos).select(tecnico =>
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

  private async carregaChamadosAbertos()
  {
    const data = await this._osSvc.obterPorParametros({
      codStatusServicos: StatusServicoEnum.ABERTO.toString(),
      codFiliais: this.getFiliais()
    }).toPromise();

    this.externalEvents = data.items.map(os =>
    {
      return {
        title: os.codOS.toString(),
        nomeLocal: os.localAtendimento?.nomeLocal,
        cliente: os.cliente?.razaoSocial,
        regiao: os.regiaoAutorizada?.regiao?.nomeRegiao,
        autorizada: os.regiaoAutorizada?.autorizada?.nomeFantasia,
        color: '#1064b0',
        start: moment(),
        end: moment().add(60, 'minutes'),
        ordemServico: os
      }
    });

    this.externalEventsFiltered = this.externalEvents;
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

  public filtrarChamadosAbertos(query: string)
  {
    if (query && query.trim() != '')
    {
      this.externalEventsFiltered = this.externalEvents.filter((ev) =>
      {
        return (
          ev.title?.toLowerCase().indexOf(query.toLowerCase()) > -1 ||
          ev.nomeLocal?.toLowerCase().indexOf(query.toLowerCase()) > -1 ||
          ev.cliente?.toLowerCase().indexOf(query.toLowerCase()) > -1 ||
          ev.regiao?.toLowerCase().indexOf(query.toLowerCase()) > -1 ||
          ev.autorizada?.toLowerCase().indexOf(query.toLowerCase()) > -1
        );
      })
    } else
    {
      this.externalEventsFiltered = this.externalEvents;
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

  private async createNewEvent(args, inst)
  {
    var ev = args.event;
    const eventIndex = this.externalEventsFiltered.map(function (e) { return e.title; }).indexOf(args.event.title);
    this.externalEventsFiltered.splice(eventIndex, 1);
    ev.color = this.getEventColor(args);

    var agendaTecnico: AgendaTecnico =
    {
      inicio: moment(ev.start).format('yyyy-MM-DD HH:mm:ss'),
      fim: moment(ev.end).format('yyyy-MM-DD HH:mm:ss'),
      codOS: ev.title,
      codTecnico: ev.resource,
      ultimaAtualizacao: moment().format('yyyy-MM-DD HH:mm:ss'),
      tipo: "OS"
    }

    var ag = (await this._agendaTecnicoSvc.criar(agendaTecnico).toPromise());

    if (ag)
    {
      ev.ordemServico.codTecnico = ag.codTecnico;
      ev.ordemServico.dataHoraTransf = ag.ultimaAtualizacao;
      ev.ordemServico.codAgendaTecnico = ag.codAgendaTecnico;
      ev.ordemServico.codStatusServico = StatusServicoEnum.TRANSFERIDO;
      ev.ordemServico.statusServico.codStatusServico = StatusServicoEnum.TRANSFERIDO;
      ev.ordemServico.agendaTecnico = ag;

      await this._osSvc.atualizar(ev.ordemServico).toPromise().then(() =>
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

    this._notify.toast({ message: 'Não foi possível fazer o agendamento.' });
    this.deleteEvent(args, inst);
    return false;
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

  private realocarAgendamento(args)
  {
    var now = moment();
    var initialTime = moment(args.date).format('yyyy-MM-DD HH:mm:ss');
    var codTecnico = args.resource;

    if (moment(args.date) < now) return;

    var atendimentosTecnico = Enumerable.from(this.events)
      .where(i => i.resource == codTecnico && i.ordemServico != null && i.ordemServico?.codStatusServico != StatusServicoEnum.FECHADO)
      .toArray();

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