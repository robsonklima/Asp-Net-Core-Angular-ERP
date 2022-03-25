import { ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { DefeitoComponenteService } from 'app/core/services/defeito-componente.service';
import { DefeitoComponente, DefeitoComponenteData } from 'app/core/types/defeito-componente.types';
import { DefeitoParameters } from 'app/core/types/defeito.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-defeito-causa-lista',
  templateUrl: './defeito-causa-lista.component.html',
  styles: [
    /* language=SCSS */
    `
    .list-grid-u {
      grid-template-columns: 142px 50px 20% 20% 20%;
      
      @screen sm {
          grid-template-columns: 142px 50px 20% 20% 20%;
      }
  
      @screen md {
          grid-template-columns: 142px  50px 20% 20% 20%;
      }
  
      @screen lg {
          grid-template-columns: 142px  50px 20% 20% 20%;
      }
  }
    `
  ],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class DefeitoCausaListaComponent {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: DefeitoComponenteData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    private _defeitoComponenteService: DefeitoComponenteService,
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

  async obterDados(filtro: string = '') {
    this.isLoading = true;

    const parametros: DefeitoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: 'codDefeitoComponente',
      sortDirection: 'asc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    }

    const data = await this._defeitoComponenteService.obterPorParametros(parametros).toPromise();
    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  buscaNomeDefeito(dados: DefeitoComponente) {
    return dados.defeito?.codEDefeito + ' - ' + dados.defeito?.nomeDefeito;
  }

  buscaSelecionado(dados: DefeitoComponente) {
    return dados.selecionado == 1 ? 'Sim' : "Não";
  }

  paginar() {
    this.obterDados();
  }
}