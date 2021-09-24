import { Component, OnInit, ViewChild } from "@angular/core";
import { IndicadorService } from "app/core/services/indicador.service";
import { IndicadorAgrupadorEnum, IndicadorParameters, IndicadorTipoEnum } from "app/core/types/indicador.types";
import { UsuarioSessao } from "app/core/types/usuario.types";
import { UserService } from "app/core/user/user.service";
import moment from "moment";
import {
  ApexChart,
  ApexAxisChartSeries,
  ChartComponent,
  ApexDataLabels,
  ApexPlotOptions,
  ApexYAxis,
  ApexLegend,
  ApexGrid,
  ApexStates
} from "ng-apexcharts";

export type ChartOptions = 
{
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: any;
  grid: ApexGrid;
  colors: string[];
  legend: ApexLegend;
  states: ApexStates;
};

@Component({
  selector: 'app-dashboard-spa',
  templateUrl: './dashboard-spa.component.html'
})
export class DashboardSpaComponent implements OnInit 
{
  @ViewChild("chart") chart: ChartComponent;
  usuarioSessao: UsuarioSessao;
  public chartOptions: Partial<ChartOptions>;
  isLoading: boolean;
  haveData: boolean;

  constructor(
    private _indicadorService: IndicadorService,
    private _userService: UserService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }
  ngOnInit(): void 
  {
    this.carregarGrafico();
  }

  public async carregarGrafico() 
  {
    this.isLoading = true;
    const params: IndicadorParameters = 
    {
        agrupador: IndicadorAgrupadorEnum.FILIAL,
        tipo: IndicadorTipoEnum.SPA,
        codTiposIntervencao: "1,2,3,4,6,7",
        codAutorizadas: "8, 10, 13, 40, 48, 102, 108, 119, 130, 132, 141, 169, 172, 177, 178, 182, 183, 189, 190, 191, 192, 202",
        codTiposGrupo: "1,3,5,7,8,9,10,11",
        //dataInicio: moment().startOf('month').toISOString(),
        //dataFim: moment().endOf('month').toISOString()
        dataInicio: moment().subtract(1, 'year').startOf('month').toISOString(),
        dataFim: moment().subtract(1, 'year').endOf('month').toISOString(),
    }

    let data = await this._indicadorService.obterPorParametros(params).toPromise();
    
    if (data?.length) 
    {
      data = data.sort((a,b) => (a.valor > b.valor) ? 1 : ((b.valor > a.valor) ? -1 : 0));
      const labels = data.map(d => d.label);
      const valores = data.map(d => d.valor);
      const cores = data.map(d => d.valor < 85 ?  "#cc0000" : "#009900" );
      this.haveData = true;

      this.inicializarGrafico(labels, valores, cores);
    } 

    this.isLoading = false;
  }

  private inicializarGrafico(labels: string[], valores: number[], cores: string[]) 
  {
    this.chartOptions = 
    {
        series: [{
          name: "Percentual",
          data: valores
        }],
      chart: {
        height: 350,
        type: "bar"
      },
      colors: cores,
      plotOptions: 
      {
        bar: 
        {
          columnWidth: "45%",
          distributed: true
        }
      },
      states: 
      {
        active:
         {
          filter: 
          {
            type: "none"
          }
        }
      },
      dataLabels: 
      {
        enabled: false
      },
      legend: 
      {
        show: false
      },
      grid: 
      {
        show: true
      },
      xaxis: 
      {
        categories: labels,
        labels:
        {
          style: 
          {
            colors: cores,
            fontSize: "12px"
          }
        }
      },
      yaxis: 
      {
        max: 100,
        labels: 
        {
          formatter: (value) => 
          {
            return (value + "%").replace('.', ',');
          }
        }
      }
    };
  }
}