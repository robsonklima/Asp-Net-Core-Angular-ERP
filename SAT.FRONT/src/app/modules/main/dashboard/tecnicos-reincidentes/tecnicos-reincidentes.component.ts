import { Component, Input, OnInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardReincidenciaTecnicosReincidentes } from 'app/core/types/dashboard.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-tecnicos-reincidentes',
  templateUrl: './tecnicos-reincidentes.component.html'
})
export class TecnicosReincidentesComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  @Input() ordem: string;
  public reincidenciaTecnicosModel: ViewDashboardReincidenciaTecnicosReincidentes[] = [];
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
    this.reincidenciaTecnicosModel =
      this.ordem == 'asc' ?
        (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.REINCIDENCIA_TECNICOS_MENOS_REINCIDENTES }).toPromise())
          .viewDashboardReincidenciaTecnicosMenosReincidentes :
        (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.REINCIDENCIA_TECNICOS_MAIS_REINCIDENTES }).toPromise())
          .viewDashboardReincidenciaTecnicosMaisReincidentes;

    this.loading = false;
  }
}