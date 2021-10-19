import { Component, Input, OnInit } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import moment from 'moment';

@Component({
  selector: 'app-tecnicos-desempenho-spa',
  templateUrl: './tecnicos-desempenho-spa.component.html',
  styleUrls: ['./tecnicos-desempenho-spa.component.css']
})
export class TecnicosDesempenhoSpaComponent implements OnInit {
  @Input() ordem: string;
  public desempenhoTecnicosModel: DesempenhoTecnicosModel[] = [];
  public loading: boolean = true;

  constructor(private _tecnicoService: TecnicoService,
    private _indicadorService: IndicadorService) { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {
    this.loading = true;

    let dadosIndicadoresPercent = await this.buscaIndicadores(IndicadorAgrupadorEnum.TECNICO_PERCENT_SPA);
    let dadosIndicadoresQnt = await this.buscaIndicadores(IndicadorAgrupadorEnum.TECNICO_QNT_CHAMADOS_SPA);
    let listaTecnicos = (await this._tecnicoService.obterPorParametros({ indAtivo: 1 }).toPromise()).items;

    for (let indicador of dadosIndicadoresPercent) {
      let tecnico = listaTecnicos.find(t => t.codTecnico == +indicador.label);
      let model: DesempenhoTecnicosModel = new DesempenhoTecnicosModel();
      model.filial = tecnico.filial.nomeFilial;
      model.nomeTecnico = tecnico.nome;
      model.spa = indicador.valor;
      model.qntAtendimentos = dadosIndicadoresQnt.find(f => f.label == indicador.label).valor;

      this.desempenhoTecnicosModel.push(model);
    }

    this.desempenhoTecnicosModel =
      this.ordem == 'asc' ? this.desempenhoTecnicosModel.orderBy('spa').thenBy('qntAtendimentos').take(5) :
        this.desempenhoTecnicosModel.orderByDesc('spa').thenByDesc('qntAtendimentos').take(5);

    this.loading = false;
  }

  private async buscaIndicadores(indicadorAgrupadorEnum: IndicadorAgrupadorEnum): Promise<Indicador[]> {
    let indicadorParams = {
      tipo: IndicadorTipoEnum.SPA,
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

export class DesempenhoTecnicosModel {
  nomeTecnico: string;
  filial: string;
  spa: number = 0;
  qntAtendimentos: number = 0;
}
