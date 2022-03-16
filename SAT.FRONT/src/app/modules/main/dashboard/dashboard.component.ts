import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { MatTabGroup } from '@angular/material/tabs';
import { takeUntil } from 'rxjs/operators';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { appConfig as c } from 'app/core/config/app.config';
import { PerfilEnum } from 'app/core/types/perfil.types';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',

})
export class DashboardComponent implements AfterViewInit {
  @ViewChild("tabGroup", { static: false }) tabGroup: MatTabGroup;
  usuarioSessao: UsuarioSessao;
  slides: string[] = [];
  filtro: any;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _cdr: ChangeDetectorRef,
    protected _userService: UserService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
    this.slides = this.carregarSlides();
    this.configurarSlidesAutomaticos();
  }

  private carregarSlides() {
    const codPerfil = this.usuarioSessao.usuario.codPerfil;

    switch (codPerfil) {
      case PerfilEnum.ADM_DO_SISTEMA:
        return [
          'Status Filiais',
          'Detalhado Filial',
          'Chamados Antigos',
          'BBTS Regiões',
          'BBTS Filiais',
          'BBTS Multa Regiões',
          'BBTS Multa Disponibilidade',
          'Média Global',
          'SPA',
          'Desempenho Técnico',
          'SLA Clientes',
          'Reincidência Filiais',
          'Reincidência Técnicos',
          'Reincidência Clientes',
          'Equipamentos Reincidentes',
          'Pendência Filiais',
          'Pendência Técnicos',
          'Peças Faltantes Filiais',
          'Peças Mais Faltantes',
          'Top Peças Faltantes',
          'Densidade',
        ];

        case PerfilEnum.FILIAL_COORDENADOR:
          return [
            'Pendência Filial',
          ];

      default:
        return [];
    }
  }

  public verificarPermissaoSlide(slide: string): boolean {
    return this.slides.indexOf(slide) > -1;
  }

  private trocarDashboardOuSlide(): void {
    if (this.tabGroup.selectedIndex != this.tabGroup._tabs.length - 1) {
      this.tabGroup.selectedIndex = this.tabGroup.selectedIndex + 1;
    } else {
      this.tabGroup.selectedIndex = 0;
    }
  }

  private configurarSlidesAutomaticos() {
    interval(c.tempo_atualizacao_dashboard_minutos * 60 * 1000)
      .pipe(
        takeUntil(this._onDestroy)
      )
      .subscribe(() => {
        this.trocarDashboardOuSlide();
      });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}