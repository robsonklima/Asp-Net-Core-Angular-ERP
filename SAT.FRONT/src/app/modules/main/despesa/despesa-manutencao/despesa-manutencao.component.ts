import { AfterViewInit, ChangeDetectorRef, Component, LOCALE_ID, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { DespesaItemService } from 'app/core/services/despesa-item.service';
import { DespesaService } from 'app/core/services/despesa.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { Despesa, DespesaItem } from 'app/core/types/despesa.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import Enumerable from 'linq';
import { DespesaItemDialogComponent } from './despesa-item-dialog/despesa-item-dialog.component';

@Component({
  selector: 'app-despesa-manutencao',
  templateUrl: './despesa-manutencao.component.html',
  styles: [`
        .list-grid-despesa-manutencao  {
            grid-template-columns: 100px 100px 100px 100px 100px;
            @screen sm { grid-template-columns: 100px 100px 100px 100px 100px;}
            @screen md { grid-template-columns: 100px 100px 100px 100px 100px; }
            @screen lg { grid-template-columns: 100px 100px 100px 100px 100px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class DespesaManutencaoComponent implements AfterViewInit
{

  isLoading: boolean = false;
  isDespesaLoading: boolean = false;
  sort: any;
  paginator: any;
  codRAT: number;
  despesa: Despesa;
  despesaItens: DespesaItem[] = [];
  rat: RelatorioAtendimento;
  ordemServico: OrdemServico;
  displayedColumns: string[] = ['acao', 'despesaTipo', 'numNF', 'quilometragem', 'valorTotal'];

  constructor (
    private _cdr: ChangeDetectorRef,
    private _route: ActivatedRoute,
    private _despesaSvc: DespesaService,
    private _relatorioAtendimentoSvc: RelatorioAtendimentoService,
    private _ordemServicoSvc: OrdemServicoService,
    private _despesaItemSvc: DespesaItemService,
    private _dialog: MatDialog)
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
    this.isDespesaLoading = true;
    this.despesa = (await this._despesaSvc.obterPorParametros({ codRATs: this.codRAT.toString() }).toPromise()).items[0];
    this.despesaItens = Enumerable.from(this.despesa.despesaItens).orderByDescending(i => i.codDespesaItem).toArray();
    this.isDespesaLoading = false;
  }

  lancarDespesaItem()
  {
    const dialogRef = this._dialog.open(DespesaItemDialogComponent, {
      data: {
        codDespesa: this.despesa.codDespesa
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
        this.obterDespesa();
    });
  }

  public async obterDados()
  {
    this.isLoading = true;

    await this.obterRAT();
    await this.obterOS();
    await this.obterDespesa();

    this.isLoading = false;
  }
}
