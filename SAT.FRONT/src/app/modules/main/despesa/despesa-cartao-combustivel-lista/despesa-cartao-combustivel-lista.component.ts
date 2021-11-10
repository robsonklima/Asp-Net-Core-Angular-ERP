import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { UserService } from 'app/core/user/user.service';
import { LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { Filterable } from 'app/core/filters/filterable';
import { MatSidenav } from '@angular/material/sidenav';
import { IFilterable } from 'app/core/types/filtro.types';
import { DespesaCartaoCombustivelData } from 'app/core/types/despesa-cartao-combustivel.types';
registerLocaleData(localePt);

@Component({
  selector: 'app-despesa-cartao-combustivel-lista',
  templateUrl: './despesa-cartao-combustivel-lista.component.html',
  styles: [`
        .list-grid-despesa-cartao-combustivel {
            grid-template-columns: auto 80px 80px;
            @screen sm { grid-template-columns: auto 80px 80px; }
            @screen md { grid-template-columns: auto 80px 80px; }
            @screen lg { grid-template-columns: auto 80px 80px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class DespesaCartaoCombustivelListaComponent extends Filterable implements AfterViewInit, IFilterable
{
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('sidenav') sidenav: MatSidenav;

  isLoading: boolean = false;
  cartoes: DespesaCartaoCombustivelData;

  constructor (
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef)
  {
    super(_userService, "despesa-cartao-combustivel");
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


  public async obterDados()
  {
    this.isLoading = true;

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