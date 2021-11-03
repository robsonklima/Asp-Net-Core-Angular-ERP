import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { UsuarioData, UsuarioParameters } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-ponto-colaborador-lista',
  templateUrl: './ponto-colaborador-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-pc {
          grid-template-columns: auto 116px 198px 218px 72px 218px 72px;
          
          @screen sm {
              grid-template-columns: auto 116px 198px 218px 72px 218px 72px;
          }
      
          @screen md {
              grid-template-columns: auto 116px 198px 218px 72px 218px 72px;
          }
      
          @screen lg {
              grid-template-columns: auto 116px 198px 218px 72px 218px 72px;
          }
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class PontoColaboradorListaComponent implements AfterViewInit {
  codPontoPeriodo: number;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: UsuarioData;
  isLoading: boolean = false;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _userSvc: UserService,
    private _route: ActivatedRoute
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

    this._cdr.detectChanges();
  }

  async obterDados() {
    this.isLoading = true;
    
    const params: UsuarioParameters = {
      pageSize: this.paginator?.pageSize,
      pageNumber: this.paginator.pageIndex + 1,
      sortActive: this.sort.active || 'nomeUsuario',
      sortDirection: this.sort.direction || 'asc',
      indAtivo: 1,
      codPontoPeriodo: this.codPontoPeriodo
    };

    const data = await this._userSvc
      .obterPorParametros(params)
      .toPromise();

    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  paginar() {
    this.obterDados();
  }
}

