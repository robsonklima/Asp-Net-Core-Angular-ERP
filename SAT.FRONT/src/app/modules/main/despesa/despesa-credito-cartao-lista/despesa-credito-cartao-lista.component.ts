import { AfterViewInit, ChangeDetectorRef, Component, LOCALE_ID, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DespesaProtocoloData } from 'app/core/types/despesa-protocolo.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-despesa-credito-cartao-lista',
  templateUrl: './despesa-credito-cartao-lista.component.html',
  styles: [`
        .list-grid-despesa-credito-cartao {
            grid-template-columns: 70px 70px 60px auto 60px 150px 100px 100px 60px 60px 100px 80px;
            @screen sm { grid-template-columns: 70px 70px 60px auto 60px 150px 100px 100px 60px 60px 100px 80px; }
            @screen md { grid-template-columns: 70px 70px 60px auto 60px 150px 100px 100px 60px 60px 100px 80px; }
            @screen lg { grid-template-columns: 70px 70px 60px auto 60px 150px 100px 100px 60px 60px 100px 80px; }
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
  protocolos: DespesaProtocoloData;

  constructor (
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef)
  {
    super(_userService, 'despesa-credito-cartao');
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