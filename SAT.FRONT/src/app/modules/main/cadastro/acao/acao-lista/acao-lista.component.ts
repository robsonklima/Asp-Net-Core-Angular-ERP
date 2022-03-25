import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { AcaoService } from 'app/core/services/acao.service';
import { ExportacaoService } from 'app/core/services/exportacao.service';
import { AcaoData, AcaoParameters } from 'app/core/types/acao.types';
import { FileMime } from 'app/core/types/file.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-acao-lista',
  templateUrl: './acao-lista.component.html',
  styles: [
    /* language=SCSS */
    `
    .list-grid-u {
      grid-template-columns: 142px 80px 50% 25% 42px;
      
      @screen sm {
          grid-template-columns: 142px 80px 50% 25% 42px;
      }
  
      @screen md {
          grid-template-columns: 142px 80px 50% 25% 42px;
      }
  
      @screen lg {
          grid-template-columns: 142px 80px 50% 25% 42px;
      }
  }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class AcaoListaComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: AcaoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    private _acaoService: AcaoService,
    private _cdr: ChangeDetectorRef,
    private _exportacaoService: ExportacaoService,
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
        this.obterDados(text);
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
    const parametros: AcaoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: 'codEAcao',
      sortDirection: 'asc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    }

    const data = await this._acaoService.obterPorParametros(parametros).toPromise();
    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  public async exportar() {
    this.isLoading = true;
		window.location.href = await this._exportacaoService.exportar('Acao', FileMime.Excel, {});
    this.isLoading = false;
  }

  paginar() {
    this.obterDados();
  }
}
