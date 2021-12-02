import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DespesaPeriodoService } from 'app/core/services/despesa-periodo.service';
import { DespesaPeriodoData } from 'app/core/types/despesa-periodo.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-periodo-lista',
  templateUrl: './despesa-periodo-lista.component.html',
  styles: [`
        .list-grid-despesa-periodo {
            grid-template-columns: 150px auto auto 120px;
            @screen sm { grid-template-columns: 150px auto auto 120px; }
            @screen md { grid-template-columns: 150px auto auto 120px; }
            @screen lg { grid-template-columns: 150px auto auto 120px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class DespesaPeriodoListaComponent extends Filterable implements AfterViewInit, IFilterable
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('sidenav') sidenav: MatSidenav;

  isLoading: boolean = false;
  periodos: DespesaPeriodoData;

  constructor (
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _despesaPeriodoSvc: DespesaPeriodoService)
  {
    super(_userService, "despesa-periodo");
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

  private async obterPeriodos()
  {
    this.periodos = (await this._despesaPeriodoSvc.obterPorParametros({
      indAtivo: this.filter?.parametros?.indAtivo,
      inicioPeriodo: this.filter?.parametros?.inicioPeriodo,
      fimPeriodo: this.filter?.parametros?.fimPeriodo,
      pageNumber: this.paginator?.pageIndex + 1,
      pageSize: this.paginator?.pageSize,
      sortActive: 'codDespesaPeriodo',
      sortDirection: 'desc'
    }).toPromise());
  }

  public async obterDados()
  {
    this.isLoading = true;
    await this.obterPeriodos();
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
