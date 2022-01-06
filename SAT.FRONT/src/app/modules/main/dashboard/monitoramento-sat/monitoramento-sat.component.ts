import { Component, OnInit } from '@angular/core';
import { MonitoramentoService } from 'app/core/services/monitoramento.service';
import { MonitoramentoCliente } from 'app/core/types/monitoramento.types';

@Component({
  selector: 'app-monitoramento-sat',
  templateUrl: './monitoramento-sat.component.html'
})
export class MonitoramentoSatComponent implements OnInit
{
  public loading: boolean;
  public listaMonitoramentoClientes: MonitoramentoCliente[] = [];

  constructor (
    private _monitoramentoService: MonitoramentoService
  ) { }

  ngOnInit(): void
  {
    this.loading = true;
    this._monitoramentoService.obterPorParametros({}).subscribe(data =>
    {
      this.listaMonitoramentoClientes = data;
      this.loading = false;
    });
  }
}