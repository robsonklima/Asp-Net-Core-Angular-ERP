import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { TicketService } from 'app/core/services/ticket.service';
import { Ticket, TicketBacklogView, ticketClassificacaoConst, ticketStatusConst } from 'app/core/types/ticket.types';
import Enumerable from 'linq';
import moment from 'moment';

import {
  ApexAxisChartSeries,
  ApexChart, ApexDataLabels, ApexFill, ApexLegend, ApexNonAxisChartSeries, ApexPlotOptions,
  ApexResponsive, ApexTheme, ApexTitleSubtitle, ApexXAxis, ChartComponent
} from "ng-apexcharts";
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  title: ApexTitleSubtitle;
  responsive: ApexResponsive[];
  xaxis: ApexXAxis;
  legend: ApexLegend;
  fill: ApexFill;
  labels: any;
  theme: ApexTheme;
};

export type ChartPieOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
  theme: ApexTheme;
  title: ApexTitleSubtitle;
};

@Component({
  selector: 'app-ticket-graficos',
  templateUrl: './ticket-graficos.component.html'
})
export class TicketGraficosComponent implements AfterViewInit {
  @ViewChild("backlogChart") backlogChart: ChartComponent;
  public backlogChartOptions: Partial<ChartOptions>;
  public moduloChartOptions: Partial<ChartOptions>;
  public classificacaoChartOptions: Partial<ChartPieOptions>;
  public usuarioChartOptions: Partial<ChartOptions>;
  public perfilChartOptions: Partial<ChartOptions>;
  public usuarios: any;
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

  private async obterDados() {
    this.tickets = (await this._ticketService.obterPorParametros({
      dataHoraCadInicio: moment().startOf('year').format('MM/DD/YYYY')
    }).toPromise()).items;

    this.obterSumario();
    this.montarGraficoBacklog();
    this.montarGraficoModulo();
    this.montarGraficoClassificacao();
    this.montarGraficoUsuario();
    this.montarGraficoPerfil();
    
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  async montarGraficoBacklog(periodo: number = 1) {
    this.backlog = await this._ticketService.obterBacklog({
      dataHoraCadInicio: this.obterInicioPeriodo(periodo),
      dataHoraCadFim: this.obterFimPeriodo(periodo)
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

  async montarGraficoModulo(periodo:number=1) {
    const tickets = this.tickets
      .filter(t => moment(t.dataHoraCad) >= moment(this.obterInicioPeriodo(periodo)))
      .filter(t => moment(t.dataHoraCad) <= moment(this.obterFimPeriodo(periodo)));

    const modulos = Enumerable.from(tickets)
      .groupBy(t => t.ticketModulo.descricao)
      .select(s => { return { 
        x: s.key(), 
        y: tickets.filter(t => t.ticketModulo.descricao == s.key()).length } })
      .toArray();

    this.moduloChartOptions = {
      series: [
        {
          data: modulos
        }
      ],
      legend: {
        show: false,
      },
      chart: {
        height: 350,
        type: "treemap",
        toolbar: {
          show: false
        },
      }
    };
  }

  async montarGraficoPerfil(periodo:number=1) {
    const tickets = this.tickets
      .filter(t => moment(t.dataHoraCad) >= moment(this.obterInicioPeriodo(periodo)))
      .filter(t => moment(t.dataHoraCad) <= moment(this.obterFimPeriodo(periodo)));

    const perfis = Enumerable.from(tickets)
      .groupBy(t => t.usuarioCad.perfil.nomePerfil)
      .select(s => { return { 
        x: s.key(), 
        y: tickets.filter(t => t.usuarioCad.perfil.nomePerfil == s.key()).length } })
      .toArray()
      .filter(g => g.y >= 3);

    this.perfilChartOptions = {
      series: [
        {
          data: perfis
        }
      ],
      legend: {
        show: false,
      },
      chart: {
        height: 350,
        type: "treemap",
        toolbar: {
          show: false
        },
      },
      plotOptions: {
        treemap: {
          enableShades: true,
          shadeIntensity: 0.5,
          reverseNegativeShade: true,
          colorScale: {
            ranges: [
              {
                from: -6,
                to: 0,
                color: "#FFA000"
              },
              {
                from: 0.001,
                to: 6,
                color: "#D32F2F"
              }
            ]
          }
        }
      }
    };
  }

  async montarGraficoClassificacao(periodo:number=1) {
    const tickets = this.tickets
      .filter(t => moment(t.dataHoraCad) >= moment(this.obterInicioPeriodo(periodo)))
      .filter(t => moment(t.dataHoraCad) <= moment(this.obterFimPeriodo(periodo)));

    const labels = Enumerable.from(tickets)
      .groupBy(t => t.ticketClassificacao.descricao)
      .select(s => { return s.key() })
      .toArray();

    const values = Enumerable.from(tickets)
      .groupBy(t => t.ticketClassificacao.descricao)
      .select(s => { return tickets.filter(t => t.ticketClassificacao.descricao == s.key()).length })
      .toArray();

    this.classificacaoChartOptions = {
      series: values,
      chart: {
        width: "100%",
        height: "360",
        type: "pie"
      },
      labels: labels,
      theme: {
        monochrome: {
          enabled: false
        }
      },
      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              position: "bottom"
            }
          }
        }
      ]
    };
  }

