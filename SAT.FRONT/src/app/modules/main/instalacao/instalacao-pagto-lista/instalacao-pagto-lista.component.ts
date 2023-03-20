import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { InstalacaoPagtoService } from 'app/core/services/instalacao-pagto.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { InstalacaoPagto, InstalacaoPagtoData, InstalacaoPagtoParameters } from 'app/core/types/instalacao-pagto.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-pagto-lista',
  templateUrl: './instalacao-pagto-lista.component.html',
  styles: [
    `
      .list-grid-instalacao-pagto {
          grid-template-columns: 72px 200px auto 200px 140px 72px 72px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class InstalacaoPagtoListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: InstalacaoPagtoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  selectedItem: InstalacaoPagto | null = null;
  userSession: UserSession;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _InstalacaoPagtoSvc: InstalacaoPagtoService,
    private _userSvc: UserService
  ) {
    super(_userService, 'instalacao-pagto')
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
    this.obterDados();    
    this.registerEmitters();

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
        this.obterDados(text);
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

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  loadFilter(): void {
    super.loadFilter();
  }

  onSidenavClosed(): void {
    if (this.paginator) this.paginator.pageIndex = 0;
    this.loadFilter();
    this.obterDados();
  }

  async obterDados(filtro: string = '') {
    this.isLoading = true;    
    const parametros: InstalacaoPagtoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: 'CodInstalPagto',
      sortDirection: 'desc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    }

    const data: InstalacaoPagtoData = await this._InstalacaoPagtoSvc.obterPorParametros({
      ...parametros,
      ...this.filter?.parametros
    }).toPromise();  

    this.dataSourceData = data;

    this.isLoading = false;
    this._cdr.detectChanges();
  }

  paginar() {
    this.obterDados();
  }
}
