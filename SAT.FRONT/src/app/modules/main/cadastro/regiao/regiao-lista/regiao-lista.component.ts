import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { Regiao, RegiaoData, RegiaoParameters } from 'app/core/types/regiao.types';
import { RegiaoService } from 'app/core/services/regiao.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { UserSession } from 'app/core/user/user.types';
import { fuseAnimations } from '@fuse/animations';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-regiao-lista',
  templateUrl: './regiao-lista.component.html',
  styles: [`
    .list-grid {
      grid-template-columns: 72px auto 60px;
      
      /* @screen sm {
          grid-template-columns: 72px auto 32px;
      }

      @screen md {
          grid-template-columns: 72px auto 72px;
      }

      @screen lg {
          grid-template-columns: 72px auto 72px;
      } */
    }  
  `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class RegiaoListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') public sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: RegiaoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  selectedItem: Regiao | null = null;
  userSession: UserSession;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _regiaoService: RegiaoService
  ) {
    super(_userService, 'regiao')
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

    const params: RegiaoParameters = {
      ...{
        //this._regiaoService.obterPorParametros({
        pageNumber: this.paginator?.pageIndex + 1,
        sortActive: this.sort.active,
        sortDirection: this.sort.direction,
        pageSize: this.paginator?.pageSize,
        filter: filtro

      },
      ...this.filter?.parametros
    }

    const data = await this._regiaoService
      .obterPorParametros(params)
      .toPromise();

    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

paginar() {
  this.obterDados();
}
}