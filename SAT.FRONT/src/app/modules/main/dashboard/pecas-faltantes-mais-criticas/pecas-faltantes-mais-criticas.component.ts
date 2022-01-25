import { Component, Input, OnInit } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { PecaService } from 'app/core/services/peca.service';
import { IndicadorAgrupadorEnum, Indicador, IndicadorTipoEnum, ChamadosPeca, DadosPeca } from 'app/core/types/indicador.types';
import { interval, Subject } from 'rxjs';
import moment from 'moment';
import { startWith, takeUntil } from 'rxjs/operators';
import { OrdemServico, OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import { Peca } from 'app/core/types/peca.types';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import Enumerable from 'linq';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-pecas-faltantes-mais-criticas',
  templateUrl: './pecas-faltantes-mais-criticas.component.html',
  styleUrls: ['./pecas-faltantes-mais-criticas.component.css']
})
export class PecasFaltantesMaisCriticasComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
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

  constructor(
    private _indicadorService: IndicadorService,
    private _pecaService: PecaService,
    private _osService: OrdemServicoService,
    protected _userService: UserService) {
    super(_userService, 'dashboard-filtro')
  }

  ngOnInit(): void {
    this.obterDados();
    this.registerEmitters();
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  loadFilter(): void {
    super.loadFilter();
  }

  private async obterDados() {
    this.loading = true;

    let topPecas = await this._indicadorService
      .obterPorParametros(
        {
          agrupador: IndicadorAgrupadorEnum.TOP_PECAS_FALTANTES,
          tipo: IndicadorTipoEnum.PECA_FALTANTE,
          include: OrdemServicoIncludeEnum.OS_PECAS,
          //dataInicio: this.filter?.parametros.dataInicio || moment().startOf('month').format('YYYY-MM-DD hh:mm'),
          //dataFim: this.filter?.parametros.dataFim || moment().endOf('month').format('YYYY-MM-DD hh:mm')
          dataInicio: moment().add(-30, 'days').format('YYYY-MM-DD 00:00'),
          dataFim: moment().format('YYYY-MM-DD 23:59')
        }
      ).toPromise();

    let codOsPecas = topPecas.map(t => t.filho.map(f => f.valor).join(',')).toString();
    this.osTopPecas = (await this._osService.obterPorParametros({
      codOS: codOsPecas,
      include: OrdemServicoIncludeEnum.OS_PECAS
    }).toPromise()).items;

    let codPecas = topPecas.map(item => item.label).join(',');
    const pecasInfo = (await this._pecaService.obterPorParametros({
      codPeca: codPecas,
    }).toPromise()).items;

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