  async montarGraficoUsuario(periodo:number=1) {
    const tickets = this.tickets
      .filter(t => moment(t.dataHoraCad) >= moment(this.obterInicioPeriodo(periodo)))
      .filter(t => moment(t.dataHoraCad) <= moment(this.obterFimPeriodo(periodo)));

    const modulos = Enumerable.from(tickets)
      .groupBy(t => t.usuarioCad.nomeUsuario)
      .select(s => { return { 
        x: s.key(), 
        y: tickets.filter(t => t.usuarioCad.nomeUsuario == s.key()).length } })
      .toArray()
      .filter(g => g.y >= 3);

    this.usuarioChartOptions = {
      series: [{ data: modulos }],
      legend: { show: false },
      chart: {
        height: 350,
        type: "treemap",
        toolbar: {
          show: false
        },
      },
      plotOptions: {
        treemap: {
          enableShades: true,
          shadeIntensity: 0.5,
          reverseNegativeShade: true,
          colorScale: {
            ranges: [
              {
                from: -6,
                to: 0,
                color: "#E64A19"
              },
              {
                from: 0.001,
                to: 6,
                color: "#00796B"
              }
            ]
          }
        }
      }
    };
  }

  private obterInicioPeriodo(periodo: number): string {
    switch (periodo)
    {
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

  private obterFimPeriodo(periodo: number): string {
    switch (periodo)
    {
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

  private obterSumario() {
    const hoje = moment().startOf('day');
    const ontem = moment().subtract(1, 'days').startOf('day');
    const pendencias = this.tickets.filter(t => t.codStatus != ticketStatusConst.CONCLUIDO && 
                                                t.codStatus != ticketStatusConst.CANCELADO && 
                                                t.codStatus != ticketStatusConst.PENDENTE_SOLICITANTE).length;
    const emAtendimento = this.tickets.filter(t => t.codStatus == ticketStatusConst.EM_ATENDIMENTO).length;
    const pendenteSolicitante = this.tickets.filter(t => t.codStatus == ticketStatusConst.PENDENTE_SOLICITANTE).length;                  
    const antigosSemana = this.tickets.filter(t => t.codStatus == ticketStatusConst.AGUARDANDO && 
                                                   moment(t.dataHoraCad) <= moment().subtract(7, 'd')).length;
    const antigosMes = this.tickets.filter(t => t.codStatus == ticketStatusConst.AGUARDANDO && 
                                                moment(t.dataHoraCad) <= moment().subtract(30, 'd')).length;
    const novosHoje = this.tickets.filter(t => moment(t.dataHoraCad).isSame(hoje, 'd')).length;
    const novosOntem = this.tickets.filter(t => moment(t.dataHoraCad).isSame(ontem, 'd')).length;
    const melhorias = this.tickets.filter(t => t.codClassificacao == ticketClassificacaoConst.MELHORIA).length;
    const melhoriasAtendidas = this.tickets.filter(t => t.codStatus == ticketStatusConst.CONCLUIDO && 
                                                        t.codClassificacao == ticketClassificacaoConst.MELHORIA).length;
    const tempoMedioConclusaoHoras = (this.tickets.filter(t => t.codStatus == ticketStatusConst.CONCLUIDO).reduce((sum, ticket) => {
                                        return sum + moment(ticket.dataHoraFechamento).diff(moment(ticket.dataHoraCad), "hour");
                                      }, 0) / this.tickets.filter(t => t.codStatus == ticketStatusConst.CONCLUIDO).length).toFixed(0);

    this.sumario = {
      totais: {
        pendencias: pendencias,
        emAtendimento: emAtendimento,
        pendenteSolicitante: pendenteSolicitante,
        antigosSemana: antigosSemana,
        antigosMes: antigosMes,
        novosHoje: novosHoje,
        novosOntem: novosOntem,
        melhorias: melhorias,
        melhoriasAtendidas: melhoriasAtendidas,
        tempoMedioConclusaoHoras: tempoMedioConclusaoHoras
      }
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
