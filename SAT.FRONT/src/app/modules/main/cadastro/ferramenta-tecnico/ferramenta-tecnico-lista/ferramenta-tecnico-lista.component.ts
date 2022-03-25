import { ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { FerramentaTecnicoService } from 'app/core/services/ferramenta-tecnico.service';
import { AcaoParameters } from 'app/core/types/acao.types';
import { FerramentaTecnicoData, FerramentaTecnicoParameters } from 'app/core/types/ferramenta-tecnico.types';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-ferramenta-tecnico-lista',
  templateUrl: './ferramenta-tecnico-lista.component.html',
  styles: [
    /* language=SCSS */
    `
    .list-grid-u {
      grid-template-columns: 50% 25% ;
      
      @screen sm {
          grid-template-columns: 50% 25% ;
      }
  
      @screen md {
          grid-template-columns: 50% 25% ;
      }
  
      @screen lg {
          grid-template-columns: 50% 25% ;
      }
  }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class FerramentaTecnicoListaComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: FerramentaTecnicoData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    private _ferramentaTecnicoService: FerramentaTecnicoService,
    private _cdr: ChangeDetectorRef
  ) { }

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
    const parametros: FerramentaTecnicoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: 'nome',
      sortDirection: 'asc',
      pageSize: this.paginator?.pageSize,
      filter: filtro,
      indAtivo: 1
    }

    const data = await this._ferramentaTecnicoService.obterPorParametros(parametros).toPromise();
    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  paginar() {
    this.obterDados();
  }
}
