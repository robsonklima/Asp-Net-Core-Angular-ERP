import { AfterViewInit, ChangeDetectorRef, Component, LOCALE_ID, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { DespesaService } from 'app/core/services/despesa.service';
import { DespesaPeriodoTecnicoAtendimentoData } from 'app/core/types/despesa-adiantamento.types';
import { DespesaPeriodo } from 'app/core/types/despesa-periodo.types';
import { DespesaData } from 'app/core/types/despesa.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-atendimento-relatorio-lista',
  templateUrl: './despesa-atendimento-relatorio-lista.component.html',
  styles: [`
        .list-grid-despesa-atendimento-relatorio {
            grid-template-columns: 100px 75px 75px 100px 75px auto 100px  100px 100px 100px 100px;
            @screen sm { grid-template-columns: 100px 75px 75px 100px 75px auto 100px  100px 100px 100px 100px; }
            @screen md { grid-template-columns: 100px 75px 75px 100px 75px auto 100px  100px 100px 100px 100px; }
            @screen lg { grid-template-columns: 100px 75px 75px 100px 75px auto 100px  100px 100px 100px 100px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class DespesaAtendimentoRelatorioListaComponent extends Filterable implements AfterViewInit, IFilterable
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('sidenav') sidenav: MatSidenav;

  isLoading: boolean = false;
  periodo: DespesaPeriodo;
  atendimentos: DespesaPeriodoTecnicoAtendimentoData;
  despesas: DespesaData

  constructor (
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _route: ActivatedRoute,
    private _despesaPeriodoSvc: DespesaPeriodoService,
    private _despesaSvc: DespesaService,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService)
  {
    super(_userService, "despesa-atendimento-relatorio");
  }

  ngAfterViewInit()
  {
    this.obterDados();

    if (this.sort && this.paginator)
    {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() =>
      {
        this.onSortChanged()
        this.obterDados();
      });
    }

    this.registerEmitters();
    this._cdr.detectChanges();
  }

  private async obterPeriodo()
  {
    var codDespesaPeriodo =
      +this._route.snapshot.paramMap.get('codDespesaPeriodo');

    this.periodo =
      (await this._despesaPeriodoSvc.obterPorCodigo(codDespesaPeriodo).toPromise());
  }

  private async obterDespesas()
  {
    this.despesas = (await this._despesaSvc.obterPorParametros
      ({
        codTecnico: 1153,
        codDespesaPeriodo: this.periodo.codDespesaPeriodo
      }).toPromise());

    console.log(this.despesas);
  }

  public async obterDados()
  {
    this.isLoading = true;

    await this.obterPeriodo();
    await this.obterDespesas();

    this.isLoading = false;
  }

  registerEmitters(): void
  {
    this.sidenav.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  public paginar()
  {
    this.onPaginationChanged();
    this.obterDados();
  }
}