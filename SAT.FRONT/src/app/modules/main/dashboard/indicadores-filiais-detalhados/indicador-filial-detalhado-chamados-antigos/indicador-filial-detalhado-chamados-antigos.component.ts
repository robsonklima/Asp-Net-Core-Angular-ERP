import { Component, OnInit } from '@angular/core';
import { FuseAlertType } from '@fuse/components/alert';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardIndicadoresDetalhadosChamadosAntigos } from 'app/core/types/dashboard.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-indicador-filial-detalhado-chamados-antigos',
  templateUrl: './indicador-filial-detalhado-chamados-antigos.component.html',
  styles: [`tr:nth-child(odd) { background-color: rgb(239,245,254); }`]
})
export class IndicadorFilialDetalhadoChamadosAntigosComponent implements OnInit {
  loading: boolean = true;
  chamados: ViewDashboardIndicadoresDetalhadosChamadosAntigos[] = [];
  
  constructor(
    private _dashboardService: DashboardService,
  ) {}

  async ngOnInit() {
    const data = await this._dashboardService.obterViewPorParametros({
      dashboardViewEnum: DashboardViewEnum.INDICADORES_DETALHADOS_CHAMADOS_ANTIGOS,
      codFilial: 4
    }).toPromise();

    this.chamados = data.viewDashboardIndicadoresDetalhadosChamadosAntigos;
    this.loading = false;
  }
}
