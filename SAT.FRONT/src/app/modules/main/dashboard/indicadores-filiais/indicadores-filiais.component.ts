import { AfterViewInit, ChangeDetectorRef, Component, Input, OnInit, ViewChild } from '@angular/core';
import { FilialService } from 'app/core/services/filial.service';
import { IndicadorService } from 'app/core/services/indicador.service'
import { Filial, FilialData } from 'app/core/types/filial.types';
import { Indicador, IndicadorAgrupadorEnum, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import moment from 'moment';
import { SharedService } from 'app/shared.service';
import { MapaComponent } from '../mapa/mapa.component';
import { OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import Enumerable from 'linq';
import { Filterable } from 'app/core/filters/filterable';
import { UserService } from 'app/core/user/user.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-indicadores-filiais',
  templateUrl: './indicadores-filiais.component.html',
  styleUrls: ['./indicadores-filiais.component.css'
  ]
})

export class IndicadoresFiliaisComponent extends Filterable implements OnInit, AfterViewInit, IFilterable {
  private filiais: Filial[] = [];
  public indicadoresFiliais: IndicadorFilial[] = [];
  public resultadoGeralDSS: IndicadorFilial;
  public loading: boolean = true;
  public selecionaLinhas: boolean = false;
  public ufSelecionada: string;

  @Input() sidenav: MatSidenav;

  constructor(
    private _sharedService: SharedService,
    private _filialService: FilialService,
    private _indicadorService: IndicadorService,
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef) {
    super(_userService, 'dashboard-filtro')
  }


  ngAfterViewInit(): void {
    // Faz o highlight das linhas pelo mapa
    this._sharedService.getClickEvent(MapaComponent).subscribe((params) => {
      this.ufSelecionada = params[0].estado;
      this.selecionaLinhas = params[0].seleciona;

      this._cdr.detectChanges();
    });

    this.registerEmitters();
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();

      this.indicadoresFiliais = [];
      this.resultadoGeralDSS = null;
      this.loading = true;

      this.obterFiliais();
    })
  }

  loadFilter(): void {
    super.loadFilter();
  }

  ngOnInit(): void {
    this.loading = true;
    this.obterFiliais();
  }

  private montaDashboard(): void {

    this.obterIndicadores().then(data => {
      this.filiais.forEach(filial => {
        let indicadorData = Enumerable.from(data);
        this.indicadoresFiliais.push({
          filial: filial.nomeFilial,
          siglaUF: filial.cidade.unidadeFederativa.siglaUF,
          sla: indicadorData.where(w => w.label == filial.nomeFilial && w.filho[0]?.label == "SLA").firstOrDefault()?.valor || 0,
          pendencia: indicadorData.where(w => w.label == filial.nomeFilial && w.filho[0]?.label == "Pendencia").firstOrDefault()?.valor || 0,
          reincidencia: indicadorData.where(w => w.label == filial.nomeFilial && w.filho[0]?.label == "Reincidencia").firstOrDefault()?.valor || 0,
          spa: indicadorData.where(w => w.label == filial.nomeFilial && w.filho[0]?.label == "SPA").firstOrDefault()?.valor || 0
        })
      })

      this.indicadoresFiliais.sort((a, b) => (a.sla > b.sla ? -1 : 1));
      this.calculaResultadoGeralDSS();
      this.loading = false;
    });
  }

  private calculaResultadoGeralDSS(): void {
    var nroFiliais: number = this.indicadoresFiliais.length > 0 ? this.indicadoresFiliais.length : 1;
    var avgSLA: number = 0;
    var avgSPA: number = 0;
    var avgReincidencia: number = 0;
    var avgPendencia: number = 0;

    this.indicadoresFiliais.forEach(d => {
      avgSLA += d.sla;
      avgSPA += d.spa;
      avgReincidencia += d.reincidencia;
      avgPendencia += d.pendencia;
    });

    this.resultadoGeralDSS =
    {
      filial: "dss",
      sla: Math.round(avgSLA / nroFiliais),
      pendencia: Math.round(avgPendencia / nroFiliais),
      reincidencia: Math.round(avgReincidencia / nroFiliais),
      spa: Math.round(avgSPA / nroFiliais)
    }
  }

  private async obterFiliais() {
    this._filialService.obterPorParametros({ indAtivo: 1 }).subscribe((data: FilialData) => {
      // Remover EXP,OUT,IND
      this.filiais = data.items.filter((f) => f.codFilial != 7 && f.codFilial != 21 && f.codFilial != 33);

      // Monta o dashboard
      this.montaDashboard();
    });
  }

  private async obterIndicadores(): Promise<Indicador[]> {

    const params = {
      tipo: IndicadorTipoEnum.PERFORMANCE_FILIAIS,
      agrupador: IndicadorAgrupadorEnum.FILIAL,
      include: OrdemServicoIncludeEnum.OS_RAT_FILIAL_PRAZOS_ATENDIMENTO,
      filterType: OrdemServicoFilterEnum.FILTER_INDICADOR,
      dataInicio: this.filter?.parametros.dataInicio || moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      dataFim: this.filter?.parametros.dataFim || moment().endOf('month').format('YYYY-MM-DD hh:mm'),
      codFiliais: Enumerable.from(this.filiais).select(t => t.codFilial).distinct().toArray().join(',')
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
