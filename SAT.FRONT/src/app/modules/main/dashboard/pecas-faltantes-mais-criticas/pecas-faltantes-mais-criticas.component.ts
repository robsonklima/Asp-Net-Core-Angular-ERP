import { Component, OnInit } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { PecaService } from 'app/core/services/peca.service';
import { IndicadorAgrupadorEnum, Indicador, IndicadorTipoEnum, ChamadosPeca, DadosPeca } from 'app/core/types/indicador.types';
import { interval, Subject } from 'rxjs';
import moment from 'moment';
import { startWith, takeUntil } from 'rxjs/operators';
import { OrdemServico, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import { Peca } from 'app/core/types/peca.types';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import Enumerable from 'linq';

@Component({
  selector: 'app-pecas-faltantes-mais-criticas',
  templateUrl: './pecas-faltantes-mais-criticas.component.html',
  styleUrls: ['./pecas-faltantes-mais-criticas.component.css']
})
export class PecasFaltantesMaisCriticasComponent implements OnInit {
  public dadosPeca: DadosPeca;
  public chamadosPeca: ChamadosPeca;
  public osTopPecas: OrdemServico[] = [];
  public loading: boolean = true;
  public novasCad: boolean = false;
  public novasLib: boolean = false;
  public index: number = 0;
  public topPecasFaltantes: Indicador[] = [];
  public tabelaChamados = ['filial', 'ordemServico', 'dataSolucao', 'cliente', 'equipamento']
  protected _onDestroy = new Subject<void>();
  private _indicadorParams = {
    agrupador: IndicadorAgrupadorEnum.TOP_PECAS_FALTANTES,
    tipo: IndicadorTipoEnum.PECA_FALTANTE,
    dataInicio: moment().startOf('month').format('YYYY-MM-DD hh:mm'),
    dataFim: moment().endOf('month').format('YYYY-MM-DD hh:mm')
  }

  constructor(
    private _indicadorService: IndicadorService,
    private _pecaService: PecaService,
    private _osService: OrdemServicoService
  ) { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {
    this.loading = true;

    const topPecas = await this._indicadorService
      .obterPorParametros({
        ...this._indicadorParams,
        ...{ include: OrdemServicoIncludeEnum.OS_PECAS }
      }).toPromise();

    let codOsPecas = topPecas.map(t => t.filho.map(f => f.valor).join(','));
    this.osTopPecas = (await this._osService.obterPorParametros({ codOS: codOsPecas.toString() }).toPromise()).items;

    let codPecas = topPecas.map(item => item.label).join(',');
    const pecasInfo = (await this._pecaService.obterPorParametros({ codPeca: codPecas }).toPromise()).items;

    interval(30000)
      .pipe(
        startWith(0),
        takeUntil(this._onDestroy)
      )
      .subscribe(async () => {
        this.showPeca(pecasInfo[this.index], topPecas);
        this.loading = false;
        if (this.index > 8)
          this.index = 0;
        else this.index++;
      });
  }

  private showPeca(peca: Peca, topPecas: Indicador[]) {

    let topPecaAtual = topPecas.find(f => +f.label == peca.codPeca);

    let osPecas: ChamadosPeca[] = [];

    for (let c of topPecaAtual.filho) {
      let obj: ChamadosPeca = {
        filial: this.osTopPecas.find(f => f.codOS == c.valor).filial?.nomeFilial,
        ordemServico: c.valor.toString(),
        dataSolucao: c.label,
        cliente: this.osTopPecas.find(f => f.codOS == c.valor).cliente?.nomeFantasia,
        equipamento: this.osTopPecas.find(f => f.codOS == c.valor).equipamento?.nomeEquip
      }
      osPecas.push(obj);
    }

    this.dadosPeca = {
      codMagnus: peca.codMagnus,
      descricao: peca.nomePeca,
      quantidade: topPecaAtual.valor,
      index: this.index + 1,
      chamadosPeca: Enumerable.from(osPecas).orderByDescending(ord => ord.dataSolucao).toArray()
    }
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
