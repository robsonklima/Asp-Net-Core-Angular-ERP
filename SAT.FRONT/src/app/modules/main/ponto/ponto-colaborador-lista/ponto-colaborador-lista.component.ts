import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { PontoPeriodoService } from 'app/core/services/ponto-periodo.service';
import { PontoPeriodo } from 'app/core/types/ponto-periodo.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioData, UsuarioParameters } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { IFilterable } from 'app/core/types/filtro.types';
import { Filterable } from 'app/core/filters/filterable';

@Component({
  selector: 'app-ponto-colaborador-lista',
  templateUrl: './ponto-colaborador-lista.component.html',
  styles: [
    `
      .list-grid-pc {
          grid-template-columns: auto 136px 198px 218px 72px 218px 72px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class PontoColaboradorListaComponent extends Filterable implements AfterViewInit, IFilterable {
  [x: string]: any;
  codPontoPeriodo: number;
  pontoPeriodo: PontoPeriodo;
	@ViewChild('searchInputControl', { read: ElementRef }) searchInputControl: ElementRef;
	@ViewChild('sidenav') sidenav: MatSidenav;  
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) public sort: MatSort;
  filtro: string;
  dataSourceData: UsuarioData;
  isLoading: boolean = false;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _userSvc: UserService,
    private _route: ActivatedRoute,
    private _pontoPeriodoSvc: PontoPeriodoService
  ) {
    super(_userSvc,'ponto-colaborador')
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
    this.codPontoPeriodo = +this._route.snapshot.paramMap.get('codPontoPeriodo');
    this.obterDados();
    this.registerEmitters();

    if (this.sort && this.paginator) {
      this.sort.disableClear = true;
      this._cdr.markForCheck();

      this.sort.sortChange.subscribe(() => {
        this.paginator.pageIndex = 0;
        this.obterDados();
      });
    }

    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , debounceTime(700)
      , distinctUntilChanged()
    ).subscribe((text: string) => {
      this.paginator.pageIndex = 0;
      this.filtro = text;
      this.obterDados();
    });

    this._cdr.detectChanges();
  }

  async obterDados() {
    this.isLoading = true;

    this.pontoPeriodo = await this._pontoPeriodoSvc.obterPorCodigo(this.codPontoPeriodo).toPromise();
    
    const params: UsuarioParameters = 
    {
      ...{
        pageSize: this.paginator?.pageSize,
        pageNumber: this.paginator.pageIndex + 1,
        sortActive: this.sort.active || 'nomeUsuario',
        sortDirection: this.sort.direction || 'asc',
        indAtivo: statusConst.ATIVO,
        codPontoPeriodo: this.pontoPeriodo.codPontoPeriodo,
        indPonto: 1,
        codFilial: this.userSession?.usuario?.codFilial,
        filter: this.filtro
      },
      ... this.filter.codPontoPeriodoUsuarioStatus
    }
    
    this._userSvc.obterPorParametros(params).subscribe((data) => 
        {
          this.dataSourceData = data;
          this.isLoading = false;
          this._cdr.detectChanges(); 
        });
  }

  registerEmitters(): void {
      this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();     
			this.obterDados();
		});
	}

  paginar() {
    this.obterDados();
  }
}

