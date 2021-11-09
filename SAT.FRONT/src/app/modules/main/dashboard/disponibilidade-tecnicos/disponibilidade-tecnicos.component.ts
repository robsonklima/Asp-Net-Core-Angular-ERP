import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { FilialService } from 'app/core/services/filial.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Cidade } from 'app/core/types/cidade.types';
import { DashboardTecnicoDisponibilidadeFilialViewModel, Filial, FilialFilterEnum, FilialIncludeEnum } from 'app/core/types/filial.types';
import { Filtro } from 'app/core/types/filtro.types';
import { DashboardTecnicoDisponibilidadeTecnicoViewModel, Tecnico, TecnicoFilterEnum, TecnicoIncludeEnum } from 'app/core/types/tecnico.types';
import Enumerable from 'linq';
import moment from 'moment';

@Component({
  selector: 'app-disponibilidade-tecnicos',
  templateUrl: './disponibilidade-tecnicos.component.html',
  styleUrls: ['./disponibilidade-tecnicos.component.css'
  ]
})

export class DisponibilidadeTecnicosComponent implements OnInit {
  @Input() filtro: Filtro;
  public loading: boolean = true;
  public disponibilidadeTecnicosModel: DisponibilidadeTecnicosModel[] = [];

  public totalTecnicosAtivos: number = 0;
  public totalTecnicosSemChamadosTransf: number = 0;
  public totalInativos: number = 0;
  public totalTecnicos: number = 0;
  public totalOsNaoTransf: number = 0;
  public totalMediaAtendimento: number = 0;
  public totalMediaAtendimentoCorretivo: number = 0;
  public totalMediaAtendimentoPreventivo: number = 0;

  constructor(private _cdr: ChangeDetectorRef,
    private _tecnicoService: TecnicoService,
    private _filialService: FilialService) { }

  ngOnInit(): void {
    this.obterTecnicos();
  }

  private async obterTecnicos() {

    this.loading = true;
    let dataInicio = moment().add(-30, 'days').format('yyyy-MM-DD HH:mm:ss');
    let dataFim = moment().format('yyyy-MM-DD HH:mm:ss');

    let dadosTecnicosDashboard = (await this._tecnicoService
      .obterPorParametros({
        filterType: TecnicoFilterEnum.FILTER_TECNICO_OS,
        include: TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO,
        periodoMediaAtendInicio: dataInicio,
        periodoMediaAtendFim: dataFim
      }).toPromise()).items as DashboardTecnicoDisponibilidadeTecnicoViewModel[];

    for (let tecnico of dadosTecnicosDashboard) {

      let dadosDashboard = Enumerable.from(this.disponibilidadeTecnicosModel).firstOrDefault(c => c.filial.codFilial == tecnico.filial.codFilial);

      /** DADOS DOS TÉCNICOS **/
      if (!dadosDashboard) {
        dadosDashboard = new DisponibilidadeTecnicosModel();
        // Nome Filial
        dadosDashboard.filial = tecnico.filial;
        // Quantidade de técnicos ativos da filial
        dadosDashboard.qntTecnicosAtivosChamados = Enumerable.from(dadosTecnicosDashboard).count(c => c.filial.codFilial == tecnico.filial.codFilial && c.indFerias == 0);
        // Quantidade de técnicos inativos da filial
        dadosDashboard.qntTecnicosInativos = Enumerable.from(dadosTecnicosDashboard).count(c => c.filial.codFilial == tecnico.filial.codFilial && c.indFerias == 1);
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
    }

    /** DADOS DAS FILIAIS **/
    let filialTecnico = (await this._filialService.obterPorParametros({
      indAtivo: 1,
      codFiliais: Enumerable.from(dadosTecnicosDashboard).select(t => t.filial.codFilial).distinct().toArray().join(','),
      include: FilialIncludeEnum.FILIAL_ORDENS_SERVICO,
      filterType: FilialFilterEnum.FILTER_DASHBOARD_DISPONIBILIDADE_TECNICOS,
      periodoInicioAtendendimento: dataInicio,
      periodoFimAtendendimento: dataFim
    }).toPromise()).items as DashboardTecnicoDisponibilidadeFilialViewModel[];

    for (let filial of filialTecnico) {
      let dadosFilial = Enumerable.from(this.disponibilidadeTecnicosModel).firstOrDefault(c => c.filial.codFilial == filial.codFilial);
      dadosFilial.qtdOSNaoTransferidasCorretivas = filial.qtdOSNaoTransferidasCorretivas;
      dadosFilial.mediaAtendimentoTodos = dadosFilial.mediaAtendimentoTodos / dadosFilial.qntTotalTecnicos;
      dadosFilial.mediaAtendimentoCorretivo = dadosFilial.mediaAtendimentoCorretivo / dadosFilial.qntTotalTecnicos;
      dadosFilial.mediaAtendimentoPreventivo = dadosFilial.mediaAtendimentoPreventivo / dadosFilial.qntTotalTecnicos;
    }

    this.disponibilidadeTecnicosModel = Enumerable.from(this.disponibilidadeTecnicosModel).orderByDescending(o => o.filial.nomeFilial).toArray();

    this.totalTecnicosAtivos = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.qntTecnicosAtivosChamados);
    this.totalTecnicosSemChamadosTransf = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.qntTecnicosAtivosSemChamadosTransferidos);
    this.totalInativos = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.qntTecnicosInativos);
    this.totalTecnicos = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.qntTotalTecnicos);
    this.totalOsNaoTransf = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.qtdOSNaoTransferidasCorretivas);
    this.totalMediaAtendimento = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.mediaAtendimentoTodos / this.disponibilidadeTecnicosModel.length);
    this.totalMediaAtendimentoCorretivo = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.mediaAtendimentoCorretivo / this.disponibilidadeTecnicosModel.length);
    this.totalMediaAtendimentoPreventivo = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.mediaAtendimentoPreventivo / this.disponibilidadeTecnicosModel.length);

    this.disponibilidadeTecnicosModel = Enumerable.from(this.disponibilidadeTecnicosModel).orderBy(o => o.filial).toArray();
    this.loading = false;
    this._cdr.detectChanges();
  }
}

export class DisponibilidadeTecnicosModel {
  filial: Filial;
  qntTecnicosAtivosChamados: number = 0;
  qntTecnicosAtivosSemChamadosTransferidos: number = 0;
  qntTecnicosInativos: number = 0;
  qntTotalTecnicos: number = 0;
  qtdOSNaoTransferidasCorretivas: number = 0;
  mediaAtendimentoTodos: number = 0;
  mediaAtendimentoCorretivo: number = 0;
  mediaAtendimentoPreventivo: number = 0;
  mediaAtendimentoInstalacao: number = 0;
  mediaAtendimentoEngenharia: number = 0;
}
