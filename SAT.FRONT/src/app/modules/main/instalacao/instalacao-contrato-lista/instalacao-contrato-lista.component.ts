import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { ContratoService } from 'app/core/services/contrato.service';
import { Contrato, ContratoData, ContratoParameters } from 'app/core/types/contrato.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { Filterable } from 'app/core/filters/filterable';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import _ from 'lodash';
import { MatSidenav } from '@angular/material/sidenav';
import { IFilterable } from 'app/core/types/filtro.types';
@Component({
  selector: 'app-instalacao-contrato-lista',
  templateUrl: 'instalacao-contrato-lista.component.html',
  styles: [
    `
      .list-grid-instalacao-contrato {
          grid-template-columns: 72px 136px auto 200px 140px 154px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class InstalacaoContratoListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: ContratoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  selectedItem: Contrato | null = null;
  userSession: UserSession;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _contratoSvc: ContratoService,
    private _userSvc: UserService
  ) {
    super(_userService, 'instalacao-contrato')
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

    const parametros: ContratoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: 'CodContrato' || 'nomeContrato',
      sortDirection: 'desc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    }

    const data: ContratoData = await this._contratoSvc.obterPorParametros({
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