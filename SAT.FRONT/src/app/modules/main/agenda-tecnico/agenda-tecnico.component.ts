import { Component, OnInit } from '@angular/core';
import { setOptions, MbscCalendarEvent, localePtBR, Notifications, MbscEventcalendarOptions, MbscEventcalendarView } from '@mobiscroll/angular';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Coordenada, MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
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
        dragToResize: false,
        dragToCreate: false,
        clickToCreate: false,
        onEventCreate: (args, inst) => 
        {
            if (this.hasOverlap(args, inst)) {
                this._notify.toast({
                    message: 'Os atendimentos não podem se sobrepor.'
                });
                return false;
            }

            const eventIndex = this.externalEvents.map(function(e) { return e.title; }).indexOf(args.event.title);
            this.externalEvents.splice(eventIndex, 1);
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

    view: MbscEventcalendarView = { };

    events: MbscAgendaTecnicoCalendarEvent[] = [];
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

        setInterval(function()
        {
             this.autoRefresh();
        }, 1000*60*5);
    }

    private validateEvents(): void
    {
        var now = moment();
        Enumerable.from(this.events).where(e => e.ordemServico != null).forEach(e =>
        {
            var end = moment(e.end);
            if (end < now) e.color = this.getStatusColor(e.ordemServico.statusServico?.codStatusServico);
        });
    }

    private autoRefresh(): void
    {
        this.validateEvents();
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
            // codStatusServicos: "8",
            dataTransfInicio: moment().add(-1, 'days').toISOString(),
            dataTransfFim:  moment().add(1, 'days').toISOString(),
            sortActive: 'dataHoraTransf',
            sortDirection: 'asc'
        }).toPromise();

        console.log(chamados);

        this.carregaSugestaoAlmoco(tecnicos.items);
        this.carregaEventos(chamados.items);
    }

    private carregaEventos(chamados: OrdemServico[])
    {
        this.events = this.events.concat(Enumerable.from(chamados).groupBy(os => os.codTecnico).selectMany(osPorTecnico =>
        {
            var mediaTecnico = 30;
            var ultimoEvento: MbscAgendaTecnicoCalendarEvent;

            return (Enumerable.from(osPorTecnico).orderBy(os => os.dataHoraTransf).toArray().map(os =>
            {
                var deslocamento = this.calculaDeslocamentoEmMinutos(os, ultimoEvento?.ordemServico);

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

                ultimoEvento = evento;
                return evento;
            }))
        }).toArray());

        this.validateEvents();
    }

    private carregaSugestaoAlmoco(tecnicos: Tecnico[])
    {
        this.events = this.events.concat(Enumerable.from(tecnicos).select(tecnico =>
        {
            var start = this.inicioIntervalo;
            var end = this.fimIntervalo;
            var evento: MbscAgendaTecnicoCalendarEvent = 
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

    private calculaDeslocamentoEmMinutos(os: OrdemServico, osAnterior: OrdemServico): number
    {
        var origem: Coordenada = new Coordenada();
        var destino: Coordenada = new Coordenada();

        // se ele já estava atendendo algum chamado, parte das coordenadas deste chamado
        if(osAnterior != null)
            origem.cordenadas = [osAnterior.localAtendimento?.latitude, osAnterior.localAtendimento?.longitude];
        // Se o técnico não possui nada agendado, parte do endereoç deste
        else 
            origem.cordenadas = [os.tecnico?.latitude, os.tecnico?.longitude];

        destino.cordenadas = [os.localAtendimento?.latitude, os.localAtendimento?.longitude]; 

        return this._haversineSvc.getDistanceInMinutesPerKm(origem, destino, 50);
    }

    private getInterventionColor(tipoIntervencao: number): string
    {
        switch(tipoIntervencao) 
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
        console.log(statusOS);
        switch(statusOS) 
        { 
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
}