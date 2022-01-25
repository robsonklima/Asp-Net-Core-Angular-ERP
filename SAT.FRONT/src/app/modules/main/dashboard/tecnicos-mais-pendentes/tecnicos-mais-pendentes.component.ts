import { Component, Input, OnInit } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import moment from 'moment';
import Enumerable from 'linq';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-tecnicos-mais-pendentes',
  templateUrl: './tecnicos-mais-pendentes.component.html',
  styleUrls: ['./tecnicos-mais-pendentes.component.css']
})
export class TecnicosMaisPendentesComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  @Input() ordem: string;
  public pendenciaTecnicosModel: PendenciaTecnicosModel[] = [];
  public loading: boolean = true;

  constructor(
    private _tecnicoService: TecnicoService,
    private _indicadorService: IndicadorService,
    protected _userService: UserService
  ) {
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

    let dadosIndicadoresPercent = await this.buscaIndicadores(IndicadorAgrupadorEnum.TECNICO_PERCENT_PENDENTES);
    let dadosIndicadoresQnt = await this.buscaIndicadores(IndicadorAgrupadorEnum.TECNICO_QNT_CHAMADOS_PENDENTES);
    let listaTecnicos = (await this._tecnicoService.obterPorParametros({}).toPromise()).items;

    for (let indicador of dadosIndicadoresPercent) {
      let tecnico = listaTecnicos.find(t => t.codTecnico == +indicador.label);
      let model: PendenciaTecnicosModel = new PendenciaTecnicosModel();
      model.filial = tecnico?.filial?.nomeFilial;
      model.nomeTecnico = tecnico?.nome;
      model.pendencia = indicador.valor;
      model.qntAtendimentos = dadosIndicadoresQnt.find(qtd => qtd.label == indicador.label)?.valor ?? 0;

      this.pendenciaTecnicosModel.push(model);
    }

    this.pendenciaTecnicosModel.push(...
      this.ordem == 'asc' ? Enumerable.from(this.pendenciaTecnicosModel).orderByDescending(ord => ord.pendencia).take(5) :
        Enumerable.from(this.pendenciaTecnicosModel).orderBy(ord => ord.pendencia).take(5)
    );

    this.loading = false;
  }
  private async buscaIndicadores(indicadorAgrupadorEnum: IndicadorAgrupadorEnum): Promise<Indicador[]> {
    let indicadorParams = {
      tipo: IndicadorTipoEnum.PENDENCIA,
      agrupador: indicadorAgrupadorEnum,
      dataInicio: moment().add(-30, 'days').format('YYYY-MM-DD 00:00'),
      dataFim: moment().format('YYYY-MM-DD 23:59')
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
