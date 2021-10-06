import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { appConfig as c } from 'app/core/config/app.config'
import { MatTabGroup } from '@angular/material/tabs';
import { DashboardEnum } from 'app/core/types/dashboard.types';
import { MatSidenav } from '@angular/material/sidenav';
import { takeUntil } from 'rxjs/operators';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',

})
export class DashboardComponent implements AfterViewInit {
  @ViewChild("tabGroup", { static: false }) tabGroup: MatTabGroup;

  public get dashboardEnum(): typeof DashboardEnum {
    return DashboardEnum;
  }
  dashboardSelecionado: string = this.dashboardEnum.REINCIDENCIA_FILIAIS; //PERFORMANCE_FILIAIS_RESULTADO_GERAL;
  slideSelecionado: number = 0;
  @ViewChild('sidenav') sidenav: MatSidenav;
  usuarioSessao: UsuarioSessao;
  filtro: any;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _cdr: ChangeDetectorRef,
    private _userSvc: UserService
  ) { }

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

    this.sidenav.closedStart.subscribe(() => {
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
    this.filtro = this._userSvc.obterFiltro('dashboard');

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
}
