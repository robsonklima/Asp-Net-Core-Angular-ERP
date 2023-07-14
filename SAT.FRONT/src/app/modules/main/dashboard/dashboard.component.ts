import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { MatTabGroup } from '@angular/material/tabs';
import { takeUntil } from 'rxjs/operators';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { appConfig as c } from 'app/core/config/app.config';
import { PerfilEnum } from 'app/core/types/perfil.types';
import moment from 'moment';
import { SetorEnum } from 'app/core/types/setor.types';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styles: [`
    .mat-tab-label {
      font-size: 18pt;
      background-color: transparent;
      font-weight: 700;
    }
  `]
})
export class DashboardComponent implements AfterViewInit {
  @ViewChild("tabGroup", { static: false }) tabGroup: MatTabGroup;
  usuarioSessao: UsuarioSessao;
  dataAtual: any = moment();
  slides: string[] = [];
  filtro: any;
  permiteTrocarSlideAutomaticamente: boolean = true;
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

  public verificarPermissaoSlide(slide: string): boolean {
    return this.slides.indexOf(slide) > -1;
  }

  private trocarDashboardOuSlide(): void {
    if (!this.permiteTrocarSlideAutomaticamente)
      return

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

  private carregarSlides() {
    const codPerfil = this.usuarioSessao.usuario.codPerfil;
    const codSetor = this.usuarioSessao.usuario.codSetor;

    if(codSetor == SetorEnum.SISTEMA_DE_ASSISTENCIA_TECNICA || codSetor == SetorEnum.SUPORTE_TECNICO
       || codPerfil == PerfilEnum.SUPERVISOR )
    {
      return [
        'Status Filiais',
        'Chamados Antigos',
        'BBTS Regiões',
        'BBTS Filiais',
        'BBTS Multa Regiões',
        'BBTS Multa Disponibilidade',
        'Média Global',
        'SPA',
        'Desempenho Técnico',
        'Disponibilidade Técnicos',
        'SLA Clientes',
        'Reincidência Filiais',
        'Reincidência Técnicos',
        'Reincidência Clientes',
        'Equipamentos Reincidentes',
        'Pendência Filiais',
        'Pendência Técnicos',
        'Peças Mais Faltantes',
        'Densidade'
      ];
    }
    else if(codSetor == SetorEnum.OPERACAO_DE_CAMPO || codSetor == SetorEnum.CENTRO_TECNICO_OPERACIONAL)
    {
      if(codPerfil == PerfilEnum.FILIAL_TECNICO_DE_CAMPO)
      {
        return [
          'Status Filiais',
          'Detalhado Filial',
          'Média Global',
          'SPA',
          'Desempenho Técnico',
          'Disponibilidade Técnicos',
          'SLA Clientes',
          'Reincidência Filiais',
          'Reincidência Técnicos',
          'Reincidência Clientes',
          'Equipamentos Reincidentes',
          'Pendência Filiais',
          'Pendência Técnicos'
        ];
      }
      else
      {
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
          'Disponibilidade Técnicos',
          'SLA Clientes',
          'Reincidência Filiais',
          'Reincidência Técnicos',
          'Reincidência Clientes',
          'Equipamentos Reincidentes',
          'Pendência Filiais',
          'Pendência Técnicos',
          'Peças Mais Faltantes',
          'Densidade'
        ];
      }
    }
    else if(codSetor == SetorEnum.COORDENACAO_DE_CONTRATOS)
    {
      return [
        'Status Filiais',
        'Chamados Antigos',
        'BBTS Regiões',
        'BBTS Filiais',
        'BBTS Multa Regiões',
        'BBTS Multa Disponibilidade',
        'Média Global',
        'SPA',
        'Disponibilidade Técnicos',
        'SLA Clientes',
        'Reincidência Filiais',
        'Reincidência Técnicos',
        'Reincidência Clientes',
        'Equipamentos Reincidentes',
        'Pendência Filiais',
        'Peças Mais Faltantes',
        'Densidade'
      ];
    }
    else if(codSetor == SetorEnum.LOGISTICA || codSetor == SetorEnum.LABORATORIO_TECNICO)
    {
      return [
        'Status Filiais',
        'Chamados Antigos',
        'BBTS Regiões',
        'BBTS Filiais',
        'BBTS Multa Regiões',
        'BBTS Multa Disponibilidade',
        'Média Global',
        'SPA',
        'Disponibilidade Técnicos',
        'SLA Clientes',
        'Equipamentos Reincidentes',
        'Pendência Filiais',
        'Peças Mais Faltantes',
        'Densidade'
      ];
    }
    else if(codSetor == SetorEnum.GERENCIA)
    {
      return [
        'Status Filiais',
        'Chamados Antigos',
        'BBTS Regiões',
        'BBTS Filiais',
        'BBTS Multa Regiões',
        'BBTS Multa Disponibilidade',
        'Média Global',
        'SPA',
        'Densidade'
      ];
    }
    else if(codSetor == SetorEnum.HELPDESK_NOC)
    {
      return [
        'Status Filiais',
        'Chamados Antigos',
        'Disponibilidade Técnicos',
        'SLA Clientes',
        'Reincidência Clientes',
        'Densidade'
      ];
    }
    
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}