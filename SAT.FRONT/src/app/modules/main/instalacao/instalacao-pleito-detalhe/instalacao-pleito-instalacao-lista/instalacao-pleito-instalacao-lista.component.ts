import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { InstalacaoPleito } from 'app/core/types/instalacao-pleito.types';
import { InstalacaoPleitoInstalData, InstalacaoPleitoInstalParameters } from 'app/core/types/instalacao-pleito-instal.types';
import { InstalacaoPleitoInstalService } from 'app/core/services/instalacao-pleito-instal.service';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-pleito-instalacao-lista',
  templateUrl: './instalacao-pleito-instalacao-lista.component.html',
  styles: [
    `
      .list-grid-instalacao-pleito {
          grid-template-columns: 72px 72px 72px 72px 72px 72px 72px 72px 72px 72px 72px 72px 72px 72px 72px 72px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class InstalacaoPleitoInstalacaoListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @Input() instalPleito: InstalacaoPleito;
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: InstalacaoPleitoInstalData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _InstalacaoPleitoInstalSvc: InstalacaoPleitoInstalService,
    private _userSvc: UserService
  ) {
    super(_userService, 'instalacao-pleito-instal')
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
    this.obterDados();

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

    const parametros: InstalacaoPleitoInstalParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: 'CodInstalPleito',
      sortDirection: 'desc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    }

    const data: InstalacaoPleitoInstalData = await this._InstalacaoPleitoInstalSvc.obterPorParametros({
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
