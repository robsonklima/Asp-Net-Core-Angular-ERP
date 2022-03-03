import { Component, Input, OnInit } from '@angular/core';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardDisponibilidadeBBTSFiliais } from 'app/core/types/dashboard.types';
import { Filtro } from 'app/core/types/filtro.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-disponibilidade-bbts-filiais',
  templateUrl: './disponibilidade-bbts-filiais.component.html',
  styleUrls: ['./disponibilidade-bbts-filiais.component.css'
  ]
})
export class DisponibilidadeBbtsFiliaisComponent implements OnInit {
  @Input() filtro: Filtro;
  public disponibilidadeFilial: ViewDashboardDisponibilidadeBBTSFiliais[] = [];
  public filiais: string[] = [];
  public loading: boolean = true;

  constructor(private _dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.loading = true;
    this.obterDados();
  }

  private async obterDados() {
    this.disponibilidadeFilial = (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.BBTS_FILIAIS }).toPromise())
      .viewDashboardDisponibilidadeBBTSFiliais;

    this.filiais = Enumerable.from(this.disponibilidadeFilial).groupBy(g => g.filial).select(s => s.key()).toArray();

    this.loading = false;
  }

  buscaIndice(filial: string, indiceDisp: number) {
    let nomeIndice = 'DISP 1' + indiceDisp;
    let valorIndice = Enumerable.from(this.disponibilidadeFilial).firstOrDefault(f => f.filial == filial && f.criticidade == nomeIndice)?.indice;
    return valorIndice;
  }

  buscaSaldo(filial: string, indiceDisp: number) {
    let nomeIndice = 'DISP 1' + indiceDisp;
    let valorSaldo = Enumerable.from(this.disponibilidadeFilial).firstOrDefault(f => f.filial == filial && f.criticidade == nomeIndice)?.saldo;
    return valorSaldo;
  }
}