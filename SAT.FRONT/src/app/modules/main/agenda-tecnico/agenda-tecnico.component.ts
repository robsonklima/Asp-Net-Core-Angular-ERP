import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { setOptions, localePtBR, Notifications, MbscEventcalendarOptions } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { AgendaTecnico, Coordenada, MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import moment from 'moment';
import Enumerable from 'linq';
import { HaversineService } from 'app/core/services/haversine.service';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';
import { fromEvent, interval, Subject } from 'rxjs';
import { MatSidenav } from '@angular/material/sidenav';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { MatDialog } from '@angular/material/dialog';
import { RoteiroMapaComponent } from './roteiro-mapa/roteiro-mapa.component';
import { Router } from '@angular/router';

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
export class AgendaTecnicoComponent implements AfterViewInit
{
  loading: boolean;
  tecnicos: Tecnico[] = [];
  events: MbscAgendaTecnicoCalendarEvent[] = [];
  chamados: OrdemServico[] = [];
  resources = [];
  externalEvents: MbscAgendaTecnicoCalendarEvent = [];
  externalEventsFiltered: MbscAgendaTecnicoCalendarEvent = [];
  inicioExpediente = moment().set({ hour: 8, minute: 0, second: 0, millisecond: 0 });
  fimExpediente = moment().set({ hour: 18, minute: 0, second: 0, millisecond: 0 });
  inicioIntervalo = moment().set({ hour: 12, minute: 0, second: 0, millisecond: 0 });
  fimIntervalo = moment().set({ hour: 13, minute: 0, second: 0, millisecond: 0 });
  limiteSuperiorIntervalo = moment().set({ hour: 14, minute: 0, second: 0, millisecond: 0 });
  limiteInferiorIntervalo = moment().set({ hour: 11, minute: 0, second: 0, millisecond: 0 });

  calendarOptions: MbscEventcalendarOptions = {
    view: {
      timeline: {
        type: 'day',
        allDay: false,
        startDay: 1,
        startTime: '07:00',
        endTime: '24:00'
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

      this.createNewEvent(args);
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

      if (this.isTechnicianInterval(args) && this.invalidTechnicianInterval(args))
      {
        this._notify.toast({
          message: 'O intervalo deve ser feito entre 11h e 14h.'
        });
        return false;
      }

      return this.updateEvent(args);
    },
    onEventDoubleClick: (args, inst) =>
    {
      this.showOSInfo(args);
    }
  };

  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _notify: Notifications,
    private _router: Router,
    private _tecnicoSvc: TecnicoService,
    private _osSvc: OrdemServicoService,
    private _haversineSvc: HaversineService,
    private _cdr: ChangeDetectorRef,
    private _agendaTecnicoSvc: AgendaTecnicoService,
    public _dialog: MatDialog
  ) { }

  ngAfterViewInit(): void
  {
    interval(10 * 60 * 1000)
      .pipe(
        startWith(0),
        takeUntil(this._onDestroy)
      )
      .subscribe(() =>
      {
        if (!this.sidenav.opened)
        {
          this.carregaTecnicosEChamadosTransferidos();
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

  private validateEvents(): void
  {
    var now = moment();
    Enumerable.from(this.events).where(e => e.ordemServico != null).forEach(e =>
    {
      var end = moment(e.end);
      if (end < now) 
      {
        e.color = this.getStatusColor(e.ordemServico.statusServico?.codStatusServico);
        if (e.ordemServico.statusServico.codStatusServico == 3) e.editable = false;
      }
    });
    this._cdr.detectChanges();
  }

  private async carregaTecnicosEChamadosTransferidos()
  {
    this.loading = true;

    const tecnicos = await this._tecnicoSvc.obterPorParametros({
      indAtivo: 1,
      codFiliais: "4",
      codPerfil: 35,
      periodoMediaAtendInicio: moment().add(-7, 'days').format('yyyy-MM-DD 00:00'),
      periodoMediaAtendFim: moment().format('yyyy-MM-DD 23:59'),
      sortActive: 'nome',
      sortDirection: 'asc'
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
      codFiliais: "4",
      codStatusServicos: "1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16",
      include: OrdemServicoIncludeEnum.OS_AGENDA,
      dataTransfInicio: moment().add(-1, 'days').toISOString(),
      dataTransfFim: moment().add(1, 'days').toISOString(),
      sortActive: 'dataHoraTransf',
      sortDirection: 'asc'
    }).toPromise()).items;

    const intervalos = await this._agendaTecnicoSvc.obterPorParametros({
      tipo: "INTERVALO",
      codFiliais: "4",
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
      title: os.codOS.toString(),
      color: this.getInterventionColor(os.tipoIntervencao?.codTipoIntervencao),
      editable: true,
      resource: os.tecnico?.codTecnico,
    }

    return evento;
  }

  private criaNovoEventoOS(os: OrdemServico, mediaTecnico: number, ultimoEvento: MbscAgendaTecnicoCalendarEvent): MbscAgendaTecnicoCalendarEvent
  {
    var deslocamento = this.calculaDeslocamentoEmMinutos(os, ultimoEvento?.ordemServico);

    var start = moment(ultimoEvento != null ? ultimoEvento.end : this.inicioExpediente).add(deslocamento, 'minutes');

    // se começa durante a sugestão de intervalo ou deopis das 18h
    if (start.isBetween(this.inicioIntervalo, this.fimIntervalo))
      start = moment(this.fimIntervalo).add(deslocamento, 'minutes');
    else if (start.hour() >= this.fimExpediente.hour())
      start = moment(this.inicioExpediente).add(1, 'day').add(deslocamento, 'minutes');

    // se termina durante a sugestao de intervalo
    var end = moment(start).add(mediaTecnico, 'minutes');
    if (end.isBetween(this.inicioIntervalo, this.fimIntervalo))
    {
      start = moment(this.fimIntervalo).add(deslocamento, 'minutes');
      end = moment(start).add(mediaTecnico || 30, 'minutes');
    }

    var evento: MbscAgendaTecnicoCalendarEvent =
    {
      start: start,
      end: end,
      ordemServico: os,
      title: os.codOS.toString(),
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
    var start = this.inicioIntervalo;
    var end = this.fimIntervalo;

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
      codFiliais: "4"
    }).toPromise();

    this.externalEvents = data.items.map(os =>
    {
      return {
        title: os.codOS.toString(),
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
      default:
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
          ev.title.toLowerCase().indexOf(query.toLowerCase()) > -1
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

  private invalidTechnicianInterval(args)
  {
    return moment(args.event.start) > this.limiteSuperiorIntervalo || moment(args.event.start) < this.limiteInferiorIntervalo;
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

  // valida atualizaçaõ do evento no banco
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

  private createNewEvent(args)
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
            return false;
          });
      },
      e =>
      {
        this._notify.toast({ message: 'Não foi possível fazer o agendamento.' });
        return false;
      });
  }

  private showOSInfo(args)
  {
    var os = args.event.ordemServico;

    if (os == null) return;

    var text = "";
    if (os.localAtendimento?.nomeLocal) text += 'Local Atendimento: ' + args.event.ordemServico.localAtendimento?.nomeLocal + '\n';
    if (os.defeito) text += ', Defeito: ' + os.defeito + '\n';

    this._notify.alert(
      {
        title: "OS " + args.event.ordemServico.codOS.toString(),
        message: text,
        display: 'center'
      }
    );
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

    dialogRef.afterClosed().subscribe(() =>
    {

    });
  }
}