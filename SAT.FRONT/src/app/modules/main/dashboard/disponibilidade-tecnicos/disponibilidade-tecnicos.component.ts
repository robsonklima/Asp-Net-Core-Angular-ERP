import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { FilialService } from 'app/core/services/filial.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Filtro } from 'app/core/types/filtro.types';
import { Tecnico } from 'app/core/types/tecnico.types';
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

    let listaTecnicos = (await this._tecnicoService
      .obterPorParametros({})
      .toPromise()).items;

    // Remover EXP,OUT,IND
    let filiais = (await this._filialService.obterPorParametros({ indAtivo: 1 }).toPromise()).items.
      filter(g =>
        g.codFilial != null && g.codFilial != 7 && g.codFilial != 21 && g.codFilial != 33).map(m => m.codFilial);

    let groupList = listaTecnicos.filter(g => filiais.includes(g.codFilial)).groupBy('codFilial');

    for (let grupo of groupList) {

      let dado = new DisponibilidadeTecnicosModel();
      let qntTodosAtendimentos = 0;
      let qntTodosAtendimentosCorretiva = 0;
      let qntTodosAtendimentosPreventiva = 0;
      let valores = groupList.find(k => k.key == grupo.key).value;

      // Dados dos técnicos
      if (valores) {

        for (let tecnico of valores.filter(m => m.indAtivo && m.usuario?.indAtivo)) {

          if (!dado.filial) {
            dado.filial = tecnico.filial.nomeFilial; // Nome Filial
          }

          dado.totalQntTecnicos++;

          if (tecnico.indFerias) {
            dado.qntInativos++;
          }
          else {
            let listaOSTecnico = await this._ordemServicoService.obterPorParametros({
              codTecnico: tecnico.codTecnico,
              dataAberturaInicio: '2021-08-01', //moment().startOf('month').format('YYYY-MM-DD hh:mm'),
              dataAberturaFim: moment().endOf('month').format('YYYY-MM-DD hh:mm'),
            }).toPromise();

            if (listaOSTecnico.items?.length > 0) {

              dado.qntAtivosChamados++;

              qntTodosAtendimentos += listaOSTecnico.items.map(os => os.relatoriosAtendimento).length;
              qntTodosAtendimentosCorretiva += listaOSTecnico.items.filter(os => os.codTipoIntervencao == 2).length;
              qntTodosAtendimentosPreventiva += listaOSTecnico.items.filter(os => os.codTipoIntervencao == 6).length;

              let existeChamadoTransferido = listaOSTecnico.items.some(os => os.codStatusServico == 8);

              if (!existeChamadoTransferido) {
                dado.qntAtivosSemChamadosTransferidos++;
              }

              dado.qntOsNaoTransferidas =
                listaOSTecnico.items.filter(os =>
                  os.codTipoIntervencao == 2 &&
                  os.relatoriosAtendimento.filter(rat =>
                    rat.statusServico.codStatusServico != 2 &&
                    rat.statusServico.codStatusServico != 8 &&
                    moment(rat.dataHoraSolucao, 'YYYY-MM-DD') > moment().add(-30, 'days'))).length;

            }
          }
        }
        dado.mediaAtendimentoTodos = ((qntTodosAtendimentos / dado.qntAtivosChamados) / 5) || 0;
        dado.mediaAtendimentoCorretivo = ((qntTodosAtendimentosCorretiva / dado.qntAtivosChamados) / 5) || 0;
        dado.mediaAtendimentoPreventivo = ((qntTodosAtendimentosPreventiva / dado.qntAtivosChamados) / 5) || 0;
        this.disponibilidadeTecnicosModel.push(dado);
      }
    }

    this.totalTecnicosAtivos = this.disponibilidadeTecnicosModel.sum('qntAtivosChamados');
    this.totalTecnicosSemChamadosTransf = this.disponibilidadeTecnicosModel.sum('qntAtivosSemChamadosTransferidos');
    this.totalInativos = this.disponibilidadeTecnicosModel.sum('qntInativos')
    this.totalTecnicos = this.disponibilidadeTecnicosModel.sum('totalQntTecnicos')
    this.totalOsNaoTransf = this.disponibilidadeTecnicosModel.sum('qntOsNaoTransferidas')
    this.totalMediaAtendimento = this.disponibilidadeTecnicosModel.sum('mediaAtendimentoTodos')
    this.totalMediaAtendimentoCorretivo = this.disponibilidadeTecnicosModel.sum('mediaAtendimentoCorretivo')
    this.totalMediaAtendimentoPreventivo = this.disponibilidadeTecnicosModel.sum('mediaAtendimentoPreventivo')

    this.loading = false;
    this._cdr.detectChanges();
  }
}

export class DisponibilidadeTecnicosModel {
  filial: string;
  qntAtivosChamados: number = 0;
  qntAtivosSemChamadosTransferidos: number = 0;
  qntInativos: number = 0;
  totalQntTecnicos: number = 0;
  qntOsNaoTransferidas: number = 0;
  mediaAtendimentoTodos: number = 0;
  mediaAtendimentoCorretivo: number = 0;
  mediaAtendimentoPreventivo: number = 0;
}