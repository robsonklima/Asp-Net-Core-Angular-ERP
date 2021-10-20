import { Component, OnInit } from '@angular/core';
import { IndicadorService } from 'app/core/services/indicador.service';
import { PecaService } from 'app/core/services/peca.service';
import { IndicadorAgrupadorEnum, Indicador, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import { interval, Subject } from 'rxjs';
import moment from 'moment';
import Enumerable from 'linq';
import { FilialService } from 'app/core/services/filial.service';
import { startWith, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-pecas-faltantes-mais-criticas',
  templateUrl: './pecas-faltantes-mais-criticas.component.html',
  styleUrls: ['./pecas-faltantes-mais-criticas.component.css']
})
export class PecasFaltantesMaisCriticasComponent implements OnInit 
{
  public dadosPeca: DadosPeca;
  public dadosEstoque: DadosEstoque[] = [];
  public loading: boolean = true;
  public index: number = 0;
  public topCodMagnus: string[] = [];
  public topEstoque = [];
  protected _onDestroy = new Subject<void>();

  constructor(
    private _indicadorService: IndicadorService,
    private _pecaService: PecaService,
    private _filialService: FilialService
  ) { }

   ngOnInit(): void {
    
    this.obterDados();

  }

  private async obterDados()
  {
    this.loading = true;

    const dadosPecasFaltantes = await this.buscaIndicadores(IndicadorAgrupadorEnum.TOP_PECAS_FALTANTES);

    const topPecas = dadosPecasFaltantes.orderBy('valor').take(10);
    
    for (let p of topPecas) 
    {             
      this.topCodMagnus.push((await this._pecaService.obterPorCodigo(+p.label).toPromise()).codMagnus);
    }   

    interval(10000)
    .pipe(
        startWith(0),
        takeUntil(this._onDestroy)
    )
    .subscribe(() => {
      this.index == 9 ? 0 : this.index++;

      this.showPeca(topPecas[this.index]);
      this.loading = false;
      
    });
  }

  private showPeca(topPecas): Promise<DadosPeca> {
    return new Promise((resolve, reject) => {
      this._pecaService.obterPorCodigo(+topPecas.label).subscribe(p => {
        let dados = new DadosPeca();
        dados.codMagnus= p.codMagnus;
        dados.descricao= p.nomePeca;
        dados.quantidade= topPecas.valor;
        this.dadosPeca = dados;
        resolve(this.dadosPeca)
      }, err => {
        reject(err);
      });
    });
  }
  

  private async buscaIndicadores(indicadorAgrupadorEnum: IndicadorAgrupadorEnum): Promise<Indicador[]> {
    let indicadorParams = {
      tipo: IndicadorTipoEnum.PECA_FALTANTE,
      agrupador: indicadorAgrupadorEnum,
      include: 3,
      dataInicio: moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      dataFim: moment().endOf('month').format('YYYY-MM-DD hh:mm')
    }

    return await this._indicadorService.obterPorParametros(indicadorParams).toPromise();
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}

export class DadosPeca
{
  codMagnus: string;
  descricao: string;
  quantidade: number;
}

export class DadosEstoque
{
  filial: string;
  quantidade: number;
}

