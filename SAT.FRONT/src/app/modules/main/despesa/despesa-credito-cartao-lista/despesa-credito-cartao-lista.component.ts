import { AfterViewInit, ChangeDetectorRef, Component, LOCALE_ID, Pipe, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaPeriodoTecnicoData, DespesaPeriodoTecnicoFilterEnum } from 'app/core/types/despesa-periodo.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import Enumerable from 'linq';

@Component({
  selector: 'app-despesa-credito-cartao-lista',
  templateUrl: './despesa-credito-cartao-lista.component.html',
  styles: [`
        .list-grid-despesa-credito-cartao {
            grid-template-columns: 60px 60px 60px auto 60px 150px 100px 100px 60px 60px 80px 60px;
            @screen sm { grid-template-columns: 60px 60px 60px auto 60px 150px 100px 100px 60px 60px 80px 60px; }
            @screen md { grid-template-columns: 60px 60px 60px auto 60px 150px 100px 100px 60px 60px 80px 60px; }
            @screen lg { grid-template-columns: 60px 60px 60px auto 60px 150px 100px 100px 60px 60px 80px 60px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class DespesaCreditoCartaoListaComponent extends Filterable implements AfterViewInit, IFilterable
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('sidenav') sidenav: MatSidenav;

  isLoading: boolean = false;
  periodos: DespesaPeriodoTecnicoData;

  constructor (
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService)
  {
    super(_userService, 'despesa-credito-cartao');
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
        this.onSortChanged();
        this.obterDados();
      });
    }

    this.registerEmitters();
    this._cdr.detectChanges();
  }

  public async obterDados()
  {
    this.isLoading = true;
    await this.obterPeriodosTecnico();
    this.prepareData();

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

  private async obterPeriodosTecnico()
  {
    this.periodos = (await this._despesaPeriodoTecnicoSvc.obterPorParametros(
      {
        filterType: DespesaPeriodoTecnicoFilterEnum.FILTER_CREDITOS_CARTAO,
        pageNumber: this.paginator.pageIndex + 1,
        pageSize: this.paginator.pageSize,
        sortActive: 'codDespesaPeriodoTecnico',
        sortDirection: 'desc'
      }
    ).toPromise());
  }

  prepareData()
  {
    this.periodos.items.forEach(p =>
    {
      p.tecnico.despesaCartaoCombustivelTecnico =
        Enumerable.from(p.tecnico.despesaCartaoCombustivelTecnico)
          .orderByDescending(i => i.dataHoraInicio)
          .toArray();
    })
  }

  public paginar()
  {
    this.onPaginationChanged();
    this.obterDados();
  }
}