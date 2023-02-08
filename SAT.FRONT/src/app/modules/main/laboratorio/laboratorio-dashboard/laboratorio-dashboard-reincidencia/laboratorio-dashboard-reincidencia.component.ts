import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { DashboardLabService } from 'app/core/services/dashboard-lab.service';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexTitleSubtitle,
  ApexStroke,
  ApexGrid
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  dataLabels: ApexDataLabels;
  grid: ApexGrid;
  stroke: ApexStroke;
  title: ApexTitleSubtitle;
};

@Component({
  selector: 'app-laboratorio-dashboard-reincidencia',
  templateUrl: './laboratorio-dashboard-reincidencia.component.html'
})
export class LaboratorioDashboardReincidenciaComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  isLoading: boolean = true;

  constructor(
    private _dashboardLabService: DashboardLabService,
    private _cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.montarGrafico();
  }

  private async montarGrafico() {
    const data = await this._dashboardLabService.obterIndiceReincidencia({ }).toPromise();
    const labels = data.map(d => d.mesExtenso);
    const values = data.map(d => d.indiceReincidencia);

    this.chartOptions = {
      series: [
        {
          name: "Índice de Reincidência por Mês",
          data: values
        }
      ],
      chart: {
        height: 350,
        type: "line",
        zoom: {
          enabled: false
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: "straight"
      },
      grid: {
        row: {
          colors: ["#f3f3f3", "transparent"], 
          opacity: 0.5
        }
      },
      xaxis: {
        categories: labels,
        labels: {
          formatter: function(val) {
            return val + "";
          }
        }
      },
    };

    this.isLoading = false;
    this._cdr.detectChanges();
  }
}
