import { ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { AcaoComponenteService } from 'app/core/services/acao-componente.service';
import { AcaoComponente, AcaoComponenteData } from 'app/core/types/acao-componente.types';
import { AcaoParameters } from 'app/core/types/acao.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-acao-causa-lista',
  templateUrl: './acao-causa-lista.component.html',
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
export class AcaoCausaListaComponent {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) private sort: MatSort;
  dataSourceData: AcaoComponenteData;
  isLoading: boolean = false;
  @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
  userSession: UserSession;

  constructor(
    private _acaoComponenteService: AcaoComponenteService,
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

    const parametros: AcaoParameters = {
      pageNumber: this.paginator?.pageIndex + 1,
      sortActive: 'codAcaoComponente',
      sortDirection: 'asc',
      pageSize: this.paginator?.pageSize,
      filter: filtro
    }

    const data = await this._acaoComponenteService.obterPorParametros(parametros).toPromise();
    this.dataSourceData = data;
    this.isLoading = false;
    this._cdr.detectChanges();
  }

  buscaNomeAcao(dados: AcaoComponente) {
    return dados.acao?.codEAcao + ' - ' + dados.acao?.nomeAcao;
  }

  buscaSelecionado(dados: AcaoComponente) {
    return dados.selecionado == 1 ? 'Sim' : "Não";
  }

  paginar() {
    this.obterDados();
  }
}