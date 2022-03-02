import { Component, Inject, Input, OnInit } from '@angular/core';
import { LOCALE_ID } from '@angular/core';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { DashboardViewEnum, ViewDashboardEquipamentosMaisReincidentes } from 'app/core/types/dashboard.types';
import { DashboardService } from 'app/core/services/dashboard.service';

@Component({
  selector: 'app-equipamentos-mais-reincidentes',
  templateUrl: './equipamentos-mais-reincidentes.component.html',
  styleUrls: ['./equipamentos-mais-reincidentes.component.css']
})

export class EquipamentosMaisReincidentesComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  public equipamentosReincidentesModel: ViewDashboardEquipamentosMaisReincidentes[] = [];
  public loading: boolean = true;

  constructor(

    @Inject(LOCALE_ID) public locale: string,
    private _dashboardService: DashboardService,
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
    this.equipamentosReincidentesModel = (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.REINCIDENCIA_EQUIPAMENTOS_MAIS_REINCIDENTES }).toPromise())
      .viewDashboardEquipamentosMaisReincidentes;
    this.loading = false;
  }
}