import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import { LocalAtendimentoData, LocalAtendimentoParameters } from 'app/core/types/local-atendimento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-local-atendimento-lista',
  templateUrl: './local-atendimento-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-ge {
          grid-template-columns: 72px auto 112px 112px 112px 112px;
          
          @screen sm {
              grid-template-columns: 72px auto 112px 112px 112px 112px;
          }
      
          @screen md {
              grid-template-columns: 72px auto 112px 112px 112px 112px;
          }
      
          @screen lg {
              grid-template-columns: 72px auto 112px 112px 112px 112px;
          }
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class LocalAtendimentoListaComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: LocalAtendimentoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  userSession: UserSession;
  
  constructor(
    private _cdr: ChangeDetectorRef,
    private _localAtendimentoService: LocalAtendimentoService,
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

  async obterDados() {
    this.isLoading = true;
    
    const params: LocalAtendimentoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active || 'nomeLocal',
      sortDirection: this.sort?.direction || 'asc',
      pageSize: this.paginator?.pageSize,
      filter: this.searchInputControl.nativeElement.val
    };

    const data = await this._localAtendimentoService
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
