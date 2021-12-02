import { Component, OnInit } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import { OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';

@Component({
  selector: 'app-cinco-pecas-mais-faltantes',
  templateUrl: './cinco-pecas-mais-faltantes.component.html',
  styleUrls: ['./cinco-pecas-mais-faltantes.component.css']
})

export class CincoPecasMaisFaltantesComponent implements OnInit {
  public loading: boolean = true;
  public topPecasFaltantes: TopPecasFaltantesModel[] = []

  constructor(private _indicadorService: IndicadorService) { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {
    this.loading = true;

    this._indicadorService.obterPorParametros({
      tipo: IndicadorTipoEnum.PECA_FALTANTE,
      agrupador: IndicadorAgrupadorEnum.TOP_PECAS_MAIS_FALTANTES,
      filterType: OrdemServicoFilterEnum.FILTER_PECAS_FALTANTES,
      include: OrdemServicoIncludeEnum.OS_PECAS
    }).subscribe((indicadores: Indicador[]) => {
      for (let data of indicadores) {
        let peca: TopPecasFaltantesModel = new TopPecasFaltantesModel();
        peca.codMagnus = data.label;
        peca.peca = data.filho[0].label;
        peca.quantidade = data.filho[0].valor;
        this.topPecasFaltantes.push(peca);
      }

      this.loading = false;
    });
  }
}

export class TopPecasFaltantesModel {
  public codMagnus: string;
  public peca: string;
  public quantidade: number;
}