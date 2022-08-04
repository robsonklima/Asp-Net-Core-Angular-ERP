import { FileMime } from './../../../../../core/types/file.types';
import { AfterViewChecked, AfterViewInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { PecaService } from 'app/core/services/peca.service';
import { PecaData, PecaParameters, PecaStatus } from 'app/core/types/peca.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';

@Component({
  selector: 'app-peca-lista',
  templateUrl: './peca-lista.component.html',
  styles: [
    /* language=SCSS */
    `
      .list-grid-ge {
          grid-template-columns: 15% auto 15%;
          
          /* @screen sm {
              grid-template-columns: 15% auto 15%;
          }
      
          @screen md {
              grid-template-columns: 15% auto 15%;
          }
      
          @screen lg {
              grid-template-columns: 15% auto 15%;
          } */
      }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class PecaListaComponent extends Filterable implements OnInit, AfterViewInit, IFilterable 
{
  @ViewChild('sidenav') public sidenav: MatSidenav;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  @ViewChild(MatSort) sort: MatSort;
  dataSourceData: PecaData;
  byteArray;
  isLoading: boolean = false;
  pecaStatus: string[] = [];
  
  constructor(
    protected _userService: UserService,
    private _cdr: ChangeDetectorRef, 
    private _pecaService: PecaService,
    private _exportacaoService: ExportacaoService

    ) {
      super(_userService, 'peca')
      this.userSession = JSON.parse(this._userService.userSession);
     }

  ngOnInit(): void { }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterDados();
    })
  }

  ngAfterViewInit(): void {
    this.registerEmitters();
    this.obterDados();
    this.obterStatus();

    if (this.sort && this.paginator) 
    {
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

  public async obterDados(filtro: string = '') 
  {
    this.isLoading = true;

    const params: PecaParameters = 
    {
      ...{
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: this.sort?.active || 'codPeca',
      sortDirection: this.sort?.direction || 'desc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    },
    ...this.filter?.parametros
    }    
    console.log(this.filter?.parametros);
    

    this.dataSourceData = await this._pecaService.obterPorParametros(params).toPromise();
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  private async obterStatus(): Promise<void>
  {
    Object.keys(PecaStatus)
    .filter((e) => isNaN(Number(e)))
    .forEach((tr) => 
      this.pecaStatus.push(tr));
  }

  // public async exportar()
  // {      
  //   this.isLoading = true;
    
  //   const params: PecaParameters = 
  //   {
  //     sortDirection: 'desc',
  //     pageSize: 1000
  //   }

  //   this.isLoading = false;
  // }

  async exportar() {
    this.isLoading = true;

    await this._exportacaoService.exportar('Peca', FileMime.Excel, this.filter?.parametros);

    this.isLoading = false;
  }

  public paginar() { this.obterDados();  }
}
