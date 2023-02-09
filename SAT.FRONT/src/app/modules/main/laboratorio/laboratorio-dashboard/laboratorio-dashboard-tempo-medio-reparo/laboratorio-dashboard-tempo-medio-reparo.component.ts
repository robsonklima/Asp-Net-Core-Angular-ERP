import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { DashboardLabService } from 'app/core/services/dashboard-lab.service';
import moment from 'moment';
import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexXAxis,
  ApexPlotOptions,
  ApexTooltip
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  xaxis: ApexXAxis;
  tooltip: ApexTooltip;
};

@Component({
  selector: 'app-laboratorio-dashboard-tempo-medio-reparo',
  templateUrl: './laboratorio-dashboard-tempo-medio-reparo.component.html'
})
export class LaboratorioDashboardTempoMedioReparoComponent implements OnInit {
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
    const data = await this._dashboardLabService
      .obterTempoMedioReparo({ sortActive: 'TempoMedioReparo', sortDirection: 'desc' }).toPromise();

    const labels = data.map(d => d.nomePecaAbrev);
    const temposMediosReparo = data.map(d => d.tempoMedioReparo);
    
    

    this.chartOptions = {
      series: [
        {
          name: "Tempo Médio de Reparo (min)",
          data: temposMediosReparo
        }
      ],
      chart: {
        type: "bar",
        height: 350
      },
      plotOptions: {
        bar: {
          horizontal: true
        }
      },
      dataLabels: {
        enabled: false
      },
      xaxis: {
        categories: labels,
        labels: {
          formatter: function(val) {
            return moment().startOf('day').add(val, 'minutes').format('hh:mm');
          }
        }
      },
      tooltip: {
        enabled: false
      },
    };

    this._cdr.detectChanges();
    this.isLoading = false;
  }
}
