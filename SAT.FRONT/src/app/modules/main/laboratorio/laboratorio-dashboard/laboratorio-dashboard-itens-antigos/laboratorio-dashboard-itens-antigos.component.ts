import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { DashboardLabService } from 'app/core/services/dashboard-lab.service';
import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexXAxis,
  ApexPlotOptions
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  xaxis: ApexXAxis;
};

@Component({
  selector: 'app-laboratorio-dashboard-itens-antigos',
  templateUrl: './laboratorio-dashboard-itens-antigos.component.html'
})
export class LaboratorioDashboardItensAntigosComponent implements OnInit {
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
      .obterTopItensMaisAntigos({ sortActive: 'Qtd', sortDirection: 'desc' }).toPromise();

    const labels = data.map(d => d.nomeAbrev);
    const values = data.map(d => d.qtd);

    this.chartOptions = {
      series: [
        {
          name: "Itens Mais Antigos",
          data: values
        }
      ],
      chart: {
        type: "bar",
        height: 600
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
        categories: labels
      }
    };

    this._cdr.detectChanges();
    this.isLoading = false;
  }
}
