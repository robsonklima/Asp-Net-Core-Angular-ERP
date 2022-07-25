import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSidenav } from '@angular/material/sidenav';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { Filterable } from 'app/core/filters/filterable';
import { DefeitoComponenteService } from 'app/core/services/defeito-componente.service';
import { DefeitoComponente, DefeitoComponenteData, DefeitoComponenteParameters } from 'app/core/types/defeito-componente.types';
import { IFilterable } from 'app/core/types/filtro.types';
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
      
     /* @screen sm {
          grid-template-columns: 142px 50px 20% 20% 20%;
      }
  
      @screen md {
          grid-template-columns: 142px  50px 20% 20% 20%;
      }
  
      @screen lg {
          grid-template-columns: 142px  50px 20% 20% 20%;
      }*/
  }
    `
    ],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class DefeitoCausaListaComponent extends Filterable implements AfterViewInit, IFilterable {

    @ViewChild('sidenav') sidenav: MatSidenav;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;
    @ViewChild(MatSort) sort: MatSort;
    dataSourceData: DefeitoComponenteData;
    isLoading: boolean = false;

    userSession: UserSession;

    constructor(
        protected _userService: UserService,
        private _defeitoComponenteService: DefeitoComponenteService,
        private _cdr: ChangeDetectorRef
        
    ) {
        super(_userService, 'defeito-causa')
        this.userSession = JSON.parse(this._userService.userSession);
    }

    registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
			this.obterDados();
		})
	}


    async ngAfterViewInit() {
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

        const parametros: DefeitoComponenteParameters = {
            ...{
                pageNumber: this.paginator?.pageIndex + 1,
                sortActive: 'codDefeitoComponente',
                sortDirection: 'asc',
                pageSize: this.paginator?.pageSize,
                filter: filtro
            },
            ...this.filter?.parametros
            
        }      

        const data = await this._defeitoComponenteService.obterPorParametros(parametros).toPromise();
        this.dataSourceData = data;

        console.log(data);
        
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