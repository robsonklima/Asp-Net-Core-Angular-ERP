import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { MatTabChangeEvent, MatTabGroup } from '@angular/material/tabs';
import { DashboardEnum } from 'app/core/types/dashboard.types';
import { takeUntil } from 'rxjs/operators';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { appConfig as c } from 'app/core/config/app.config';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',

})
export class DashboardComponent implements AfterViewInit {
  @ViewChild("tabGroup", { static: false }) tabGroup: MatTabGroup;

  public get dashboardEnum(): typeof DashboardEnum { return DashboardEnum }
  nomeSlideSelecionado: string = this.dashboardEnum.PERFORMANCE_FILIAIS_RESULTADO_GERAL;
  slideSelecionado: number = 0;
  usuarioSessao: UsuarioSessao;
  filtro: any;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _cdr: ChangeDetectorRef,
    protected _userService: UserService
  ) { }

  async ngAfterViewInit() {
    this.carregarDashboardsPerfil();

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

  selecionarSlide(nome: string) {
    this.nomeSlideSelecionado = nome;
    this.tabGroup.selectedIndex = 0;
  }

  public onAlterarTabPorClique = (tabChangeEvent: MatTabChangeEvent): void => {
    this.slideSelecionado = tabChangeEvent.index;
  }

  private trocarDashboardOuSlide(): void {
    let slides: string[] = Object.values(this.dashboardEnum);

    if (this.slideSelecionado == this.tabGroup._tabs.length - 1) {
      let subSlideIndex = slides.indexOf(this.nomeSlideSelecionado);

      if (subSlideIndex == slides.length - 1) {
        this.nomeSlideSelecionado = slides[0];
      } else {
        this.nomeSlideSelecionado = slides[subSlideIndex + 1];
      }

      this.slideSelecionado = 0;
      this.tabGroup.selectedIndex = 0;
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

  private carregarDashboardsPerfil() {
    const dashs = this.dashboardEnum;
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}