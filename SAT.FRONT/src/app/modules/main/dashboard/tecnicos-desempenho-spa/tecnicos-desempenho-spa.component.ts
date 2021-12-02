import { Component, Input, OnInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { IndicadorService } from 'app/core/services/indicador.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import { OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import Enumerable from 'linq';
import moment from 'moment';

@Component({
  selector: 'app-tecnicos-desempenho-spa',
  templateUrl: './tecnicos-desempenho-spa.component.html',
  styleUrls: ['./tecnicos-desempenho-spa.component.css']
})
export class TecnicosDesempenhoSpaComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  @Input() ordem: string;
  public desempenhoTecnicosModel: DesempenhoTecnicosModel[] = [];
  public loading: boolean = true;

  constructor(private _tecnicoService: TecnicoService,
    private _indicadorService: IndicadorService,
    protected _userService: UserService) {
    super(_userService, 'dashboard-filtro')
  }

  ngOnInit(): void {
    this.obterDados();
    this.registerEmitters();
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  loadFilter(): void {
    super.loadFilter();
  }

  private async obterDados() {
    this.loading = true;

    let dadosIndicadoresPercent = await this.buscaIndicadores(IndicadorAgrupadorEnum.TECNICO_PERCENT_SPA);
    let dadosIndicadoresQnt = await this.buscaIndicadores(IndicadorAgrupadorEnum.TECNICO_QNT_CHAMADOS_SPA);
    let listaTecnicos = (await this._tecnicoService.obterPorParametros({ indAtivo: 1 }).toPromise()).items;

    for (let indicador of dadosIndicadoresPercent) {
      let tecnico = Enumerable.from(listaTecnicos).firstOrDefault(t => t.codTecnico == +indicador.label);

      // Existem tecnicos de teste ou help desk que não queremos na tabela
      if (tecnico == undefined) continue;

      let model: DesempenhoTecnicosModel = new DesempenhoTecnicosModel();
      model.filial = tecnico.filial.nomeFilial;
      model.nomeTecnico = tecnico.nome;
      model.spa = indicador.valor;
      model.qntAtendimentos = dadosIndicadoresQnt.find(f => f.label == indicador.label).valor;

      this.desempenhoTecnicosModel.push(model);
    }

    this.desempenhoTecnicosModel =
      this.ordem == 'asc' ?
        Enumerable.from(this.desempenhoTecnicosModel).orderBy(ord => ord.spa).thenBy(ord => ord.qntAtendimentos).take(5).toArray() :
        Enumerable.from(this.desempenhoTecnicosModel).orderByDescending(ord => ord.spa).thenByDescending(ord => ord.qntAtendimentos).take(5).toArray();

    this.loading = false;
  }

  private async buscaIndicadores(indicadorAgrupadorEnum: IndicadorAgrupadorEnum): Promise<Indicador[]> {
    let indicadorParams = {
      tipo: IndicadorTipoEnum.SPA,
      agrupador: indicadorAgrupadorEnum,
      include: OrdemServicoIncludeEnum.OS_TECNICO_ATENDIMENTO,
      filterType: OrdemServicoFilterEnum.FILTER_INDICADOR,
      dataInicio: this.filter?.parametros.dataInicio || moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      dataFim: this.filter?.parametros.dataFim || moment().endOf('month').format('YYYY-MM-DD hh:mm')
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
