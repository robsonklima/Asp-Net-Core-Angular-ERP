import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { DashboardLabService } from 'app/core/services/dashboard-lab.service';
import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexXAxis,
  ApexPlotOptions,
  ApexStroke
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
};

@Component({
  selector: 'app-laboratorio-dashboard-produtividade-tecnica',
  templateUrl: './laboratorio-dashboard-produtividade-tecnica.component.html'
})
export class LaboratorioDashboardProdutividadeTecnicaComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  isLoading: boolean = true;

  constructor(
    private _dashboardLabService: DashboardLabService,
    private _cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.montarGrafico();
  }

  private async montarGrafico() {
    const data = await this._dashboardLabService
      .obterProdutividadeTecnica({ sortActive: 'Total', sortDirection: 'desc' }).toPromise();
    const labels = data.map(d => d.nomeUsuario);
    const eletronico = data.map(d => d.eletronico);
    const mecanico = data.map(d => d.mecanico);
    

    this.chartOptions = {
      series: [
        {
          name: "Eletrônico",
          data: eletronico
        },
        {
          name: "Mecânico",

          data: mecanico
        }
      ],
      chart: {
        type: "bar",
        height: 430
      },
      plotOptions: {
        bar: {
          horizontal: true,
          dataLabels: {
            position: "top"
          }
        }
      },
      dataLabels: {
        enabled: true,
        offsetX: -6,
        style: {
          fontSize: "12px",
          colors: ["#fff"]
        }
      },
      stroke: {
        show: true,
        width: 1,
        colors: ["#fff"]
      },
      xaxis: {
        categories: labels
      }
    };

    this.isLoading = false;
    this._cdr.detectChanges();
  }
}
