import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { FilialService } from 'app/core/services/filial.service';
import { FilialData, FilialParameters } from 'app/core/types/filial.types';
import { IFilterable } from 'app/core/types/filtro.types';
import { UserService } from 'app/core/user/user.service';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-filial-lista',
  templateUrl: './filial-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-u {
          grid-template-columns: 142px auto 25% 25% 42px;
          
          /* @screen sm {
              grid-template-columns: 142px auto 25% 25% 42px;
          }
      
          @screen md {
              grid-template-columns: 142px auto 25% 25% 42px;
          }
      
          @screen lg {
              grid-template-columns: 142px auto 25% 25% 42px;
          } */
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class FilialListaComponent extends Filterable implements AfterViewInit, IFilterable {
  @ViewChild('sidenav') public sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: FilialData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;

  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef,
    private _filialService: FilialService
  ) {
    super(_userService, 'filial')
    this.userSession = JSON.parse(this._userService.userSession);
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  ngAfterViewInit(): void { 
    this.registerEmitters();
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

  async obterDados(filtro: string = '') {
    this.isLoading = true;

    const parametros: FilialParameters = {
      ...{
        //this.dataSourceData = await this._filialService.obterPorParametros({
        pageNumber: this.paginator?.pageIndex + 1,
        sortActive: this.sort?.active || 'nomeFilial',
        sortDirection: this.sort?.direction || 'asc',
        pageSize: this.paginator?.pageSize,
        filter: filtro
      },
      ...this.filter?.parametros
    }
    
    //}).toPromise();
    const data = await this._filialService.obterPorParametros(parametros).toPromise();
      this.dataSourceData = data;
      this.isLoading = false;
      this._cdr.detectChanges();


      // this.isLoading = false;
      // this._cdr.detectChanges();
    }

    paginar() {
      this.obterDados();
    }
  }
