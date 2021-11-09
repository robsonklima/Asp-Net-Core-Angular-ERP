import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { FilialService } from 'app/core/services/filial.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Cidade } from 'app/core/types/cidade.types';
import { Filial, FilialFilterEnum, FilialIncludeEnum } from 'app/core/types/filial.types';
import { Filtro } from 'app/core/types/filtro.types';
import { OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import { statusServicoConst } from 'app/core/types/status-servico.types';
import { Tecnico, TecnicoFilterEnum, TecnicoIncludeEnum } from 'app/core/types/tecnico.types';
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
  public resultadoGeralTotal: Tecnico;
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
    private _ordemServicoService: OrdemServicoService,
    private _filialService: FilialService) { }

  ngOnInit(): void {
    this.obterTecnicos();
  }

  private async obterTecnicos() {

    this.loading = true;
    let dataInicio = moment().add(-30, 'days').format('yyyy-MM-DD HH:mm:ss');
    let dataFim = moment().format('yyyy-MM-DD HH:mm:ss');

    let dadosTecnicosDashboard = Enumerable.from((await this._tecnicoService
      .obterPorParametros({
        filterType: TecnicoFilterEnum.FILTER_TECNICO_OS,
        periodoMediaAtendInicio: dataInicio,
        periodoMediaAtendFim: dataFim
      }).toPromise()).items as DashboardTecnicoDisponibilidadeTecnicoViewModel[])
      .where(tec =>
        tec.usuario != undefined && tec.filial != undefined && tec.usuario.codFilial != undefined &&
        tec.usuario?.indAtivo == 1 && tec.indAtivo == 1
      ).toArray();
//debugger; lista n trazendo dados?
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

        // TERMINAR A REGRA

        dadosDashboard.mediaAtendimentoTodos = tecnico.qtdChamadosAtendidosTodasIntervencoes;
        dadosDashboard.mediaAtendimentoCorretivo = tecnico.qtdChamadosAtendidosSomenteCorretivos;
        dadosDashboard.mediaAtendimentoPreventivo = tecnico.qtdChamadosAtendidosSomentePreventivos;

        this.disponibilidadeTecnicosModel.push(dadosDashboard);
      }

      // Quantidade de técnicos sem OS Transferidas
      if (Enumerable.from(tecnico.ordensServico).count(w => w.codStatusServico == statusServicoConst.TRANSFERIDO) == 0) {
        dadosDashboard.qntTecnicosAtivosSemChamadosTransferidos++;
        dadosDashboard.qntTecnicosAtivosChamados--;
      }
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
      Enumerable.from(this.disponibilidadeTecnicosModel).firstOrDefault(c => c.filial.codFilial == filial.codFilial)
        .qtdOSNaoTransferidasCorretivas = filial.qtdOSNaoTransferidasCorretivas;
    }

    this.disponibilidadeTecnicosModel = Enumerable.from(this.disponibilidadeTecnicosModel).orderByDescending(o => o.filial.nomeFilial).toArray();

    // Remover EXP,OUT,IND
    // let filiais = (await this._filialService.obterPorParametros({ indAtivo: 1 }).toPromise()).items.
    //   filter(g =>
    //     g.codFilial != null && g.codFilial != 7 && g.codFilial != 21 && g.codFilial != 33).map(m => m.codFilial);

    // let groupList = Enumerable.from(dadosTecnicosDashboard.filter(g =>
    //   filiais.includes(g.filial?.codFilial))).groupBy(gr => gr.filial?.codFilial).toArray();

    // debugger;
    // for (let grupo of Enumerable.from(dadosTecnicosDashboard).groupBy(gr => gr.filial?.codFilial)) {

    //   let dado = new DisponibilidadeTecnicosModel();
    //   let qntTodosAtendimentos = 0;
    //   let qntTodosAtendimentosCorretiva = 0;
    //   let qntTodosAtendimentosPreventiva = 0;
    //   // let valores = Enumerable.from(dadosTecnicosDashboard).where(k => k.filial?.codFilial == grupo.key).toArray();
    //   //  debugger;
    //   // Dados dos técnicos
    //   // if (grupo) {
    //   //debugger;
    //   for (let tecnico of grupo.getSource().filter(t => t.indTecnicoAtivo == 1 && t.indUsuarioAtivo == 1)) {

    //     if (!dado.filial) {
    //       dado.filial = tecnico.filial.nomeFilial; // Nome Filial
    //     }

    //     dado.totalQntTecnicos++;

    //     if (tecnico.indFerias) {
    //       dado.qntInativos++;
    //     }
    //     else {

    //       // let listaOSTecnico = await this._ordemServicoService.obterPorParametros({
    //       //   codTecnico: tecnico.codTecnico,
    //       //   dataAberturaInicio: moment().startOf('month').format('YYYY-MM-DD hh:mm'),
    //       //   dataAberturaFim: moment().endOf('month').format('YYYY-MM-DD hh:mm'),
    //       //   include: OrdemServicoIncludeEnum.OS_TECNICOS
    //       // }).toPromise();

    //       // if (listaOSTecnico.items?.length > 0) {

    //       // dado.qntAtivosChamados++;

    //       // qntTodosAtendimentos += tecnico.qtdChamadosTotal;
    //       // qntTodosAtendimentosCorretiva += tecnico.qtdChamadosCorretivos;
    //       // qntTodosAtendimentosPreventiva += tecnico.qtdChamadosPreventivos;

    //       // dado.qntAtivosSemChamadosTransferidos += tecnico.qtdChamadosTransferidos;

    //       // dado.qntOsNaoTransferidas = 
    //       //   listaOSTecnico.items.filter(os =>
    //       //     os.codTipoIntervencao == 2 &&
    //       //     os.relatoriosAtendimento.filter(rat =>
    //       //       rat.statusServico.codStatusServico != 2 &&
    //       //       rat.statusServico.codStatusServico != 8 &&
    //       //       moment(rat.dataHoraSolucao, 'YYYY-MM-DD') > moment().add(-30, 'days'))).length;

    //       // }
    //     }
    //   }
    //   dado.mediaAtendimentoTodos = ((qntTodosAtendimentos / dado.qntAtivosChamados) / 5) || 0;
    //   dado.mediaAtendimentoCorretivo = ((qntTodosAtendimentosCorretiva / dado.qntAtivosChamados) / 5) || 0;
    //   dado.mediaAtendimentoPreventivo = ((qntTodosAtendimentosPreventiva / dado.qntAtivosChamados) / 5) || 0;
    //   this.disponibilidadeTecnicosModel.push(dado);
    // }
    //}

    this.totalTecnicosAtivos = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.qntTecnicosAtivosChamados);
    this.totalTecnicosSemChamadosTransf = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.qntTecnicosAtivosSemChamadosTransferidos);
    this.totalInativos = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.qntTecnicosInativos);
    this.totalTecnicos = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.qntTotalTecnicos);
    this.totalOsNaoTransf = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.qtdOSNaoTransferidasCorretivas);
    this.totalMediaAtendimento = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.mediaAtendimentoTodos);
    this.totalMediaAtendimentoCorretivo = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.mediaAtendimentoCorretivo);
    this.totalMediaAtendimentoPreventivo = Enumerable.from(this.disponibilidadeTecnicosModel).sum(s => s.mediaAtendimentoPreventivo);

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
}

export class DashboardTecnicoDisponibilidadeFilialViewModel implements Filial {
  codFilial: number;
  razaoSocial: string;
  nomeFilial: string;
  cidade: Cidade;
  endereco: string;
  cep: string;
  qtdOSNaoTransferidasCorretivas: number;
}

export class DashboardTecnicoDisponibilidadeTecnicoViewModel extends Tecnico {
  filial: Filial;
  qtdChamadosAtendidosTodasIntervencoes: number;
  qtdChamadosAtendidosSomenteCorretivos: number;
  qtdChamadosAtendidosSomentePreventivos: number;
  qtdChamadosAtendidosTodasIntervencoesDia: number;
}