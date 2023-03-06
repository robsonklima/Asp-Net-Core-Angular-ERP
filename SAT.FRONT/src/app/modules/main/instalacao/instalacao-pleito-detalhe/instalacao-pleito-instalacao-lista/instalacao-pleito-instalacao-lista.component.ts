import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Input, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { InstalacaoPleitoInstalService } from 'app/core/services/instalacao-pleito-instal.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { InstalacaoPleitoInstalData, InstalacaoPleitoInstalParameters } from 'app/core/types/instalacao-pleito-instal.types';
import { InstalacaoPleito, InstalacaoPleitoData } from 'app/core/types/instalacao-pleito.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-instalacao-pleito-instalacao-lista',
  templateUrl: './instalacao-pleito-instalacao-lista.component.html',
  styles: [
    `
      .list-grid-instalacao-pleito-instal {
          grid-template-columns: 72px;
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
  dataSourceData: InstalacaoPleitoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  selectedItem: InstalacaoPleito | null = null;
  userSession: UserSession;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _InstalacaoPleitoInstalSvc: InstalacaoPleitoInstalService,
    private _userSvc: UserService
  ) {
    super(_userService, 'instalacao-pleito')
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  async ngAfterViewInit() {
    //this.obterDados();

    console.log(this.instalPleito);

    //this.registerEmitters();

    // if (this.sort && this.paginator) {
    //   fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
    //     map((event: any) => {
    //       return event.target.value;
    //     })
    //     , debounceTime(700)
    //     , distinctUntilChanged()
    //   ).subscribe((text: string) => {
    //     this.paginator.pageIndex = 0;
    //     this.searchInputControl.nativeElement.val = text;
    //     this.obterDados(text);
    //   });

    //   this.sort.disableClear = true;
    //   this._cdr.markForCheck();

    //   this.sort.sortChange.subscribe(() => {
    //     this.paginator.pageIndex = 0;
    //     this.obterDados();
    //   });
    // }

    this._cdr.detectChanges();    
  }

  async obterDados(filtro: string = '') {
    this.isLoading = true;    
   
    const parametros: InstalacaoPleitoInstalParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: 'CodInstalPleito',
      sortDirection: 'desc',
      pageSize: this.paginator?.pageSize,
      codInstalPleito: this.instalPleito?.codInstalPleito,
      filter: filtro
    }

    const data: InstalacaoPleitoInstalData = await this._InstalacaoPleitoInstalSvc.obterPorParametros({
      ...parametros,
      ...this.filter?.parametros
    }).toPromise();

    this.dataSourceData = data;    
    this.isLoading = false;
    console.log("teste");

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

  paginar() {
    this.obterDados();
  }
}
