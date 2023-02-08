import { Component, OnInit, ViewChild } from '@angular/core';
import { DashboardLabService } from 'app/core/services/dashboard-lab.service';

@Component({
  selector: 'app-laboratorio-dashboard-faltantes-pendentes-reparados',
  templateUrl: './laboratorio-dashboard-faltantes-pendentes-reparados.component.html'
})
export class LaboratorioDashboardFaltantesPendentesReparadosComponent implements OnInit {
  loading: boolean = true;

  constructor(
    private _dashboardLabService: DashboardLabService
  ) { }

  ngOnInit(): void {
    this.montarGrafico();
  }

  private async montarGrafico() {
    

    this.loading = false;
  }
}
