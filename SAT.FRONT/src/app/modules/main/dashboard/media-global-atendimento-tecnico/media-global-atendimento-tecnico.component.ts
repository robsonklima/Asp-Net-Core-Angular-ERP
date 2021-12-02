import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Filtro } from 'app/core/types/filtro.types';
import { DashboardTecnicoDisponibilidadeTecnicoViewModel, TecnicoFilterEnum, TecnicoIncludeEnum } from 'app/core/types/tecnico.types';
import Enumerable from 'linq';
import moment from 'moment';
import { DisponibilidadeTecnicosModel } from '../disponibilidade-tecnicos/disponibilidade-tecnicos.component';

@Component({
  selector: 'app-media-global-atendimento-tecnico',
  templateUrl: './media-global-atendimento-tecnico.component.html',
  styleUrls: ['./media-global-atendimento-tecnico.component.css'
  ]
})

export class MediaGlobalAtendimentoTecnicoComponent implements OnInit {
  @Input() filtro: Filtro;
  public mediaGlobalAtendimentoTecnicosModel: MediaGlobalAtendimentoTecnicosModel = new MediaGlobalAtendimentoTecnicosModel();
  public loading: boolean = true;
  public disponibilidadeTecnicosModel: DisponibilidadeTecnicosModel[] = [];

  constructor(private _cdr: ChangeDetectorRef,
    private _tecnicoService: TecnicoService,
    private _indicadorService: IndicadorService,) { }

  ngOnInit(): void {
    this.obterDados();
  }

  async obterDados() {
    this.loading = true;

    // let dadosTecnicosDashboard = (await this._tecnicoService
    //   .obterPorParametros({
    //     filterType: TecnicoFilterEnum.FILTER_TECNICO_OS,
    //     include: TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO,
    //     periodoMediaAtendInicio: moment().add(-30, 'days').format('yyyy-MM-DD HH:mm:ss'), // Ultimos 30 dias
    //     periodoMediaAtendFim: moment().format('yyyy-MM-DD HH:mm:ss')
    //   }).toPromise()).items as DashboardTecnicoDisponibilidadeTecnicoViewModel[];

    let dadosTecnicosDashboard = (await this._indicadorService.obterIndicadoresDisponibilidadeTecnicos().toPromise());


    for (let tecnico of dadosTecnicosDashboard) {

      let dadosDashboard = Enumerable.from(this.disponibilidadeTecnicosModel).firstOrDefault(c => c.codFilial == tecnico.codFilial);

      /** DADOS DOS TÉCNICOS **/
      if (!dadosDashboard) {
        dadosDashboard = new DisponibilidadeTecnicosModel();
        // Nome Filial
        dadosDashboard.nomeFilial = tecnico.nomeFilial;
        // Quantidade de técnicos ativos da filial
        dadosDashboard.qntTecnicosAtivosChamados = Enumerable.from(dadosTecnicosDashboard).count(c => c.codFilial == tecnico.codFilial && c.indFerias == 0);
        // Quantidade de técnicos inativos da filial
        dadosDashboard.qntTecnicosInativos = Enumerable.from(dadosTecnicosDashboard).count(c => c.codFilial == tecnico.codFilial && c.indFerias == 1);
        // Quantidade de técnicos total da filial
        dadosDashboard.qntTotalTecnicos = dadosDashboard.qntTecnicosAtivosChamados + dadosDashboard.qntTecnicosInativos;

        this.disponibilidadeTecnicosModel.push(dadosDashboard);
      }

      // Quantidade de técnicos sem OS Transferidas
      if (tecnico.tecnicoSemChamadosTransferidos) {
        dadosDashboard.qntTecnicosAtivosSemChamadosTransferidos++;
        dadosDashboard.qntTecnicosAtivosChamados--;
      }

      dadosDashboard.mediaAtendimentoTodos += tecnico.mediaAtendimentosPorDiaTodasIntervencoes;
      dadosDashboard.mediaAtendimentoCorretivo += tecnico.mediaAtendimentosPorDiaCorretivos;
      dadosDashboard.mediaAtendimentoPreventivo += tecnico.mediaAtendimentosPorDiaPreventivos;
      dadosDashboard.mediaAtendimentoInstalacao += tecnico.mediaAtendimentosPorDiaInstalacoes;
      dadosDashboard.mediaAtendimentoEngenharia += tecnico.mediaAtendimentosPorDiaEngenharia;
    }

    this.mediaGlobalAtendimentoTecnicosModel.qntTodasIntervencoes = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => (s.mediaAtendimentoTodos / s.qntTotalTecnicos) / this.disponibilidadeTecnicosModel.length);
    this.mediaGlobalAtendimentoTecnicosModel.qntCorretivos = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => (s.mediaAtendimentoCorretivo / s.qntTotalTecnicos) / this.disponibilidadeTecnicosModel.length);
    this.mediaGlobalAtendimentoTecnicosModel.qntPreventivos = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => (s.mediaAtendimentoPreventivo / s.qntTotalTecnicos) / this.disponibilidadeTecnicosModel.length);
    this.mediaGlobalAtendimentoTecnicosModel.qntInstalacoes = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => (s.mediaAtendimentoInstalacao / s.qntTotalTecnicos) / this.disponibilidadeTecnicosModel.length);
    this.mediaGlobalAtendimentoTecnicosModel.qntAltEngenharia = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => (s.mediaAtendimentoEngenharia / s.qntTotalTecnicos) / this.disponibilidadeTecnicosModel.length);

    this.loading = false;
    this._cdr.detectChanges();
  }
}

export class MediaGlobalAtendimentoTecnicosModel {
  qntTodasIntervencoes: number = 0;
  qntCorretivos: number = 0;
  qntPreventivos: number = 0;
  qntInstalacoes: number = 0;
  qntAltEngenharia: number = 0;
}