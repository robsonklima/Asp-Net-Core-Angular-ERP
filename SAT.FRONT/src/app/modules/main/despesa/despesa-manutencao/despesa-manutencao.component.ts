import { AfterViewInit, ChangeDetectorRef, Component, LOCALE_ID } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DespesaTipoService } from 'app/core/services/despesa-tipo.service';
import { DespesaService } from 'app/core/services/despesa.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { Despesa, DespesaTipo } from 'app/core/types/despesa.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';

@Component({
  selector: 'app-despesa-manutencao',
  templateUrl: './despesa-manutencao.component.html',
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class DespesaManutencaoComponent implements AfterViewInit
{

  isLoading: boolean = false;
  sort: any;
  paginator: any;
  codRAT: number;
  despesa: Despesa;
  rat: RelatorioAtendimento;
  ordemServico: OrdemServico;
  tiposDespesa: DespesaTipo[] = [];
  displayedColumns: string[] = ['acao', 'despesaTipo', 'numNF', 'quilometragem', 'valorTotal'];

  constructor (
    private _cdr: ChangeDetectorRef,
    private _despesaSvc: DespesaService,
    private _relatorioAtendimentoSvc: RelatorioAtendimentoService,
    private _ordemServicoSvc: OrdemServicoService,
    private _despesaTipoSvc: DespesaTipoService,
    private _route: ActivatedRoute) 
  {
    this.codRAT = +this._route.snapshot.paramMap.get('codRAT');
  }

  async ngAfterViewInit()
  {
    await this.obterDados();

    if (this.sort && this.paginator)
    {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() =>
      {
        this.obterDados();
      });
    }

    this._cdr.detectChanges();
  }

  private async obterRAT()
  {
    this.rat = (await this._relatorioAtendimentoSvc.obterPorCodigo(this.codRAT).toPromise());
  }

  private async obterOS()
  {
    this.ordemServico = (await this._ordemServicoSvc.obterPorCodigo(this.rat.codOS).toPromise());
  }

  private async obterDespesa()
  {
    this.despesa = (await this._despesaSvc.obterPorParametros({ codRATs: this.codRAT.toString() }).toPromise()).items[0];
  }

  private async obterTiposDespesa()
  {
    this.tiposDespesa = (await this._despesaTipoSvc.obterPorParametros({ indAtivo: 1 }).toPromise()).items;
  }

  private async lancarDespesaItem()
  {

  }

  private criarFormularioDespesaItem()
  {

  }


  public async obterDados()
  {
    this.isLoading = true;

    await this.obterRAT();
    await this.obterOS();
    await this.obterDespesa();
    await this.obterTiposDespesa();

    this.isLoading = false;
  }
}
