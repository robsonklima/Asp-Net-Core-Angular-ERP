import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DefeitoService } from 'app/core/services/defeito.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { DefeitoData, DefeitoParameters } from 'app/core/types/defeito.types';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-defeito-lista',
  templateUrl: './defeito-lista.component.html',
  styles: [
    /* language=SCSS */
    `
    .list-grid-u {
      grid-template-columns: 142px 80px 50% 25% 42px;
      
      @screen sm {
          grid-template-columns: 142px 80px 50% 25% 42px;
      }
  
      @screen md {
          grid-template-columns: 142px 80px 50% 25% 42px;
      }
  
      @screen lg {
          grid-template-columns: 142px 80px 50% 25% 42px;
      }
  }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class DefeitoListaComponent extends Filterable implements AfterViewInit, IFilterable {
  
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: DefeitoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    private _defeitoService: DefeitoService,
    private _cdr: ChangeDetectorRef,
    private _exportacaoService: ExportacaoService,
    protected _userService: UserService
  ) {
    super(_userService, 'defeito')
    this.userSession = JSON.parse(this._userService.userSession);
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
    const parametros: DefeitoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: 'nomeDefeito',
      sortDirection: 'asc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    }

    const data: DefeitoData = await this._defeitoService.obterPorParametros({
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

  public async exportar() {
    this.isLoading = true;

    let exportacaoParam: Exportacao = {
			formatoArquivo: ExportacaoFormatoEnum.EXCEL,
			tipoArquivo: ExportacaoTipoEnum.DEFEITO,
			entityParameters: {}
		}

		await this._exportacaoService.exportar(FileMime.Excel, exportacaoParam);
    this.isLoading = false;
  }
  
}
