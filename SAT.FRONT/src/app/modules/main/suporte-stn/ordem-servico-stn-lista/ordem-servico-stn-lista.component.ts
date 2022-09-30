import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoSTNService } from 'app/core/services/ordem-servico-stn.service';
import { IFilterable } from 'app/core/types/filtro.types';
import { OrdemServicoSTNData, OrdemServicoSTNParameters } from 'app/core/types/ordem-servico-stn.types';
import { statusServicoSTNConst } from 'app/core/types/status-servico-stn.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-ordem-servico-stn-lista',
  templateUrl: './ordem-servico-stn-lista.component.html',
  styles: [`
    .list-grid-os-stn-lista {
      grid-template-columns: 72px 96px 64px 280px 186px 72px 128px 148px auto 176px 64px;
    }  
  `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OrdemServicoSTNListaComponent  extends Filterable implements AfterViewInit, OnInit, IFilterable {
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

  ngOnInit() {
    
  }

  async ngAfterViewInit() {
    this.obterDados();
    //this.registerEmitters();
    
    if (this.sort && this.paginator) {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.obterDados();
      });
    }
    
    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) => { return event.target.value })
      , debounceTime(700)
      , distinctUntilChanged()
      ).subscribe((text: string) => {
        debugger
        this.paginator.pageIndex = 0;
        this.obterDados(text);
      });

    this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
		});

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

    this._ordemServicoSTNService
      .obterPorParametros(params)
      .subscribe((data) => {
        this.dataSourceData = data;
        this.isLoading = false;
        this._cdr.detectChanges();
        console.log(data.items);
      }, e => {
        this._snack.exibirToast(`Erro ao consultar registros ${e.message}`, 'error');
      });
  }

  obterCorStatus(codStatusSTN: number): string {
    switch (codStatusSTN) {
      case statusServicoSTNConst.ABERTO:
        return '';
      
      case statusServicoSTNConst.AGUARD_ATENDIMENTO:
        return '';

      case statusServicoSTNConst.CANCELADO:
        return '';

      case statusServicoSTNConst.FECHADO:
        return '';
    
      default:
        return null;
    }
  }

  registerEmitters(): void {
    
	}

  paginar() {
    this.obterDados();
  }
}
