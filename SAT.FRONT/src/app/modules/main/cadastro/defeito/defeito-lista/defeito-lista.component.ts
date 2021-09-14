import { UserService } from './../../../../../core/user/user.service';
import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { DefeitoService } from '../../../../../core/services/defeito.service';
import { DefeitoParameters, DefeitoData } from '../../../../../core/types/defeito.types';
import { fuseAnimations } from '../../../../../../@fuse/animations/public-api';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-defeito-lista',
  templateUrl: './defeito-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-def {
          grid-template-columns: 50px 90px auto 10%;

          @screen sm {
            grid-template-columns: 50px 90px auto 10%;
          }
      
          @screen md {
            grid-template-columns: 50px 90px auto 10%;
          }
      
          @screen lg {
            grid-template-columns:  50px 90px auto 10%;
          }
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class DefeitoListaComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: DefeitoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    private _defeitoService: DefeitoService,
    private _cdr: ChangeDetectorRef,
    private _userService: UserService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngAfterViewInit() {
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
      pageNumber: this.paginator?.pageIndex +1,
      sortActive: 'nomeDefeito',
      sortDirection: 'asc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    }

    const data = await this._defeitoService.obterPorParametros(parametros).toPromise();
    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  paginar() {
    this.obterDados();
  }
}
