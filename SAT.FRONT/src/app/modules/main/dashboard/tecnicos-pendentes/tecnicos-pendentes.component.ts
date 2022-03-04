import { Component, Input, OnInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardTecnicosPendentes } from 'app/core/types/dashboard.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-tecnicos-pendentes',
  templateUrl: './tecnicos-pendentes.component.html',
  styles: [`tr:nth-child(odd) { background-color: rgb(239,245,254);}`]
})
export class TecnicosPendentesComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  @Input() ordem: string;
  public pendenciaTecnicosModel: ViewDashboardTecnicosPendentes[] = [];
  public loading: boolean = true;

  constructor(private _dashboardService: DashboardService,
    protected _userService: UserService) {
    super(_userService, 'dashboard-filtro')
  }

  ngOnInit(): void {
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

    this.pendenciaTecnicosModel =
      this.ordem == 'asc' ?
        (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.PENDENCIA_TECNICOS_MENOS_PENDENCIA }).toPromise())
          .viewDashboardTecnicosMenosPendentes :
        (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.PENDENCIA_TECNICOS_MAIS_PENDENCIA }).toPromise())
          .viewDashboardTecnicosMaisPendentes;

    this.loading = false;
  }
}