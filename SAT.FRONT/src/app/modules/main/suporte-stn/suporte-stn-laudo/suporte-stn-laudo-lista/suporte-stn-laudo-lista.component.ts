import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { LaudoService } from 'app/core/services/laudo.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { LaudoData, Laudo, LaudoParameters } from 'app/core/types/laudo.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-suporte-stn-laudo-lista',
  templateUrl: './suporte-stn-laudo-lista.component.html',
  styles: [`
    .list-grid-os-stn-lista {
      grid-template-columns: 72px 80px 96px 300px 150px 300px auto 175px 100px;
    }  
  `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class SuporteStnLaudoListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: LaudoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  selectedItem: Laudo | null = null;
  userSession: UserSession;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _laudoService: LaudoService,
    private _snack: CustomSnackbarService
  ) {
    super(_userService, 'suporte-stn');
  }

  ngAfterViewInit(): void {
    this.obterDados();
    this.registerEmitters();

    fromEvent(this.searchInputControl?.nativeElement , 'keyup').pipe(
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
  
    this._cdr.detectChanges();
  }

  async obterDados(filtro: string = '') {
    this.isLoading = true;

    const params: LaudoParameters = {
      ...{
        pageNumber: this.paginator?.pageIndex + 1 || 1,
        sortActive: this.sort?.active || 'codLaudo',
        sortDirection: this.sort?.direction || 'desc',
        pageSize: this.paginator?.pageSize,
        filter: filtro
      },
      ...this.filter?.parametros
    }

    this._laudoService
      .obterPorParametros({
				...params,
				...this.filter?.parametros
			})
      .subscribe((data) => {
        this.dataSourceData = data;
        this.isLoading = false;
        this._cdr.detectChanges();
      }, e => {
        this._snack.exibirToast(`Erro ao consultar registros ${e.message}`, 'error');
      });
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
      console.log(this.filter);
      
    });
  }

  paginar() {
    this.obterDados();
  }
}
