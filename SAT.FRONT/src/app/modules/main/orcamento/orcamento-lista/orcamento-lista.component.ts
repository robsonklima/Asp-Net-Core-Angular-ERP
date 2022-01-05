import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { OrcamentoService } from 'app/core/services/orcamento.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { Orcamento, OrcamentoData, OrcamentoParameters } from 'app/core/types/orcamento.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-orcamento-lista',
  templateUrl: './orcamento-lista.component.html',
  styles: [`
        .list-grid-orcamentos {
            grid-template-columns: 48px 72px 48px 80px 118px 118px auto 72px 138px 120px 248px 70px 120px;
            
            @screen md {
              grid-template-columns: 48px 72px 48px 80px 118px 72px auto 148px 138px 120px;
            }

            @screen lg {
              grid-template-columns: 48px 72px 48px 80px 118px 118px auto 72px 138px 120px 248px 70px 120px;
          }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrcamentoListaComponent extends Filterable implements AfterViewInit, IFilterable
{
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;

  @ViewChild(MatSort) sort: MatSort;

  dataSourceData: OrcamentoData;
  selectedItem: Orcamento | null = null;
  isLoading: boolean = false;
  protected _onDestroy = new Subject<void>();

  constructor (
    private _orcamentoSvc: OrcamentoService,
    private _cdr: ChangeDetectorRef,
    protected _userService: UserService
  )
  {
    super(_userService, 'orcamento');
  }

  ngAfterViewInit(): void
  {
    this.obterOrcamentos();
    this.registerEmitters();

    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) =>
      {
        return event.target.value;
      })
      , debounceTime(1000)
      , distinctUntilChanged()
    ).subscribe((text: string) =>
    {
      this.paginator.pageIndex = 0;
      this.obterOrcamentos(text);
    });

    if (this.sort && this.paginator)
    {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() =>
      {
        this.onSortChanged();
        this.obterOrcamentos();
      });
    }

    this._cdr.detectChanges();
  }

  registerEmitters(): void
  {
    this.sidenav.closedStart.subscribe(() =>
    {
      this.onSidenavClosed();
      this.obterOrcamentos();
    })
  }

  private async obterOrcamentos(filtro: string = '')
  {
    this.isLoading = true;

    const params: OrcamentoParameters = {
      pageNumber: this.paginator.pageIndex + 1,
      sortActive: this.filter?.parametros?.sortActive || this.sort.active || 'codOrc',
      sortDirection: this.filter?.parametros?.direction || this.sort.direction || 'desc',
      pageSize: this.filter?.parametros?.qtdPaginacaoLista ?? this.paginator?.pageSize,
      filter: filtro
    };

    const data: OrcamentoData = await this._orcamentoSvc
      .obterPorParametros({
        ...params,
        ...this.filter?.parametros
      })
      .toPromise();

    this.dataSourceData = data;
    this.isLoading = false;
  }

  paginar()
  {
    this.onPaginationChanged();
    this.obterOrcamentos();
  }

  ngOnDestroy()
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
