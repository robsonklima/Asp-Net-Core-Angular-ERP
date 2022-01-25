import { Component, OnInit } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import { OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import Enumerable from 'linq';
import moment from 'moment';

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

    //let dataInicio = moment().add(-30, 'days').format('yyyy-MM-DD HH:mm:ss'); // Ultimos 30 dias
    //let dataFim = moment().format('yyyy-MM-DD HH:mm:ss');
    
    let dataInicio = moment().add(-30, 'days').format('YYYY-MM-DD 00:00');
    let dataFim = moment().format('YYYY-MM-DD 23:59');

    this._indicadorService.obterPorParametros({
      tipo: IndicadorTipoEnum.PECA_FALTANTE,
      agrupador: IndicadorAgrupadorEnum.TOP_CINCO_PECAS_MAIS_FALTANTES,
      filterType: OrdemServicoFilterEnum.FILTER_PECAS_FALTANTES,
      include: OrdemServicoIncludeEnum.OS_PECAS,
      dataInicio: dataInicio,
      dataFim: dataFim
    }).subscribe((indicadores: Indicador[]) => {
      let agrupaIndicadores = Enumerable.from(indicadores).groupBy(g => g.label).toArray();
      for (let indicador of agrupaIndicadores) {
        let data = indicador.getSource();
        let peca: TopPecasFaltantesModel = new TopPecasFaltantesModel();
        peca.codMagnus = indicador.key();
        peca.peca = data[0].filho[0].label;
        peca.quantidade = Enumerable.from(data).sum(s => s.filho[0].valor);
        this.topPecasFaltantes.push(peca);
      }

      this.topPecasFaltantes = Enumerable.from(this.topPecasFaltantes).orderByDescending(ord => ord.quantidade).take(5).toArray();
      this.loading = false;
    });
  }
}

export class TopPecasFaltantesModel {
  public codMagnus: string;
  public peca: string;
  public quantidade: number;
}