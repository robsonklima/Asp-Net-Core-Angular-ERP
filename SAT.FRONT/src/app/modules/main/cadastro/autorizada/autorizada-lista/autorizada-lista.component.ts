import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { Autorizada, AutorizadaData, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { UserSession } from 'app/core/user/user.types';
import { fuseAnimations } from '@fuse/animations';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { FileMime } from 'app/core/types/file.types';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-autorizada-lista',
  templateUrl: './autorizada-lista.component.html',
  styles: [`
    .list-grid-autorizada {
      grid-template-columns: 72px auto 72px 72px;
      
      /* @screen sm {
          grid-template-columns: 72px auto 72px 72px;
      }

      @screen md {
          grid-template-columns: 72px auto 72px 72px;
      }

      @screen lg {
          grid-template-columns: 72px auto 72px 72px;
      } */
    }  
  `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class AutorizadaListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') public sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: AutorizadaData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  selectedItem: Autorizada | null = null;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _autorizadaService: AutorizadaService,
    private _exportacaoService: ExportacaoService,
    protected _userService: UserService
  ) {
    super(_userService, 'autorizada')
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

    const params: AutorizadaParameters = {
      ...{
        pageNumber: this.paginator?.pageIndex + 1,
        sortActive: this.sort?.active || 'razaoSocial',
        sortDirection: this.sort?.direction || 'asc',
        pageSize: this.paginator?.pageSize,
        filter: filtro
      },
      ...this.filter?.parametros
    }
    const data = await this._autorizadaService
      .obterPorParametros(params)
      .toPromise();

    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  public async exportar() {
    this.isLoading = true;
    await this._exportacaoService.exportar('Autorizada', FileMime.Excel, {});
    this.isLoading = false;
  }

  paginar() {
    this.obterDados();
  }
}