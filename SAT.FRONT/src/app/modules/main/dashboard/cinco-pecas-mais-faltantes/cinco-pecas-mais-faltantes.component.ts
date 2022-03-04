import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardPecasMaisFaltantes } from 'app/core/types/dashboard.types';

@Component({
  selector: 'app-cinco-pecas-mais-faltantes',
  templateUrl: './cinco-pecas-mais-faltantes.component.html',
  styles: [`tr:nth-child(odd) { background-color: rgb(239,245,254); }`]
})

export class CincoPecasMaisFaltantesComponent implements OnInit {
  public loading: boolean = true;
  public topPecasFaltantes: ViewDashboardPecasMaisFaltantes[] = []

  constructor(private _dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {
    this.loading = true;
    this.topPecasFaltantes = (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.PECAS_MAIS_FALTANTES }).toPromise())
      .viewDashboardPecasMaisFaltantes;
    this.loading = false;
  }
}