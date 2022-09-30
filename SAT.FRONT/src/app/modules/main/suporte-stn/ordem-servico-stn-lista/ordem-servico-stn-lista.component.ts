import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoSTNService } from 'app/core/services/ordem-servico-stn.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { OrdemServicoSTNData, OrdemServicoSTNParameters } from 'app/core/types/ordem-servico-stn.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-ordem-servico-stn-lista',
  templateUrl: './ordem-servico-stn-lista.component.html',
  styles: [`
    .list-grid-os-stn-lista {
      grid-template-columns: 72px 96px 72px 280px 248px 72px 128px 128px auto 98px;
    }  
  `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrdemServicoSTNListaComponent  extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') public sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: OrdemServicoSTNData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;

  constructor(
    private _ordemServicoSTNService: OrdemServicoSTNService,
    private _cdr: ChangeDetectorRef,
    private _snack: CustomSnackbarService,
    protected _userService: UserService
  ) {
    super(_userService, 'ordem-servico-stn');
  }

  async ngAfterViewInit() {
    this.obterDados();
    this.registerEmitters();
    this._cdr.detectChanges();
  }

  async obterDados(filtro: string = '') {
    this.isLoading = true;

    const params: OrdemServicoSTNParameters = {
      ...{
        pageNumber: this.paginator?.pageIndex + 1 || 1,
        sortActive: this.sort?.active || 'codAtendimento',
        sortDirection: this.sort?.direction || 'desc',
        pageSize: this.paginator?.pageSize,
        filter: filtro
      },
      ...this.filter?.parametros
    }
    const data = await this._ordemServicoSTNService
      .obterPorParametros(params)
      .toPromise();

    this.dataSourceData = data;
    console.log(data.items);
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  registerEmitters(): void {
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

    this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
		})
	}

  paginar() {
    this.obterDados();
  }
}
