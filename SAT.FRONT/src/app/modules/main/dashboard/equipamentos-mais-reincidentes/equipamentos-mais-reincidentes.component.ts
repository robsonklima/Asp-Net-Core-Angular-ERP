import { Component, Inject, Input, OnInit } from '@angular/core';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { IndicadorService } from 'app/core/services/indicador.service';
import { IndicadorAgrupadorEnum, Indicador, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import { LOCALE_ID } from '@angular/core';
import moment from 'moment';
import Enumerable from 'linq';
import { OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';

@Component({
  selector: 'app-equipamentos-mais-reincidentes',
  templateUrl: './equipamentos-mais-reincidentes.component.html',
  styleUrls: ['./equipamentos-mais-reincidentes.component.css']
})

export class EquipamentosMaisReincidentesComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  public equipamentosReincidentesModel: EquipamentosReincidentesModel[] = [];
  public loading: boolean = true;

  constructor(

    @Inject(LOCALE_ID) public locale: string,
    private _equipContratoService: EquipamentoContratoService,
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

    let dadosIndicadoresPercent = await this.buscaIndicadores();
    //let equipsReincidentes = dadosIndicadoresPercent.filter(e => e.valor > 0);
    let equipContratoList = Enumerable.from(dadosIndicadoresPercent).orderBy(ord => ord.valor).take(5);

    for (let indicador of equipContratoList) {
      let model = new EquipamentosReincidentesModel()
      {
        let equipContrato = (await this._equipContratoService.obterPorCodigo(+indicador.label).toPromise());

        model.modelo = equipContrato.equipamento.nomeEquip;
        model.cliente = equipContrato.cliente.nomeFantasia;
        model.serie = equipContrato.numSerie;
        model.reincidencia = indicador.valor.toString();
      }

      this.equipamentosReincidentesModel.push(model);
    }

    this.loading = false;
  }

  private async buscaIndicadores(): Promise<Indicador[]> {
    let indicadorParams = {
      tipo: IndicadorTipoEnum.REINCIDENCIA,
      agrupador: IndicadorAgrupadorEnum.EQUIPAMENTO_PERCENT_REINCIDENTES,
      include: OrdemServicoIncludeEnum.OS_EQUIPAMENTOS_ATENDIMENTOS,
      filterType: OrdemServicoFilterEnum.FILTER_INDICADOR,
      //dataInicio: this.filter?.parametros.dataInicio || moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      //dataFim: this.filter?.parametros.dataFim || moment().endOf('month').format('YYYY-MM-DD hh:mm')
      dataInicio: moment().add(-30, 'days').format('YYYY-MM-DD 00:00'),
      dataFim: moment().format('YYYY-MM-DD 23:59')
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
