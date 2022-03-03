import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardDisponibilidadeBBTSMapaRegioes, ViewDashboardDisponibilidadeBBTSMultasRegioes } from 'app/core/types/dashboard.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-mapa-disponibilidade',
  templateUrl: './mapa-disponibilidade.component.html',
  styleUrls: ['./mapa-disponibilidade.component.css'
  ],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None,
})

export class MapaDisponibilidadeComponent implements OnInit {

  private viewDadosMultas: ViewDashboardDisponibilidadeBBTSMultasRegioes[] = [];
  public viewDados: ViewDashboardDisponibilidadeBBTSMapaRegioes[] = [];
  public grupoRegiao: any[] = [];

  public loading: boolean = true;
  @Input() bbtsRegiaoMulta: boolean;

  constructor(private _dashboardService: DashboardService) { }

  async ngOnInit(): Promise<void> {
    this.buscarDados();
  }

  private async buscarDados() {
    if (this.bbtsRegiaoMulta) {
      this.viewDadosMultas = (await (this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.BBTS_MULTA_REGIOES }))
        .toPromise()).viewDashboardDisponibilidadeBBTSMultasRegioes;

      this.grupoRegiao = Enumerable.from(this.viewDadosMultas).groupBy(gr => gr.regiao).toArray();

    } else {
      this.viewDados = (await (this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.BBTS_MAPA_REGIOES }))
        .toPromise()).viewDashboardDisponibilidadeBBTSMapaRegioes;
    }
    this.loading = false;
  }

  public buscarDadosMultaPorRegiao(regiao: String, criticidade: String): number {
    return Enumerable.from(this.viewDadosMultas).firstOrDefault(f => f.regiao == regiao && f.criticidade == criticidade)?.multa;
  }
}