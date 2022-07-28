import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { FormaPagamentoService } from 'app/core/services/forma-pagamento.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { FormaPagamentoData, FormaPagamentoParameters } from 'app/core/types/forma-pagamento.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-formas-pagamento-lista',
  templateUrl: './formas-pagamento-lista.component.html',
  styles: [
    /* language=SCSS */
    `
    .list-grid-u {
      grid-template-columns: 142px 50% 200px 25%;
      
      /* @screen sm {
          grid-template-columns: 142px 50% 200px 25%;
      }
  
      @screen md {
          grid-template-columns: 142px 50% 200px 25%;
      }
  
      @screen lg {
          grid-template-columns: 142px 50% 200px 25%;
      } */
  }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class FormasPagamentoListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') public sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: FormaPagamentoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;

  constructor(
    protected _userService: UserService,
    private _formasPagamentoService: FormaPagamentoService,
    private _cdr: ChangeDetectorRef
  ) {
    super(_userService, 'formas-pagamento')
    this.userSession = JSON.parse(this._userService.userSession);
   }

   registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  async ngAfterViewInit() {
    this.registerEmitters();
    this.obterDados();

    if (this.sort && this.paginator) {
      fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
        map((event: any) => {
          return event.target.value;
        })
        , debounceTime(700)
        , distinctUntilChanged()
      ).subscribe((text: string) => {
        this.paginator.pageIndex = 0;
        this.searchInputControl.nativeElement.val = text;
        this.obterDados();
      });

      this.sort.disableClear = true;
      this._cdr.markForCheck();
      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.obterDados();
      });
    }

    this._cdr.detectChanges();
  }

  async obterDados(filtro: string = '') {

    this.isLoading = true;
    const parametros: FormaPagamentoParameters = {
      ...{
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: 'DescFormaPagto',
      sortDirection: 'asc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    },
    ...this.filter?.parametros
    }

    const data = await this._formasPagamentoService.obterPorParametros(parametros).toPromise();
    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  paginar() {
    this.obterDados();
  }
}
