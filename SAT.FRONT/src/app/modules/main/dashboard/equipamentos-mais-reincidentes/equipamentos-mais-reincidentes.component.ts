import { formatPercent } from '@angular/common';
import { Component, Inject, Input, NgModule, OnInit } from '@angular/core';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { IndicadorService } from 'app/core/services/indicador.service';
import { IndicadorAgrupadorEnum, Indicador, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import { LOCALE_ID } from '@angular/core';
import moment from 'moment';

@Component({
  selector: 'app-equipamentos-mais-reincidentes',
  templateUrl: './equipamentos-mais-reincidentes.component.html',
  styleUrls: ['./equipamentos-mais-reincidentes.component.css']
})

export class EquipamentosMaisReincidentesComponent implements OnInit {
  public equipamentosReincidentesModel: EquipamentosReincidentesModel[] = [];
  public loading: boolean = true;
  
  constructor(
  
    @Inject(LOCALE_ID ) public locale: string,
    private _equipContratoService: EquipamentoContratoService,
    private _indicadorService: IndicadorService
    ) { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {
    this.loading = true;

    let dadosIndicadoresPercent = await this.buscaIndicadores(IndicadorAgrupadorEnum.EQUIPAMENTO_PERCENT_REINCIDENTES);
    let equipsReincidentes = dadosIndicadoresPercent.filter( e => e.valor > 0 );
    let equipContratoList = dadosIndicadoresPercent.orderBy('valor').take(5);

    for (let indicador of equipContratoList) {
      let model = new EquipamentosReincidentesModel()
      {
        let equipContrato = (await this._equipContratoService.obterPorCodigo(+indicador.label).toPromise());

        model.modelo = equipContrato.equipamento.nomeEquip;
        model.cliente= equipContrato.cliente.nomeFantasia;
        model.serie = equipContrato.numSerie;
        model.reincidencia = indicador.valor.toString();
      }

      this.equipamentosReincidentesModel.push(model);
    }

    this.loading = false;
  }
  
  private async buscaIndicadores(indicadorAgrupadorEnum: IndicadorAgrupadorEnum): Promise<Indicador[]> {
    let indicadorParams = {
      tipo: IndicadorTipoEnum.REINCIDENCIA,
      agrupador: indicadorAgrupadorEnum,
      dataInicio:'2021-09-01', //moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      dataFim: '2021-09-30'//moment().endOf('month').format('YYYY-MM-DD hh:mm')
    }
    return await this._indicadorService.obterPorParametros(indicadorParams).toPromise();
  }
  
}
export class EquipamentosReincidentesModel {
  modelo: string;
  cliente: string;
  serie: string;
  reincidencia: string;
}
