import { Component, OnInit } from '@angular/core';
import { MonitoramentoService } from 'app/core/services/monitoramento.service';
import { Monitoramento } from 'app/core/types/monitoramento.types';
import moment from 'moment';

@Component({
  selector: 'app-servicos',
  templateUrl: './servicos.component.html'
})
export class ServicosComponent implements OnInit {
  listaMonitoramento: Monitoramento[] = [];

  constructor(
    private _monitoramentoService: MonitoramentoService,
  ) { }

  async ngOnInit() {
    await this.obterMonitoramentos();
  }

  private obterMonitoramentos(): Promise<any>
  {
      return new Promise((resolve, reject) =>
      {
          this._monitoramentoService.obterPorParametros({
              sortActive: "dataHoraProcessamento",
              sortDirection: "asc",
          }).subscribe((data) =>
          {
              this.listaMonitoramento = data.items;
              for (let i = 0; i < data.items.length; i++)
              {
                  this.listaMonitoramento[i].status = this._monitoramentoService.obterStatus(this.listaMonitoramento[i]);
                  this.listaMonitoramento[i].descricao = this._monitoramentoService.obterDescricao(this.listaMonitoramento[i]);
              }
              resolve(data);
          }, () =>
          {
              reject();
          });
      })
  }

  filtrarMonitoramento(tipo: string)
  {
      return this.listaMonitoramento.filter(m => m.tipo == tipo)
  }

  obterOciosidadePorExtenso(dataHora: string): string
  {
      return moment(dataHora).locale('pt').fromNow();
  }
}
