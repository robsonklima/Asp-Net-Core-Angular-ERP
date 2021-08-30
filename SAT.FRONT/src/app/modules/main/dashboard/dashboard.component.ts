import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { GraficoOrdemServicoComponent } from './grafico-ordem-servico/grafico-ordem-servico.component';
import { GraficoSLAComponent } from './grafico-sla/grafico-sla.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements AfterViewInit {
  @ViewChild(GraficoOrdemServicoComponent) graficoOrdemServico: GraficoOrdemServicoComponent;
  @ViewChild(GraficoSLAComponent) graficoSLA: GraficoSLAComponent;

  @ViewChild('sidenav') sidenav: MatSidenav;
  userSession: UsuarioSessao;
  filtro: any;

  constructor(
    private _userService: UserService,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
    this.sidenav.closedStart.subscribe(() => {
      this.graficoOrdemServico.carregarFiltro();
      this.graficoOrdemServico.carregarGrafico();

      this.graficoSLA.carregarFiltro();
      this.graficoSLA.carregarGrafico();
    })
  }
}
