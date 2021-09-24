import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { FilialService } from 'app/core/services/filial.service';
import { IndicadorService } from 'app/core/services/indicador.service'
import { Filial, FilialData } from 'app/core/types/filial.types';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import moment from 'moment';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-indicadores-filiais',
  templateUrl: './indicadores-filiais.component.html',
  styleUrls: ['./indicadores-filiais.component.css'
  ]
})

export class IndicadoresFiliaisComponent {

  displayedColumns: string[] = ['status', 'filial', 'sla', 'pendencia', 'reincidencia', 'spa'];
  element_data: IndicadorFilial[] = [];
  loading: boolean = true;
  filiais: Filial[] = [];

  constructor(
    private _filialService: FilialService,
    private _indicadorService: IndicadorService) { }

  ngOnInit(): void {
    this.loading = true;
    this.obterFiliais();
    this.montaDashboard();
  }

  montaDashboard(): void {
    forkJoin(
      {
        sla: this.obterDados(IndicadorTipoEnum.SLA),
        pendencia: this.obterDados(IndicadorTipoEnum.PENDENCIA),
        reincidencia: this.obterDados(IndicadorTipoEnum.REINCIDENCIA),
        spa: this.obterDados(IndicadorTipoEnum.SPA)
      }).subscribe(data => {

        this.filiais.forEach(filial => {
          this.element_data.push({
            filial: filial.nomeFilial,
            sla: data.sla.filter((pend) => pend.label == filial.nomeFilial).map((valor) => valor.valor).shift() || 0,
            pendencia: data.pendencia.filter((pend) => pend.label == filial.nomeFilial).map((valor) => valor.valor).shift() || 0,
            reincidencia: data.reincidencia.filter((pend) => pend.label == filial.nomeFilial).map((valor) => valor.valor).shift() || 0,
            spa: data.spa.filter((pend) => pend.label == filial.nomeFilial).map((valor) => valor.valor).shift() || 0
          })
        });

        this.element_data.sort((a, b) => (a.sla > b.sla ? -1 : 1));
        this.loading = false;
      });
  }

  obterFiliais(): void {
    this._filialService.obterPorParametros({}).subscribe((data: FilialData) => {
      // Remover EXP e IND
      this.filiais = data.items.filter((f) => f.codFilial != 7 && f.codFilial != 33);
    });
  }

  obterDados(indicadorTipoEnum: IndicadorTipoEnum): Promise<Indicador[]> {
    const params = {
      tipo: indicadorTipoEnum,
      agrupador: IndicadorAgrupadorEnum.FILIAL,
      codAutorizadas: "",
      codTiposGrupo: "",
      codTiposIntervencao: "",
      dataInicio: moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      dataFim: moment().endOf('month').format('YYYY-MM-DD hh:mm')
    }

    return this._indicadorService.obterPorParametros(params).toPromise();
  }
}

export interface IndicadorFilial {
  filial: string;
  sla: number;
  pendencia: number;
  reincidencia: number;
  spa: number;
}
