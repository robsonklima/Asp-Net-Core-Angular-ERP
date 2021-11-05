import { Component, OnInit } from '@angular/core';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServico, OrdemServicoData, OrdemServicoFilterEnum, OrdemServicoIncludeEnum, OrdemServicoParameters } from 'app/core/types/ordem-servico.types';
import Enumerable from 'linq';
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
    this.element_data_corretivas = (await this.obterDados(OrdemServicoFilterEnum.FILTER_CORRETIVAS_ANTIGAS)).items;//CORRETIVA
    Enumerable.from(this.element_data_corretivas).orderBy(os => os.dataHoraAberturaOS);
    this.loading_corretivas = false;
  }

  async obterDadosOrcamentos() {
    this.element_data_orcamentos = (await (this.obterDados(OrdemServicoFilterEnum.FILTER_ORCAMENTOS_ANTIGOS))).items;//ORÇAMENTO
    Enumerable.from(this.element_data_orcamentos).orderBy(os => os.dataHoraAberturaOS);
    this.loading_orcamentos = false;
  }

  async obterDados(_filterType: OrdemServicoFilterEnum): Promise<OrdemServicoData> {
    const params: OrdemServicoParameters = {
      include: OrdemServicoIncludeEnum.OS_EQUIPAMENTOS,
      filterType: _filterType,
      pageSize: 5,
      sortDirection: 'asc',
      sortActive: 'datahoraAberturaOS'
    };

    return await this._ordemServicoService
      .obterPorParametros({
        ...params
      }).toPromise();
  }
}