import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import Enumerable from 'linq';
import { Filterable } from 'app/core/filters/filterable';
import { UserService } from 'app/core/user/user.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardIndicadoresFiliais } from 'app/core/types/dashboard.types';

@Component({
  selector: 'app-indicadores-filiais',
  templateUrl: './indicadores-filiais.component.html',
  styleUrls: ['./indicadores-filiais.component.css'
  ]
})

export class IndicadoresFiliaisComponent extends Filterable implements OnInit, AfterViewInit, IFilterable {
  public indicadoresFiliais: ViewDashboardIndicadoresFiliais[] = [];
  public indicadoresFiliaisTotal: ViewDashboardIndicadoresFiliais;
  public loading: boolean = true;

  @Input() sidenav: MatSidenav;

  constructor(
    private _dashboardService: DashboardService,
    protected _userService: UserService) {
    super(_userService, 'dashboard-filtro')
  }
  ngAfterViewInit(): void {
    this.registerEmitters();
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.indicadoresFiliais = [];
      this.loading = true;
    })
  }

  loadFilter(): void {
    super.loadFilter();
  }

  ngOnInit(): void {
    this.loading = true;
    this.montaDashboard();
  }

  private async montaDashboard(): Promise<void> {
    this.indicadoresFiliais =   (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.INDICADORES_FILIAL }).toPromise())
    .viewDashboardIndicadoresFiliais;
    this.indicadoresFiliaisTotal = Enumerable.from(this.indicadoresFiliais).firstOrDefault(q => q.filial == "TOTAL");
    this.indicadoresFiliais = Enumerable.from(this.indicadoresFiliais).where(f => f.filial != "TOTAL").toArray();
    this.indicadoresFiliais.sort((a, b) => (a.sla > b.sla ? -1 : 1));
    this.loading = false;
  }
}