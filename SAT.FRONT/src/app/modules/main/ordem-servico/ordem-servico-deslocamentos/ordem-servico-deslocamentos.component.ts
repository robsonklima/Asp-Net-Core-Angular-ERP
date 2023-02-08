import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { Tecnico, TecnicoParameters, ViewTecnicoDeslocamento, ViewTecnicoDeslocamentoData } from 'app/core/types/tecnico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Utils } from 'app/core/utils/utils';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { OrdemServicoDeslocamentosMapaComponent } from '../ordem-servico-deslocamentos-mapa/ordem-servico-deslocamentos-mapa.component';

@Component({
  selector: 'app-ordem-servico-deslocamentos',
  templateUrl: './ordem-servico-deslocamentos.component.html',
  styles: [`
    .list-grid-desl-tec {
        grid-template-columns: 65px auto 65px 65px 165px 165px 165px 165px 165px 65px;
    }`
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrdemServicoDeslocamentosComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') public sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: ViewTecnicoDeslocamentoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  selectedItem: Tecnico | null = null;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _tecnicoService: TecnicoService,
    protected _userService: UserService,
    private _dialog: MatDialog,
    private _utils: Utils
  ) {
    super(_userService, 'tecnico')
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
    this.registerEmitters();
    this.obterDados();

    if (this.sort && this.paginator)
    {
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

  async obterDados(filtro: string = '') {
    this.isLoading = true;

    const params: TecnicoParameters = {
      ...{
        pageNumber: this.paginator?.pageIndex + 1,
        sortActive: this.sort?.active || 'tecnico',
        sortDirection: this.sort?.direction || 'asc',
        pageSize: this.paginator?.pageSize,
        filter: filtro
      },
      ...this.filter?.parametros
    }

    const data = await this._tecnicoService
      .obterDeslocamentos(params)
      .toPromise();

    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
    });
  }

  obterDuracao(data: string): string {
    return this._utils.obterDuracao(data);
  }

  abrirMapa(deslocamento: ViewTecnicoDeslocamento) {
    this._dialog.open(OrdemServicoDeslocamentosMapaComponent, {
      data: {
        deslocamento: deslocamento
      },
      width: '980px',
      height: '680px'
    });
  }

  paginar() {
    this.obterDados();
  }
}
