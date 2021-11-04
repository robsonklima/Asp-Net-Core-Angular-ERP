import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { PontoPeriodoService } from 'app/core/services/ponto-periodo.service';
import { PontoPeriodoData, PontoPeriodoParameters } from 'app/core/types/ponto-periodo.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-ponto-periodo-lista',
  templateUrl: './ponto-periodo-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-ge {
          grid-template-columns: 72px 128px auto 154px 164px;
          
          @screen sm {
              grid-template-columns: 72px 128px auto 154px 164px;
          }
      
          @screen md {
              grid-template-columns: 72px 128px auto 154px 164px;
          }
      
          @screen lg {
              grid-template-columns: 72px 128px auto 154px 164px;
          }
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class PontoPeriodoListaComponent implements AfterViewInit {
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
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

    this._cdr.detectChanges();
  }

  async obterDados() {
    this.isLoading = true;
    
    const params: PontoPeriodoParameters = {
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val,
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