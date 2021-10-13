import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { setOptions, localePtBR, Notifications, MbscEventcalendarOptions } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { AgendaTecnico, Coordenada, MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import moment from 'moment';
import Enumerable from 'linq';
import { HaversineService } from 'app/core/services/haversine.service';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';
import { fromEvent, interval, Subject } from 'rxjs';
import { MatSidenav } from '@angular/material/sidenav';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';

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
export class AgendaTecnicoComponent implements AfterViewInit {
    loading: boolean;
    tecnicos: Tecnico[] = [];
    events: MbscAgendaTecnicoCalendarEvent[] = [];
    resources = [];
    externalEvents = [];
    externalEventsFiltered = [];
    inicioExpediente = moment().set({ hour: 8, minute: 0, second: 0, millisecond: 0 });
    fimExpediente = moment().set({ hour: 18, minute: 0, second: 0, millisecond: 0 });
    inicioIntervalo = moment().set({ hour: 12, minute: 0, second: 0, millisecond: 0 });
    fimIntervalo = moment().set({ hour: 13, minute: 0, second: 0, millisecond: 0 });
    limiteIntervalo = moment().set({ hour: 14, minute: 0, second: 0, millisecond: 0 });

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
        onEventCreate: (args, inst) => {
            if (this.hasOverlap(args, inst)) {
                this._notify.toast({
                    message: 'Os atendimentos não podem se sobrepor.'
                });
                return false;
            }
            else if (this.invalidInsert(args)) {
                this._notify.toast({
                    message: 'O atendimento não pode ser agendado para antes da linha do tempo.'
                });
                return false;
            }

            const eventIndex = this.externalEventsFiltered.map(function (e) { return e.title; }).indexOf(args.event.title);
            this.externalEventsFiltered.splice(eventIndex, 1);
        },
        onEventUpdate: (args, inst) => {
            if (this.hasOverlap(args, inst)) {
                this._notify.toast({
                    message: 'Os atendimentos não podem se sobrepor.'
                });
                return false;
            }
            else if (this.hasChangedResource(args)) {
                this._notify.toast({
                    message: 'O atendimento não pode ser transferido para outro técnico.'
                });
                return false;
            }
            else if (this.invalidMove(args)) {
                this._notify.toast({
                    message: 'O atendimento não pode ser agendado para antes da linha do tempo.'
                });
                return false;
            }

            if (this.isTechnicianInterval(args) && this.invalidTechnicianInterval(args)) {
                this._notify.toast({
                    message: 'O intervalo deve ser feito até às 14h.'
                });
                return false;
            }

            if (!this.updateEvent(args))
            {
                this._notify.toast({
                    message: 'Não foi possível atualizar o agendamento.'
                });
                return false;
            }
            else
            {
                this._notify.toast({
                    message: 'Agendamento atualizado com sucesso.'
                });
                return true;
            }
        },
        onEventDoubleClick: (args, inst) =>
        {
            this.showOSInfo(args);
        }
    };

    @ViewChild('sidenav') sidenav: MatSidenav;
    @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
    protected _onDestroy = new Subject<void>();

    constructor(
        private _notify: Notifications,
        private _tecnicoSvc: TecnicoService,
        private _osSvc: OrdemServicoService,
        private _haversineSvc: HaversineService,
        private _cdr: ChangeDetectorRef,
        private _agendaTecnicoSvc: AgendaTecnicoService
    ) { }

    ngAfterViewInit(): void {
        interval(10 * 60 * 1000)
            .pipe(
                startWith(0),
                takeUntil(this._onDestroy)
            )
            .subscribe(() => {
                if (!this.sidenav.opened) {
                    this.carregaTecnicosEChamadosTransferidos();
                    this.carregaChamadosAbertos();
                }
            });

        fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
            map((event: any) => {
                return event.target.value;
            })
            , debounceTime(700)
            , distinctUntilChanged()
            ).subscribe((text: string) => {
                this.filtrarChamadosAbertos(text);
            });
    }

    private validateEvents(): void {
        var now = moment();
        Enumerable.from(this.events).where(e => e.ordemServico != null).forEach(e => {
            var end = moment(e.end);
            if (end < now) e.color = this.getStatusColor(e.ordemServico.statusServico?.codStatusServico);
        });
        this._cdr.detectChanges();
    }

    private async carregaTecnicosEChamadosTransferidos() {
        this.loading = true;

        const tecnicos = await this._tecnicoSvc.obterPorParametros({
            indAtivo: 1,
            codFilial: 4,
            codPerfil: 35,
            periodoMediaAtendInicio: moment().add(-7, 'days').format('yyyy-MM-DD 00:00'),
            periodoMediaAtendFim: moment().format('yyyy-MM-DD 23:59'),
            sortActive: 'nome',
            sortDirection: 'asc'
        }).toPromise();

        this.resources = tecnicos.items.map(tecnico => {
            return {
                id: tecnico.codTecnico,
                name: tecnico.nome,
                img: `https://sat.perto.com.br/DiretorioE/AppTecnicos/Fotos/${tecnico.usuario.codUsuario}.jpg`,
            }
        });

        const chamados = await this._osSvc.obterPorParametros({
            codFiliais: "4",
            //codStatusServicos: "8",
            dataTransfInicio: moment().add(-1, 'days').toISOString(),
            dataTransfFim:  moment().add(1, 'days').toISOString(),
            sortActive: 'dataHoraTransf',
            sortDirection: 'asc'
        }).toPromise();

        this.carregaDados(chamados.items, tecnicos.items);
        this.loading = false;
    }

    private carregaDados(chamados: OrdemServico[],  tecnicos: Tecnico[])
    {
        this.events = [];
        this.carregaSugestaoAlmoco(tecnicos);
        this.carregaEventos(chamados);
    }

    private carregaEventos(chamados: OrdemServico[]) 
    {
        this.events = this.events.concat(Enumerable.from(chamados)
            .where(os => os.tecnico != null)
            .groupBy(os => os.codTecnico).selectMany(osPorTecnico =>
        {
            var mediaTecnico = osPorTecnico.firstOrDefault().tecnico.mediaTempoAtendMin ?? 30;
            mediaTecnico = mediaTecnico > 0 ? mediaTecnico : 30;
            var ultimoEvento: MbscAgendaTecnicoCalendarEvent;
            return (Enumerable.from(osPorTecnico).orderBy(os => os.dataHoraTransf).toArray().map(os => 
            {
                 var evento: MbscAgendaTecnicoCalendarEvent;

                if (os.agendaTecnico.length > 0)
                {
                    evento = this.exibeEventoExistente(os);
                }
                else
                {
                    evento = this.criaNovoEvento(os, mediaTecnico, ultimoEvento);
                }

                ultimoEvento = evento;
                return evento;
            }))

        }).toArray());

        this.validateEvents();
    }

    private exibeEventoExistente(os: OrdemServico) : MbscAgendaTecnicoCalendarEvent
    {
        var agendaTecnico = os.agendaTecnico[0];
        var evento: MbscAgendaTecnicoCalendarEvent =
        {
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

    private criaNovoEvento(os: OrdemServico, mediaTecnico: number, ultimoEvento: MbscAgendaTecnicoCalendarEvent): MbscAgendaTecnicoCalendarEvent
    {
        var deslocamento = this.calculaDeslocamentoEmMinutos(os, ultimoEvento?.ordemServico);

        var start = moment(ultimoEvento != null ? ultimoEvento.end : this.inicioExpediente).add(deslocamento, 'minutes');

        // se começa durante a sugestão de intervalo ou deopis das 18h
        if (start.isBetween(this.inicioIntervalo, this.fimIntervalo)) {
            start = moment(this.fimIntervalo).add(deslocamento, 'minutes');
        }
        else if (start.hour() >= this.fimExpediente.hour()) {
            start = moment(this.inicioExpediente).add(1, 'day').add(deslocamento, 'minutes');
        }

        // se termina durante a sugestao de intervalo
        var end = moment(start).add(mediaTecnico, 'minutes');
        if (end.isBetween(this.inicioIntervalo, this.fimIntervalo)) {
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

    private async carregaSugestaoAlmoco(tecnicos: Tecnico[])
    {
        var intervalos = await this._agendaTecnicoSvc.obterPorParametros({
            tipo: "INTERVALO",
            codFiliais: "4",
            data: moment().toISOString()
        }).toPromise();

        this.events = this.events.concat(Enumerable.from(tecnicos).select(tecnico =>
        {
            var intervalo = Enumerable.from(intervalos.items).firstOrDefault(i => i.codTecnico == tecnico.codTecnico);
            if(intervalo == null)
            {
                return this.criaNovoIntervalo(tecnico);
            }
            else
            {
                return this.exibeIntervaloExistente(intervalo);
            }

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
            start: intervalo.inicio,
            end: intervalo.fim,
            title: intervalo.tipo,
            color: '#808080',
            editable: true,
            resource: intervalo.codTecnico,
        }

       return evento;
    }

    private async carregaChamadosAbertos() {
        const data = await this._osSvc.obterPorParametros({
            codStatusServicos: "1",
            codFiliais: "4"
        }).toPromise();

        this.externalEvents = data.items.map(os => {
            return {
                title: os.codOS.toString(),
                color: '#1064b0',
                start: moment(),
                end: moment().add(60, 'minutes')
            }
        });

        this.externalEventsFiltered = this.externalEvents;
    }

    private calculaDeslocamentoEmMinutos(os: OrdemServico, osAnterior: OrdemServico): number {
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

    private getInterventionColor(tipoIntervencao: number): string {
        switch (tipoIntervencao) {
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

    private getStatusColor(statusOS: number): string {
        switch (statusOS) {
            case 1: //aberto
                return "#ff4c4c";
            case 8: //transferido
                return "#ff4c4c";
            case 2: //cancelado
                return "#BFCAD0";
            case 3: //fechado
                return "#C5C5C5";
            default:
                return "#C5C5C5";
        }
    }

    public filtrarChamadosAbertos(query: string) {
        if (query && query.trim() != '') {
            this.externalEventsFiltered = this.externalEvents.filter((ev) => {
                return (
                    ev.title.toLowerCase().indexOf(query.toLowerCase()) > -1
                );
            })
        } else {
            this.externalEventsFiltered = this.externalEvents;
        }
    }

    private hasOverlap(args, inst) {
        var ev = args.event;
        var events = inst.getEvents(ev.start, ev.end).filter(e => e.resource == ev.resource && e.id != ev.id);
        return events.length > 0;
    }

    private hasChangedResource(args) {
        return args.event.resource != args.oldEvent.resource;
    }

    private isTechnicianInterval(args) {
        return args.event.title === "INTERVALO";
    }

    private invalidTechnicianInterval(args) {
        return moment(args.event.start) > this.limiteIntervalo;
    }

    private invalidMove(args) {
        //não pode mover evento posterior a linha do tempo para antes da linha do tempo
        var now = moment();
        return moment(args.oldEvent.start) > now && moment(args.event.start) < now;
    }

    private invalidInsert(args) {
        //não pode inserir evento anterior à linha do tempo
        var now = moment();
        return moment(args.event.start) < now;
    }

    // valida atualizaçaõ do evento no banco
    private async updateEvent(args)
    {
        var result: boolean;
        var evento = args.event.ordemServico.agendaTecnico[0];
        evento.ultimaAtualizacao = moment().format('yyyy-MM-DD HH:mm:ss');
        await this._agendaTecnicoSvc.atualizar(evento).subscribe(t => 
        {
            result =  true;
        },
        e => {
            result = false;
        });   

        return result;
    }

    private showOSInfo(args)
    {
        var os = args.event.ordemServico;

        if (os == null) return;

        var text = "";
        if(os.localAtendimento?.nomeLocal) text += 'Local Atendimento: ' + args.event.ordemServico.localAtendimento?.nomeLocal  + '\n';
        if(os.defeito) text += ', Defeito: ' + os.defeito + '\n';

        this._notify.alert(
            {
                title: "OS " + args.event.ordemServico.codOS.toString(),
                message: text,
                display: 'center'
            }
        );
    }
}