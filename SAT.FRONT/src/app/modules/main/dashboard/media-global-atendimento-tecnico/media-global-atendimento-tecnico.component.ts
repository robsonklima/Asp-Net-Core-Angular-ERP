import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardDisponibilidadeTecnicosMediaGlobal } from 'app/core/types/dashboard.types';
import { Filtro } from 'app/core/types/filtro.types';

@Component({
  selector: 'app-media-global-atendimento-tecnico',
  templateUrl: './media-global-atendimento-tecnico.component.html',
  styles: [`tr:nth-child(odd) { background-color: rgb(239,245,254); }`]
})

export class MediaGlobalAtendimentoTecnicoComponent implements OnInit {
  @Input() filtro: Filtro;
  public mediaGlobalAtendimentoTecnicosModel: ViewDashboardDisponibilidadeTecnicosMediaGlobal[] = [];
  public loading: boolean = true;

  constructor(private _cdr: ChangeDetectorRef,
    private _dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.obterDados();
  }

  async obterDados() {
    this.loading = true;
    this.mediaGlobalAtendimentoTecnicosModel = (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.DISPONIBILIDADE_TECNICOS_MEDIA_GLOBAL }).toPromise())
      .viewDashboardDisponibilidadeTecnicosMediaGlobal;
    this.loading = false;
    this._cdr.detectChanges();
  }
}