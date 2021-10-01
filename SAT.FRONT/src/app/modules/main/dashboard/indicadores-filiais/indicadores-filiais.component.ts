import { AfterViewInit, ChangeDetectorRef, Component, NgZone, OnInit } from '@angular/core';
import { FilialService } from 'app/core/services/filial.service';
import { IndicadorService } from 'app/core/services/indicador.service'
import { Filial, FilialData } from 'app/core/types/filial.types';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import moment from 'moment';
import { forkJoin } from 'rxjs';
import { SharedService } from 'app/shared.service';
import { MapaComponent } from '../mapa/mapa.component';

@Component({
  selector: 'app-indicadores-filiais',
  templateUrl: './indicadores-filiais.component.html',
  styleUrls: ['./indicadores-filiais.component.css'
  ]
})

export class IndicadoresFiliaisComponent implements OnInit, AfterViewInit {

  private filiais: Filial[] = [];

  public element_data: IndicadorFilial[] = [];
  public resultado_geral_dss: IndicadorFilial;
  public loading: boolean = true;
  public selecionaLinhas: boolean = false;
  public ufSelecionada: string;

  constructor(
    private _sharedService: SharedService,
    private _filialService: FilialService,
    private _indicadorService: IndicadorService,
    private _cdr: ChangeDetectorRef) {
  }

  ngAfterViewInit(): void {
    // Faz o highlight das linhas pelo mapa
    this._sharedService.getClickEvent(MapaComponent).subscribe((params) => {
      this.ufSelecionada = params[0].estado;
      this.selecionaLinhas = params[0].seleciona;

      this._cdr.detectChanges();
    });
  }

  ngOnInit(): void {
    this.loading = true;
    this.obterFiliais();
    this.montaDashboard();
  }

  private montaDashboard(): void {
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
            spa: data.spa.filter((pend) => pend.label == filial.nomeFilial).map((valor) => valor.valor).shift() || 0,
            siglaUF: filial.cidade.unidadeFederativa.siglaUF
          })
        });

        this.element_data.sort((a, b) => (a.sla > b.sla ? -1 : 1));
        this.loading = false;
        this.calculaResultadoGeralDSS();
      });
  }

  private calculaResultadoGeralDSS(): void {
    var nroFiliais: number = this.element_data.length > 0 ? this.element_data.length : 1;
    var avgSLA: number = 0;
    var avgSPA: number = 0;
    var avgReincidencia: number = 0;
    var avgPendencia: number = 0;

    this.element_data.forEach(d => {
      avgSLA += d.sla;
      avgSPA += d.spa;
      avgReincidencia += d.reincidencia;
      avgPendencia += d.pendencia;
    });

    this.resultado_geral_dss =
    {
      filial: "dss",
      sla: Math.round(avgSLA / nroFiliais),
      pendencia: Math.round(avgPendencia / nroFiliais),
      reincidencia: Math.round(avgReincidencia / nroFiliais),
      spa: Math.round(avgSPA / nroFiliais)
    }
  }

  private obterFiliais(): void {
    this._filialService.obterPorParametros({}).subscribe((data: FilialData) => {
      // Remover EXP e IND
      this.filiais = data.items.filter((f) => f.codFilial != 7 && f.codFilial != 33);
    });
  }

  private obterDados(indicadorTipoEnum: IndicadorTipoEnum): Promise<Indicador[]> {
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
  siglaUF?: string;
  sla: number;
  pendencia: number;
  reincidencia: number;
  spa: number;
}
