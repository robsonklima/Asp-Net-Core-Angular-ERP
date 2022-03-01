import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { PontoPeriodoService } from 'app/core/services/ponto-periodo.service';
import { PontoPeriodo } from 'app/core/types/ponto-periodo.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-ponto-colaborador-lista',
  templateUrl: './ponto-colaborador-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-pc {
          grid-template-columns: auto 136px 198px 218px 72px 218px 72px;
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class PontoColaboradorListaComponent implements AfterViewInit {
  codPontoPeriodo: number;
  pontoPeriodo: PontoPeriodo;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
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
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
    this.codPontoPeriodo = +this._route.snapshot.paramMap.get('codPontoPeriodo');
    this.obterDados();

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

    this._pontoPeriodoSvc
      .obterPorCodigo(this.codPontoPeriodo)
      .subscribe((pp: PontoPeriodo) => {
        this.pontoPeriodo = pp;
      });
    
    const data = await this._userSvc
      .obterPorParametros({
        pageSize: this.paginator?.pageSize,
        pageNumber: this.paginator.pageIndex + 1,
        sortActive: this.sort.active || 'nomeUsuario',
        sortDirection: this.sort.direction || 'asc',
        indAtivo: statusConst.ATIVO,
        codPontoPeriodo: this.codPontoPeriodo,
        codPerfil: 35,
        codFilial: this.userSession?.usuario?.codFilial,
        filter: this.filtro
      })
      .toPromise();

    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  paginar() {
    this.obterDados();
  }
}

