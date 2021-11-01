import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { setOptions, localePtBR, Notifications, MbscEventcalendarOptions, MbscEventcalendar } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { AgendaTecnico, Coordenada, MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico, OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
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
import { AbstractControl, FormGroup } from '@angular/forms';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

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

export class AgendaTecnicoComponent implements AfterViewInit, OnInit
{
  loading: boolean;
  filtro: any;
  userSession: UserSession;
  tecnicos: Tecnico[] = [];
  events: MbscAgendaTecnicoCalendarEvent[] = [];
  chamados: OrdemServico[] = [];
  form: FormGroup;
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
    }
  };

  @ViewChild('sidenavChamados') sidenavChamados: MatSidenav;
  @ViewChild('sidenavFiltro') sidenavFiltro: MatSidenav;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _notify: Notifications,
    private _tecnicoSvc: TecnicoService,
    private _osSvc: OrdemServicoService,
    private _haversineSvc: HaversineService,
    private _cdr: ChangeDetectorRef,
    private _agendaTecnicoSvc: AgendaTecnicoService,
    private _userSvc: UserService,
    public _dialog: MatDialog
  )
  {
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void
  {
    this.carregarFiltro();

    this.sidenavFiltro.closedStart.subscribe(() =>
    {
      this.carregarFiltro();
      this.carregaTecnicosEChamadosTransferidos();
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

  ngOnInit(): void { }

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

    const params = {
      indAtivo: 1,
      codFiliais: this.getFiliais(),
      codPerfil: 35,
      periodoMediaAtendInicio: moment().add(-7, 'days').format('yyyy-MM-DD 00:00'),
      periodoMediaAtendFim: moment().format('yyyy-MM-DD 23:59'),
      sortActive: 'nome',
      sortDirection: 'asc'
    };

    const tecnicos = await this._tecnicoSvc.obterPorParametros({
      ...params, ...this.filtro?.parametros
    }).toPromise();

    this.resources = tecnicos.items.map(tecnico =>
    {
      return {
        id: tecnico.codTecnico,
        name: tecnico.nome,
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
        var mediaTecnico = osPorTecnico.firstOrDefault().tecnico.mediaTempoAtendMin;
        mediaTecnico = mediaTecnico > 60 ? mediaTecnico : 60;
        var ultimoEvento: MbscAgendaTecnicoCalendarEvent;

        return (Enumerable.from(osPorTecnico)
          .orderBy(os => os.dataHoraTransf)
          .toArray()
          .map(os =>
          {
            var evento = os.agendaTecnico ?
              this.exibeEventoOSExistente(os) : this.criaNovoEventoOS(os, mediaTecnico, ultimoEvento);

            ultimoEvento = evento;
            return evento;
          }))

      }).toArray());

    this.validateEvents();
  }

  private exibeEventoOSExistente(os: OrdemServico): MbscAgendaTecnicoCalendarEvent
  {
    var agendaTecnico = os.agendaTecnico;
    var evento: MbscAgendaTecnicoCalendarEvent =
    {
      codAgendaTecnico: agendaTecnico.codAgendaTecnico,
      start: agendaTecnico.inicio,
      end: agendaTecnico.fim,
      ordemServico: os,
      title: os.localAtendimento?.nomeLocal.toUpperCase(),
      color: this.getInterventionColor(os.tipoIntervencao?.codTipoIntervencao),
      editable: true,
      resource: os.tecnico?.codTecnico,
    }

    return evento;
  }

  private criaNovoEventoOS(os: OrdemServico, mediaTecnico: number, ultimoEvento: MbscAgendaTecnicoCalendarEvent): MbscAgendaTecnicoCalendarEvent
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

    this._agendaTecnicoSvc.criar(agendaTecnico).subscribe(agendamento =>
    {
      evento.codAgendaTecnico = agendamento.codAgendaTecnico;
    });

    return evento;
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
      codStatusServicos: "1",
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

  private updateResourceChange(args): boolean
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
      this._osSvc.atualizar(ev.ordemServico).subscribe(
        r =>
        {
          return this.updateEvent(args);
        },
        e => 
        {
          return false;
        });
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

  private updateEvent(args)
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

    this._agendaTecnicoSvc.atualizar(agenda).subscribe(
      r => 
      {
        this._notify.toast({ message: 'Agendamento atualizado com sucesso.' });
        return true;
      },
      e =>
      {
        this._notify.toast({ message: 'Não foi possível atualizar o agendamento.' });
        return false;
      }).add(this.updateEventColor(args));
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

  private createNewEvent(args, inst)
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

    this._agendaTecnicoSvc.criar(agendaTecnico).subscribe(
      agendamento =>
      {
        ev.ordemServico.codTecnico = agendamento.codTecnico;
        ev.ordemServico.dataHoraTransf = agendamento.ultimaAtualizacao;
        ev.ordemServico.codAgendaTecnico = agendamento.codAgendaTecnico;
        ev.ordemServico.codStatusServico = 8;
        ev.ordemServico.statusServico.codStatusServico = 8;

        this._osSvc.atualizar(ev.ordemServico).subscribe(
          r =>
          {
            this._notify.toast({ message: 'Atendimento agendado com sucesso.' });
            return true;
          },
          e => 
          {
            this._notify.toast({ message: 'Não foi possível fazer o agendamento.' });
            this.deleteEvent(args, inst);
            return false;
          });
      },
      e =>
      {
        this._notify.toast({ message: 'Não foi possível fazer o agendamento.' });
        this.deleteEvent(args, inst);
        return false;
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

  public deleteEvent(args, inst)
  {
    if (args.event.ordemServico?.codAgendaTecnico)
      this._agendaTecnicoSvc.deletar(args.event.ordemServico?.codAgendaTecnico).subscribe(() =>
      {
        inst.removeEvent(args.event);
      })
    else inst.removeEvent(args.event);
  }

  public abrirMapa(codTecnico: number): void
  {
    const resource = this.resources.filter(r => r.id === codTecnico).shift();
    const chamados = this.chamados.filter(os => os.codTecnico === codTecnico);

    const dialogRef = this._dialog.open(RoteiroMapaComponent, {
      width: '960px',
      height: '640px',
      data: {
        resource: resource,
        chamados: chamados
      }
    });

    dialogRef.afterClosed().subscribe(() => { });
  }

  aplicar(): void
  {
    const form: any = this.form.getRawValue();

    const filtro: any = {
      nome: 'agenda-tecnico',
      parametros: form
    }

    this._userSvc.registrarFiltro(filtro);

    const newFilter: any = { nome: 'agenda-tecnico', parametros: this.form.getRawValue() }
    const oldFilter = this._userSvc.obterFiltro('agenda-tecnico');

    if (oldFilter != null)
      newFilter.parametros =
      {
        ...newFilter.parametros,
        ...oldFilter.parametros
      };

    this._userSvc.registrarFiltro(newFilter);
    this.sidenavFiltro.close();
  }

  limpar(): void
  {
    this.form.reset();
    this.aplicar();
    this.sidenavFiltro.close();
  }

  selectAll(select: AbstractControl, values, propertyName)
  {
    if (select.value[0] == 0 && propertyName != '')
      select.patchValue([...values.map(item => item[`${propertyName}`]), 0]);
    else if (select.value[0] == 0 && propertyName == '')
      select.patchValue([...values.map(item => item), 0]);
    else
      select.patchValue([]);
  }

  private carregarFiltro(): void
  {
    this.filtro = this._userSvc.obterFiltro('agenda-tecnico');
    if (!this.filtro)
    {
      return;
    }

    Object.keys(this.filtro?.parametros).forEach((key) =>
    {
      if (this.filtro?.parametros[key] instanceof Array)
      {
        this.filtro.parametros[key] = this.filtro.parametros[key].join()
      };
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
    return this.userSession.usuario?.codFilial?.toString() ?? "4";
  }

}