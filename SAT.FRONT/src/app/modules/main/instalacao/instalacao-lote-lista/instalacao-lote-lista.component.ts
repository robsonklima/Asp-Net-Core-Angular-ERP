import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { ContratoService } from 'app/core/services/contrato.service';
import { InstalacaoLoteService } from 'app/core/services/instalacao-lote.service';
import { Contrato } from 'app/core/types/contrato.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { InstalacaoLoteData, InstalacaoLoteParameters } from 'app/core/types/instalacao-lote.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-lote-lista',
  templateUrl: 'instalacao-lote-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-instalacao-lote {
          grid-template-columns: 72px auto 240px 120px 154px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class InstalacaoLoteListaComponent extends Filterable implements AfterViewInit, IFilterable {
  codContrato: number;
  contrato: Contrato;
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: InstalacaoLoteData;
  isLoading: boolean = false;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _instalacaoLoteSvc: InstalacaoLoteService,
    private _contratoSvc: ContratoService,
    protected _userService: UserService,
    private _userSvc: UserService,
    private _route: ActivatedRoute
  ) {
    super(_userService, 'instalacao-lote')
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
	  this.codContrato = +this._route.snapshot.paramMap.get('codContrato');

    this.obterDados();
    this.obterContrato();
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

    const parametros: InstalacaoLoteParameters = {
      pageSize: this.paginator?.pageSize,
      filter: filtro, //this.searchInputControl.nativeElement.val,
      pageNumber: this.paginator.pageIndex + 1,
      sortActive: this.sort.active || 'NomeLote',
      sortDirection: this.sort.direction || 'asc',
      codContrato: this.codContrato
    };

    const data: InstalacaoLoteData = await this._instalacaoLoteSvc.obterPorParametros({
      ...parametros,
      ...this.filter?.parametros
    }).toPromise();
    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  async obterContrato() {
    this.contrato = await this._contratoSvc.obterPorCodigo(this.codContrato).toPromise();
  }

  paginar() {
    this.obterDados();
  }
}