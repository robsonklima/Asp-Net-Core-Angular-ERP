import { Component, OnInit } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { IndicadorAgrupadorEnum, Indicador, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import moment from 'moment';

@Component({
  selector: 'app-equipamentos-mais-reincidentes',
  templateUrl: './equipamentos-mais-reincidentes.component.html'
})
export class EquipamentosMaisReincidentesComponent implements OnInit {

  constructor(
    private _indicadorService: IndicadorService) { }

  ngOnInit(): void {
    
  }

  private async obterDados() {
    let dadosIndicadoresPercent = await this.buscaIndicadores(IndicadorAgrupadorEnum.EQUIPAMENTO_PERCENT_REINCIDENTES);
    console.log(dadosIndicadoresPercent);
  }
  
  private async buscaIndicadores(indicadorAgrupadorEnum: IndicadorAgrupadorEnum): Promise<Indicador[]> {
    let indicadorParams = {
      tipo: IndicadorTipoEnum.REINCIDENCIA,
      agrupador: indicadorAgrupadorEnum,
      codAutorizadas: "",
      codTiposGrupo: "",
      codTiposIntervencao: "",
      dataInicio: '2021-08-01',//moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      dataFim: moment().endOf('month').format('YYYY-MM-DD hh:mm')
    }
    return await this._indicadorService.obterPorParametros(indicadorParams).toPromise();
  }
}
