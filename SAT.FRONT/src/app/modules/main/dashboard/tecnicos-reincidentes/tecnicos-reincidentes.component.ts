import { Component, Input, OnInit } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import moment from 'moment';

@Component({
  selector: 'app-tecnicos-reincidentes',
  templateUrl: './tecnicos-reincidentes.component.html',
  styleUrls: ['./tecnicos-reincidentes.component.css']
})
export class TecnicosReincidentesComponent implements OnInit {
  @Input() ordem: string;
  public reincidenciaTecnicosModel: ReincidenciaTecnicosModel[] = [];
  public loading: boolean = true;

  constructor(private _tecnicoService: TecnicoService,
    private _indicadorService: IndicadorService) { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {
    this.loading = true;

    let dadosIndicadoresPercent = await this.buscaIndicadores(IndicadorAgrupadorEnum.TECNICO_PERCENT_REINCIDENTES);
    let dadosIndicadoresQnt = await this.buscaIndicadores(IndicadorAgrupadorEnum.TECNICO_QNT_CHAMADOS_REINCIDENTES);
    let listaTecnicos = (await this._tecnicoService.obterPorParametros({ indAtivo: 1 }).toPromise()).items;

    for (let indicador of dadosIndicadoresPercent) {
      let tecnico = listaTecnicos.find(t => t.codTecnico == +indicador.label);
      let model: ReincidenciaTecnicosModel = new ReincidenciaTecnicosModel();
      model.filial = tecnico.filial.nomeFilial;
      model.nomeTecnico = tecnico.nome;
      model.reincidencia = indicador.valor;
      model.qntAtendimentos = dadosIndicadoresQnt.find(f => f.label == indicador.label).valor;

      this.reincidenciaTecnicosModel.push(model);
    }

    this.reincidenciaTecnicosModel =
      this.ordem == 'asc' ? this.reincidenciaTecnicosModel.orderBy('reincidencia').thenBy('qntAtendimentos').take(5) :
        this.reincidenciaTecnicosModel.orderByDesc('reincidencia').thenByDesc('qntAtendimentos').take(5);

    this.loading = false;
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

export class ReincidenciaTecnicosModel {
  nomeTecnico: string;
  filial: string;
  reincidencia: number = 0;
  qntAtendimentos: number = 0;
}
