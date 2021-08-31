import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { GraficoOrdemServicoClienteComponent } from './grafico-ordem-servico-cliente/grafico-ordem-servico-cliente.component';
import { GraficoSLAComponent } from './grafico-sla/grafico-sla.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements AfterViewInit {
  @ViewChild(GraficoOrdemServicoClienteComponent) graficoOrdemServico: GraficoOrdemServicoClienteComponent;
  @ViewChild(GraficoSLAComponent) graficoSLA: GraficoSLAComponent;
  @ViewChild('sidenav') sidenav: MatSidenav;
  userSession: UsuarioSessao;
  filtro: any;

  constructor(
    private _userService: UserService,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
    this.filtro = this._userService.obterFiltro('dashboard');
  }

  async ngAfterViewInit() {
    this.sidenav.closedStart.subscribe(() => {
      this.filtro = this._userService.obterFiltro('dashboard');
    })
  }
}
