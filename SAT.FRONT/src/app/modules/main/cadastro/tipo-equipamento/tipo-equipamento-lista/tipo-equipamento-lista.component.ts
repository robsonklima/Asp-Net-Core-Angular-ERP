import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { TipoEquipamentoService } from 'app/core/services/tipo-equipamento.service';
import { FileMime } from 'app/core/types/file.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { TipoEquipamento, TipoEquipamentoData, TipoEquipamentoParameters } from 'app/core/types/tipo-equipamento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-tipo-equipamento-lista',
  templateUrl: './tipo-equipamento-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-ge {
          grid-template-columns: 72px 156px auto 20%;
          
          @screen sm {
              grid-template-columns: 72px auto 72px 20%;
          }
      
          @screen md {
              grid-template-columns: 72px 156px auto 20%;
          }
      
          @screen lg {
              grid-template-columns: 72px 156px auto 20%;
          }
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class TipoEquipamentoListaComponent  extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort)  sort: MatSort;
  dataSourceData: TipoEquipamentoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  selectedItem: TipoEquipamento | null = null;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _tipoEquipamentoService: TipoEquipamentoService,
    private _exportacaoService: ExportacaoService,
    protected _userService: UserService
  ) {
    super(_userService, 'tipo-equipamento')
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
    
    const params: TipoEquipamentoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active,
      sortDirection: this.sort?.direction || 'desc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    };

    const data: TipoEquipamentoData = await this._tipoEquipamentoService.obterPorParametros({
      ...params,
      ...this.filter?.parametros
    }).toPromise();
    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();

  }
	
  public async exportar() {
    this.isLoading = true;
		await this._exportacaoService.exportar('TipoEquipamento', FileMime.Excel, {});
    this.isLoading = false;
  }

  paginar() {
    this.obterDados();
  }
}
