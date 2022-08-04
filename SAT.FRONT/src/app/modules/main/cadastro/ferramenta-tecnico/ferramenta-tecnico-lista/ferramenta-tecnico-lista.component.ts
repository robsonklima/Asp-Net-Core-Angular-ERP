import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { FerramentaTecnicoService } from 'app/core/services/ferramenta-tecnico.service';
import { FerramentaTecnicoData, FerramentaTecnicoParameters } from 'app/core/types/ferramenta-tecnico.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-ferramenta-tecnico-lista',
  templateUrl: './ferramenta-tecnico-lista.component.html',
  styles: [
    /* language=SCSS */
    `
    .list-grid-u {
      grid-template-columns: 50% 25% ;
      
      /* @screen sm {
          grid-template-columns: 50% 25% ;
      }
  
      @screen md {
          grid-template-columns: 50% 25% ;
      }
  
      @screen lg {
          grid-template-columns: 50% 25% ;
      } */
  }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class FerramentaTecnicoListaComponent extends Filterable implements OnInit, AfterViewInit, IFilterable {
  @ViewChild('sidenav') public sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: FerramentaTecnicoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    protected _userService: UserService,
    private _ferramentaTecnicoService: FerramentaTecnicoService,
    private _cdr: ChangeDetectorRef,
    private _exportacaoService: ExportacaoService
  ) {
    super(_userService, 'ferramenta-tecnico')
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

  ngOnInit(): void {

  }

  async obterDados(filtro: string = '') {
    this.isLoading = true;
    const parametros: FerramentaTecnicoParameters = {
      ...{
        pageNumber: this.paginator?.pageIndex + 1,
        sortActive: 'nome',
        sortDirection: 'asc',
        pageSize: this.paginator?.pageSize,
        filter: filtro,
        indAtivo: 1
      },
      ...this.filter?.parametros
    }
    const data = await this._ferramentaTecnicoService.obterPorParametros(parametros).toPromise();
    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  async exportar() {
    this.isLoading = true;

    await this._exportacaoService.exportar('FerramentaTecnico', FileMime.Excel, this.filter?.parametros);

    this.isLoading = false;
  }

  paginar() {
    this.obterDados();
  }
}
