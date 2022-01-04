import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { appConfig as c } from 'app/core/config/app.config'
import { MatTabGroup } from '@angular/material/tabs';
import { DashboardEnum } from 'app/core/types/dashboard.types';
import { MatSidenav } from '@angular/material/sidenav';
import { takeUntil } from 'rxjs/operators';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { Filterable } from 'app/core/filters/filterable';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',

})
export class DashboardComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild("tabGroup", { static: false }) tabGroup: MatTabGroup;

  public get dashboardEnum(): typeof DashboardEnum {
    return DashboardEnum;
  }
  dashboardSelecionado: string = this.dashboardEnum.MONITORAMENTO_SAT;
  slideSelecionado: number = 0;
  @ViewChild('sidenav') sidenav: MatSidenav;
  usuarioSessao: UsuarioSessao;
  filtro: any;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _cdr: ChangeDetectorRef,
    protected _userService: UserService
  ) {
    super(_userService, 'ordem-servico')
  }

  async ngAfterViewInit() {
    interval(c.tempo_atualizacao_dashboard_minutos * 60 * 1000)
      .pipe(
        takeUntil(this._onDestroy)
      )
      .subscribe(() => {
        this.trocarDashboardOuSlide();
      });

    this.configurarFiltro();
    this._cdr.detectChanges();
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.configurarFiltro();
    })
  }

  private trocarDashboardOuSlide(): void {
    let dashboards: string[] = Object.values(this.dashboardEnum);

    if (this.slideSelecionado == this.tabGroup._tabs.length - 1) {
      let dashboardIndex = dashboards.indexOf(this.dashboardSelecionado);

      if (dashboardIndex == dashboards.length - 1) {
        this.dashboardSelecionado = dashboards[0];
      } else {
        this.dashboardSelecionado = dashboards[dashboardIndex + 1];
      }

      this.slideSelecionado = 0;
    } else {
      this.slideSelecionado = this.tabGroup.selectedIndex + 1;
      this.tabGroup.selectedIndex = this.slideSelecionado;
    }

    this._cdr.detectChanges();
  }

  private configurarFiltro(): void {
    if (!this.filtro) {
      return;
    }

    // Filtro obrigatorio de filial quando o usuario esta vinculado a uma filial
    if (this.usuarioSessao?.usuario?.codFilial) {
      this.filtro.parametros.codFiliais = [this.usuarioSessao.usuario.codFilial]
    }

    Object.keys(this.filtro?.parametros).forEach((key) => {
      if (this.filtro.parametros[key] instanceof Array) {
        this.filtro.parametros[key] = this.filtro.parametros[key].join()
      };
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }

  public abrirLinkRelatorioPecas() {
    window.open('http://satdbprod/Reports/report/Reports/Relat%C3%B3rio%20DSS%20-%20SAT%20-%20Logistica%20-%20Chamado%20Faltante%20sem%20Status%20a%20mais%20de%2024%20horas%20-%20Analistas')
  }

  public abrirLinkRelatorioReincidenciaClientes() {
    window.open('http://satdbprod/Reports/report/Reports/INDICADORES%20DI%C3%81RIOS-%C3%8DNDICE%20DE%20REINCID%C3%8ANCIA-CLIENTE')
  }

}
