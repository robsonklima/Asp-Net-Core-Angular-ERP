import { Filial } from './../../../../core/types/filial.types';
import { Component, Input, OnInit } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import moment from 'moment';

@Component({
  selector: 'app-tecnicos-mais-pendentes',
  templateUrl: './tecnicos-mais-pendentes.component.html'
})
export class TecnicosMaisPendentesComponent implements OnInit {
  @Input() ordem: string;
  public pendenciaTecnicosModel: PendenciaTecnicosModel[] = [];
  public loading: boolean = true;

  constructor(
    private _tecnicoService: TecnicoService,
    private _indicadorService: IndicadorService
    ) { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {
    this.loading = true;

    let dadosIndicadoresPercent = await this.buscaIndicadores(IndicadorAgrupadorEnum.TECNICO_PERCENT_PENDENTES);
    let dadosIndicadoresQnt = await this.buscaIndicadores(IndicadorAgrupadorEnum.TECNICO_QNT_CHAMADOS_PENDENTES);
    let listaTecnicos = (await this._tecnicoService.obterPorParametros({ indAtivo: 1 }).toPromise()).items;
    
    console.log(listaTecnicos);

    for (let indicador of dadosIndicadoresPercent) {
      let tecnico = listaTecnicos.find(t => t.codTecnico == +indicador.label);
      let model: PendenciaTecnicosModel = new PendenciaTecnicosModel();
      model.filial = tecnico?.filial?.nomeFilial;
      model.nomeTecnico = tecnico?.nome;
      model.pendencia = indicador.valor;
      model.qntAtendimentos = 0//dadosIndicadoresQnt?.find(f => f.label == indicador.label).valor?? 0;
      
      this.pendenciaTecnicosModel.push(model);
    }
    this.pendenciaTecnicosModel =
        this.pendenciaTecnicosModel.orderBy('pendencia').take(5);

    this.loading = false;
  }
  private async buscaIndicadores(indicadorAgrupadorEnum: IndicadorAgrupadorEnum): Promise<Indicador[]> {
    let indicadorParams = {
      tipo: IndicadorTipoEnum.PENDENCIA,
      agrupador: indicadorAgrupadorEnum,
      codAutorizadas: "",
      codTiposGrupo: "",
      codTiposIntervencao: "",
      dataInicio: '2021-09-01', //moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      dataFim: '2021-10-01'//moment().endOf('month').format('YYYY-MM-DD hh:mm')
    }
    return await this._indicadorService.obterPorParametros(indicadorParams).toPromise();
  }
}

export class PendenciaTecnicosModel {
  nomeTecnico: string;
  filial: string;
  pendencia: number = 0;
  qntAtendimentos: number = 0;
}
