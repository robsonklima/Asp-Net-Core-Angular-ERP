import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { PontoPeriodoService } from 'app/core/services/ponto-periodo.service';
import { PontoPeriodoData } from 'app/core/types/ponto-periodo.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
  selector: 'app-ponto-periodo-lista',
  templateUrl: './ponto-periodo-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-ge {
          grid-template-columns: 72px auto 128px 154px 72px;
          
          @screen sm {
              grid-template-columns: 72px auto 128px 154px 72px;
          }
      
          @screen md {
              grid-template-columns: 72px auto 128px 154px 72px;
          }
      
          @screen lg {
              grid-template-columns: 72px auto 128px 154px 72px;
          }
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class PontoPeriodoListaComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: PontoPeriodoData;
  isLoading: boolean = false;
  userSession: UserSession;

  constructor(
    private _cdr: ChangeDetectorRef,
    private _pontoPeriodoSvc: PontoPeriodoService,
    private _userSvc: UserService
  ) {
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  ngAfterViewInit(): void {
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
    
    const params = {
      pageSize: this.paginator?.pageSize,
      pageNumber: this.paginator.pageIndex + 1,
      sortActive: this.sort.active || 'codPontoPeriodo',
      sortDirection: this.sort.direction || 'desc',
    };

    const data = await this._pontoPeriodoSvc
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