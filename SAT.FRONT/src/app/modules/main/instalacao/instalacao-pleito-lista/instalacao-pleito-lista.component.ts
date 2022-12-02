import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { InstalacaoPleitoService } from 'app/core/services/instalacao-pleito.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { InstalacaoPleito, InstalacaoPleitoData, InstalacaoPleitoParameters } from 'app/core/types/instalacao-pleito.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-pleito-lista',
  templateUrl: './instalacao-pleito-lista.component.html',
  styles: [
    `
      .list-grid-instalacao-pleito {
          grid-template-columns: 72px 200px auto 200px 140px 72px 72px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class InstalacaoPleitoListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: InstalacaoPleitoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  selectedItem: InstalacaoPleito | null = null;
  userSession: UserSession;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _InstalacaoPleitoSvc: InstalacaoPleitoService,
    private _userSvc: UserService
  ) {
    super(_userService, 'instalacao-pleito')
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
    
    const parametros: InstalacaoPleitoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: 'CodInstalPleito',
      sortDirection: 'desc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    }

    const data: InstalacaoPleitoData = await this._InstalacaoPleitoSvc.obterPorParametros({
      ...parametros,
      ...this.filter?.parametros
    }).toPromise();

    this.dataSourceData = data;
    console.log(this.dataSourceData);
    
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  paginar() {
    this.obterDados();
  }
}