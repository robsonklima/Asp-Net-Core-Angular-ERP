import { Component, Input, OnInit } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { UserService } from 'app/core/user/user.service';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardPecasCriticaChamadosFaltantes, ViewDashboardPecasCriticaEstoqueFaltantes, ViewDashboardPecasCriticasMaisFaltantes } from 'app/core/types/dashboard.types';

@Component({
  selector: 'app-pecas-faltantes-mais-criticas',
  templateUrl: './pecas-faltantes-mais-criticas.component.html',
  styleUrls: ['./pecas-faltantes-mais-criticas.component.css']
})
export class PecasFaltantesMaisCriticasComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  public loading: boolean = true;

  public index: number = 0;
  public topPecasCriticasFaltantes: ViewDashboardPecasCriticasMaisFaltantes[] = [];
  public topPecaCriticaAtual: ViewDashboardPecasCriticasMaisFaltantes;
  public topPecasCriticasChamados: ViewDashboardPecasCriticaChamadosFaltantes[] = [];
  public topPecasCriticasEstoque: ViewDashboardPecasCriticaEstoqueFaltantes[] = [];

  protected _onDestroy = new Subject<void>();

  constructor(
    private _dashboardService: DashboardService,
    protected _userService: UserService) {
    super(_userService, 'dashboard-filtro')
  }

  ngOnInit(): void {
    this.index = 0;
    this.obterDados();
    this.registerEmitters();
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  loadFilter(): void {
    super.loadFilter();
  }

  private async obterDados() {
    this.loading = true;

    this.topPecasCriticasFaltantes = (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.PECAS_CRITICAS_MAIS_FALTANTES }).toPromise())
      .viewDashboardPecasCriticasMaisFaltantes;

    interval(10000)
      .pipe(
        startWith(0),
        takeUntil(this._onDestroy)
      )
      .subscribe(async () => {
        this.loading = true;
        this.carregarProximaPeca();
        this.loading = false;
      });
  }

  private async carregarProximaPeca() {
    if (this.index >= this.topPecasCriticasFaltantes.length)
      this.index = 0;

    this.topPecaCriticaAtual = this.topPecasCriticasFaltantes[this.index];

    this.topPecasCriticasChamados = (await this._dashboardService.obterViewPorParametros({
      dashboardViewEnum: DashboardViewEnum.PECAS_CRITICAS_CHAMADOS_FALTANTES,
      codPeca: this.topPecaCriticaAtual.codPeca
    }).toPromise())
      .viewDashboardPecasCriticaChamadosFaltantes;

    this.topPecasCriticasEstoque = (await this._dashboardService.obterViewPorParametros({
      dashboardViewEnum: DashboardViewEnum.PECAS_CRITICAS_ESTOQUE_FALTANTES,
      codPeca: this.topPecaCriticaAtual.codPeca
    }).toPromise())
      .viewDashboardPecasCriticaEstoqueFaltantes;

    this.index++;
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
