import { Component, OnInit } from '@angular/core';
import { MonitoramentoService } from 'app/core/services/monitoramento.service';
import { Monitoramento } from 'app/core/types/monitoramento.types';
import moment from 'moment';

@Component({
  selector: 'app-servicos',
  templateUrl: './servicos.component.html'
})
export class ServicosComponent implements OnInit
{
  monitoramentos: Monitoramento[] = [];
  monitoramentoChamados: Monitoramento[] = [];
  monitoramentoServicos: Monitoramento[] = [];
  monitoramentoConexoes: Monitoramento[] = [];
  monitoramentoIntegracoes: Monitoramento[] = [];
  loading: boolean;

  constructor(
    private _monitoramentoService: MonitoramentoService,
  ) { }

  async ngOnInit()
  {
    await this.obterMonitoramentos();
  }

  private obterMonitoramentos(): Promise<any>
  {
    return new Promise((resolve, reject) =>
    {
      this.loading = true;

      this._monitoramentoService.obterPorParametros({
        sortActive: "dataHoraProcessamento",
        sortDirection: "asc",
      }).subscribe((data) =>
      {
        this.monitoramentos = data.items;

        for (let i = 0; i < data.items.length; i++)
        {
          this.monitoramentos[i].status = this._monitoramentoService.obterStatus(this.monitoramentos[i]);
          this.monitoramentos[i].descricao = this._monitoramentoService.obterDescricao(this.monitoramentos[i]);
        }

        this.monitoramentoChamados = this.monitoramentos.filter(m => m.tipo == 'CHAMADO');
        this.monitoramentoServicos = this.monitoramentos.filter(m => m.tipo == 'SERVICO');
        this.monitoramentoConexoes = this.monitoramentos.filter(m => m.tipo == 'CONEXAO');
        this.monitoramentoIntegracoes = this.monitoramentos.filter(m => m.tipo == 'INTEGRACAO');

        this.loading = false;  
        resolve(data);
      }, () =>
      {
        this.loading = false;
        reject();
      });
    })
  }

  obterOciosidadePorExtenso(dataHora: string): string
  {
    return moment(dataHora).locale('pt').fromNow();
  }
}
