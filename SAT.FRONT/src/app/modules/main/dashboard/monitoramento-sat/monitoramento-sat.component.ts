import { Component, OnInit } from '@angular/core';
import { MonitoramentoService } from 'app/core/services/monitoramento.service';
import { Monitoramento, MonitoramentoTipoEnum } from 'app/core/types/monitoramento.types';

@Component({
  selector: 'app-monitoramento-sat',
  templateUrl: './monitoramento-sat.component.html'
})
export class MonitoramentoSatComponent implements OnInit
{
  public loading: boolean;
  public listaMonitoramentoClientes: Monitoramento[] = [];

  constructor (
    private _monitoramentoService: MonitoramentoService
  ) { }

  ngOnInit(): void
  {
    this.loading = true;
    this._monitoramentoService.obterPorParametros({ tipo: MonitoramentoTipoEnum.CLIENTE }).subscribe(data =>
    {
      this.listaMonitoramentoClientes = data;
      this.loading = false;
    });
  }
}