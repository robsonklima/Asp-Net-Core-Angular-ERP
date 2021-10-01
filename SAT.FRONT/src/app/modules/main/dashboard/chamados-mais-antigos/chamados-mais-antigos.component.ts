import { Component, OnInit } from '@angular/core';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServico, OrdemServicoData, OrdemServicoParameters } from 'app/core/types/ordem-servico.types';
import moment from 'moment';

@Component({
  selector: 'app-chamados-mais-antigos',
  templateUrl: './chamados-mais-antigos.component.html',
  styleUrls: ['./chamados-mais-antigos.component.css']
})
export class ChamadosMaisAntigosComponent implements OnInit {

  element_data_corretivas: OrdemServico[] = [];
  element_data_orcamentos: OrdemServico[] = [];
  loading_corretivas: boolean = true;
  loading_orcamentos: boolean = true;

  constructor(private _ordemServicoService: OrdemServicoService) { }

  ngOnInit(): void {
    this.loading_corretivas = true;
    this.loading_orcamentos = true;
    this.obterDadosCorretivas();
    this.obterDadosOrcamentos();
  }

  async obterDadosCorretivas() {
    this.element_data_corretivas = (await this.obterDados("2")).items;//CORRETIVA
    this.element_data_corretivas.sort((a, b) => (a.dataHoraAberturaOS > b.dataHoraAberturaOS ? 1 : -1));
    this.loading_corretivas = false;
  }

  async obterDadosOrcamentos() {
    this.element_data_orcamentos = (await (this.obterDados("5"))).items;//ORÇAMENTO
    this.element_data_orcamentos.sort((a, b) => (a.dataHoraAberturaOS > b.dataHoraAberturaOS ? 1 : -1));
    this.loading_orcamentos = false;
  }

  async obterDados(codTipoIntervencao: string): Promise<OrdemServicoData> {
    const params: OrdemServicoParameters = {
      sortActive: 'DataHoraAberturaOS',
      sortDirection: 'desc',
      dataAberturaInicio: '2021-01-01', //moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      dataAberturaFim: moment().endOf('month').format('YYYY-MM-DD hh:mm'),
      pageSize: 5,
      codTiposIntervencao: codTipoIntervencao
    };

    return await this._ordemServicoService
      .obterPorParametros({
        ...params
      }).toPromise();
  }
}