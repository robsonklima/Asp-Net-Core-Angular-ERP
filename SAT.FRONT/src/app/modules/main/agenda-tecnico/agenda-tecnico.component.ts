import { Component, OnInit } from '@angular/core';
import { setOptions, MbscCalendarEvent, localePtBR, Notifications, MbscEventcalendarOptions, MbscEventcalendarView } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Coordenada } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import moment, { Moment } from 'moment';
import Enumerable from 'linq';
import { HaversineService } from 'app/core/services/haversine.service';

setOptions({
    locale: localePtBR,
    theme: 'windows',
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
export class AgendaTecnicoComponent implements OnInit {

    tecnicos: Tecnico[] = [];
    chamados: OrdemServico[] = [];

    constructor(
        private _notify: Notifications,
        private _tecnicoSvc: TecnicoService,
        private _osSvc: OrdemServicoService,
        private _haversineSvc: HaversineService
    ) { }

    calendarOptions: MbscEventcalendarOptions = {
        view: {
            timeline: {
                type: 'day',
                allDay: false,
                startDay: 1,
                startTime: '08:00',
                endTime: '19:00'
            }
        },
        dragToMove: true,
        externalDrop: true,
        onEventCreate: (args, inst) => 
        {
            if (this.hasOverlap(args, inst)) {
                this._notify.toast({
                    message: 'Os atendimentos não podem se sobrepor.'
                });
                return false;
            }
        },
        onEventUpdate: (args, inst) => 
        {
            if (this.hasOverlap(args, inst)) {
                this._notify.toast({
                    message: 'Os atendimentos não podem se sobrepor.'
                });
                return false;
            }
            else if (this.hasChangedResource(args, inst)) {
                this._notify.toast({
                    message: 'O atendimento não pode ser transferido para outro técnico.'
                });
                return false;
            }

            if (this.isInterval(args, inst) && this.invalidInterval(args, inst)) {
                this._notify.toast({
                    message: 'O intervalo deve ser feito até às 14h.'
                });
                return false;
            }
        }
    };

    hasOverlap(args, inst) 
    {
        var ev = args.event;
        var events = inst.getEvents(ev.start, ev.end).filter(e => e.resource == ev.resource && e.id != ev.id);
        return events.length > 0;
    }

    hasChangedResource(args, inst)
    {
        return args.event.resource != args.oldEvent.resource;
    }

    isInterval(args, inst)
    {
        return args.event.title === "INTERVALO";
    }

    invalidInterval(args, inst)
    {
        var newEventTime = moment(args.event.start);
        return moment(args.event.start) > this.limiteIntervalo;
    }

    view: MbscEventcalendarView = {
        
    };

    events: MbscCalendarEvent[] = [];
    resources = [];
    externalEvents = [];
    inicioExpediente = moment().set({hour:8,minute:0,second:0,millisecond:0});
    fimExpediente = moment().set({hour:18,minute:0,second:0,millisecond:0});
    inicioIntervalo = moment().set({hour:12,minute:0,second:0,millisecond:0});
    fimIntervalo = moment().set({hour:13,minute:0,second:0,millisecond:0});
    limiteIntervalo = moment().set({hour:14,minute:0,second:0,millisecond:0});

    retreatData = {
        title: 'Team retreat',
        color: '#1064b0',
        start: moment(),
        end: moment().add(60, 'minutes')
    };

    meetingData = {
        title: 'QA meeting',
        color: '#cf4343',
        start: moment(),
        end: moment().add(60, 'minutes')
    };

    ngOnInit(): void {
        this.obterTecnicosEChamadosTransferidos();
        this.obterChamadosAbertos();
    }

    private async obterTecnicosEChamadosTransferidos() {
        const tecnicos = await this._tecnicoSvc.obterPorParametros({
            indAtivo: 1,
            codFilial: 4,
            codPerfil: 35,
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
            codStatusServicos: "8",
            sortActive: 'dataHoraTransf',
            sortDirection: 'asc'
        }).toPromise();

        this.carregaSugestaoAlmoco(tecnicos.items);
        this.carregaEventos(chamados.items);
    }

    private carregaEventos(chamados: OrdemServico[])
    {
        this.events = this.events.concat(Enumerable.from(chamados).groupBy(os => os.codTecnico).selectMany(osPorTecnico =>
        {
            var mediaTecnico = 30;
            var ultimoEvento: MbscCalendarEvent;
            var ultimaOS: OrdemServico;

            return (Enumerable.from(osPorTecnico).orderBy(os => os.dataHoraTransf).toArray().map(os =>
            {
                var deslocamento = this.calculaDeslocamentoEmMinutos(os, ultimaOS);

                var start = moment(ultimoEvento != null ? ultimoEvento.end : this.inicioExpediente).add(deslocamento, 'minutes');

                // se começa durante a sugestão de intervalo ou deopis das 18h
                if (start.isBetween(this.inicioIntervalo, this.fimIntervalo))
                {
                    start = moment(this.fimIntervalo).add(deslocamento, 'minutes');
                }
                else if (start.hour() >= this.fimExpediente.hour())
                {
                    start = moment(this.inicioExpediente).add(1, 'day').add(deslocamento, 'minutes');
                }

                // se termina durante a sugestao de intervalo
                var end: Moment = moment(start).add(mediaTecnico, 'minutes');
                if (end.isBetween(this.inicioIntervalo, this.fimIntervalo))
                {
                    start = moment(this.fimIntervalo).add(deslocamento, 'minutes');
                    end = moment(start).add(mediaTecnico, 'minutes');
                }

                var evento: MbscCalendarEvent = 
                {
                    start: start,
                    end: end,
                    title: os.codOS.toString(),
                    color: '#388E3C',
                    editable: true,
                    resource: os.tecnico.codTecnico,
                }

                ultimoEvento = evento;
                ultimaOS = os;
                return evento;
            }))
        }).toArray());
    }

    private carregaSugestaoAlmoco(tecnicos: Tecnico[])
    {
        this.events = this.events.concat(Enumerable.from(tecnicos).select(tecnico =>
        {
            var start = this.inicioIntervalo;
            var end = this.fimIntervalo;
            var evento: MbscCalendarEvent = 
            {
                start:start,
                end: end,
                title: "INTERVALO",
                color: '#808080',
                editable: true,
                resource: tecnico.codTecnico,
            }
            return evento;
        }).toArray());
    }

    private async obterChamadosAbertos() {
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
        })
    }

    public onCellDoubleClick(event: any): void {
        this._notify.alert({
            title: 'Click',
            message: event.date + ' resource ' + event.resource
        });
    }

    public onEventCreated(event: any): void {
        const chamadoIndex = this.externalEvents.map(function(e) { return e.title; }).indexOf(event.event.title);

        this.externalEvents.splice(chamadoIndex, 1);
    }

    private calculaDeslocamentoEmMinutos(os: OrdemServico, osAnterior: OrdemServico): number
    {
        var origem: Coordenada = new Coordenada();
        var destino: Coordenada = new Coordenada();

        // se ele já estava atendendo algum chamado, parte das coordenadas deste chamado
        if(osAnterior != null)
            origem.cordenadas = [osAnterior.localAtendimento.latitude, osAnterior.localAtendimento.longitude];
        // Se o técnico não possui nada agendado, parte do endereoç deste
        else 
            origem.cordenadas = [os.tecnico.latitude, os.tecnico.longitude];

        destino.cordenadas = [os.localAtendimento.latitude, os.localAtendimento.longitude]; 

        return this._haversineSvc.getDistanceInMinutesPerKm(origem, destino, 50);
    }
}