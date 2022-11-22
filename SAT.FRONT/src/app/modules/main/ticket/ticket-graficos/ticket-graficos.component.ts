import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { TicketService } from 'app/core/services/ticket.service';
import { Ticket, TicketBacklogView, ticketClassificacaoConst, ticketStatusConst } from 'app/core/types/ticket.types';
import _ from 'lodash';
import moment from 'moment';

import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexPlotOptions,
  ApexResponsive,
  ApexXAxis,
  ApexLegend,
  ApexFill
} from "ng-apexcharts";
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  responsive: ApexResponsive[];
  xaxis: ApexXAxis;
  legend: ApexLegend;
  fill: ApexFill;
};

@Component({
  selector: 'app-ticket-graficos',
  templateUrl: './ticket-graficos.component.html'
})
export class TicketGraficosComponent implements AfterViewInit {
  @ViewChild("backlogChart") backlogChart: ChartComponent;
  public backlogChartOptions: Partial<ChartOptions>;
  tickets: Ticket[] = [];
  sumario: any = {};
  backlog: TicketBacklogView[] = [];
  isLoading: boolean = true;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _ticketService: TicketService,
    private _cdr: ChangeDetectorRef
  ) { }

  ngAfterViewInit(): void {
    interval(3 * 60 * 1000)
			.pipe(
				startWith(0),
				takeUntil(this._onDestroy)
			)
			.subscribe(() => {
				this.obterDados();
			});
  }

  async montarGraficoBacklog(tipo: number=1) {
    this.backlog = await this._ticketService.obterBacklog({
      dataHoraCadInicio: this.obterInicioPeriodo(tipo),
      dataHoraCadFim: this.obterFimPeriodo(tipo)
    }).toPromise();

    const datas = this.backlog.map(b => { return moment(b.data).format('DD/MM') });
    const abertos = this.backlog.map(b => { return +b.abertos });
    const fechados = this.backlog.map(b => { return +b.fechados });
    const backlogs = this.backlog.map(b => { return +b.backlog });

    this.backlogChartOptions = {
      series: [
        {
          name: "Abertos",
          data: abertos
        },
        {
          name: "Fechados",
          data: fechados
        },
        {
          name: "Backlog",
          data: backlogs
        }
      ],
      chart: {
        type: "bar",
        height: 350,
        stacked: true,
        toolbar: {
          show: false
        },
        zoom: {
          enabled: true
        }
      },
      responsive: [
        {
          breakpoint: 480,
          options: {
            legend: {
              position: "bottom",
              offsetX: -10,
              offsetY: 0
            }
          }
        }
      ],
      plotOptions: {
        bar: {
          horizontal: false
        }
      },
      xaxis: {
        type: "category",
        categories: datas
      },
      legend: {
        show: false,
      },
      fill: {
        opacity: 1
      }
    };
  }

  private obterInicioPeriodo(tipo: number): string {
    switch (tipo) {
      case 1:
        return moment().startOf('month').format('YYYY-MM-DD');
      case 2:
        return moment().startOf('week').format('YYYY-MM-DD');
      case 3:
        return moment().subtract(1, 'month').startOf('month').format('YYYY-MM-DD');
      case 4:
        return moment().subtract(1, 'week').startOf('week').format('YYYY-MM-DD');
      default:
        return moment().startOf('month').format('YYYY-MM-DD');
    }
  }

  private obterFimPeriodo(tipo: number): string {
    switch (tipo) {
      case 1:
        return moment().endOf('month').format('YYYY-MM-DD');
      case 2:
        return moment().endOf('week').format('YYYY-MM-DD');
      case 3:
        return moment().subtract(1, 'month').endOf('month').format('YYYY-MM-DD');
      case 4:
        return moment().subtract(1, 'week').endOf('week').format('YYYY-MM-DD');
      default:
        return moment().endOf('month').format('YYYY-MM-DD');
    }
  }

  private async obterDados() {
    const data = await this._ticketService.obterPorParametros({}).toPromise();
    this.tickets = data.items;
    this.obterSumario();
    this.montarGraficoBacklog();
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  private obterSumario() {
    const today = moment().startOf('day');
    const yesterday = moment().subtract(1, 'days').startOf('day');

    this.sumario = {
      totais: {
        pendencias: this.tickets
          .filter(t => t.codStatus == ticketStatusConst.AGUARDANDO).length,
        concluidos: this.tickets
          .filter(t => t.codStatus == ticketStatusConst.CONCLUIDO).length,
        emAtendimento: this.tickets
          .filter(t => t.codStatus == ticketStatusConst.EM_ATENDIMENTO).length,
        antigosSemana: this.tickets
          .filter(t => t.codStatus == ticketStatusConst.AGUARDANDO && moment(t.dataHoraCad) <= moment().subtract(7, 'd')).length,
        antigosMes: this.tickets
          .filter(t => t.codStatus == ticketStatusConst.AGUARDANDO && moment(t.dataHoraCad) <= moment().subtract(30, 'd')).length,
        novosHoje: this.tickets
          .filter(t => moment(t.dataHoraCad).isSame(today, 'd')).length,
        novosOntem: this.tickets
          .filter(t => moment(t.dataHoraCad).isSame(yesterday, 'd')).length,
        melhorias: this.tickets
          .filter(t => t.codClassificacao == ticketClassificacaoConst.MELHORIA).length,
        melhoriasAtendidas: this.tickets
          .filter(t => t.codStatus == ticketStatusConst.CONCLUIDO && t.codClassificacao == ticketClassificacaoConst.MELHORIA).length
      }
    }
  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
