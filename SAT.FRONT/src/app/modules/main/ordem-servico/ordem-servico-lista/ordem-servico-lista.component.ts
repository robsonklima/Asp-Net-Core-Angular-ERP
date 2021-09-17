import {
    AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';
import { fuseAnimations } from '@fuse/animations';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { MatSort } from '@angular/material/sort';
import { OrdemServico, OrdemServicoData, OrdemServicoParameters } from 'app/core/types/ordem-servico.types';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
    selector: 'ordem-servico-lista',
    templateUrl: './ordem-servico-lista.component.html',
    styles: [`
        .list-grid-ordem-servico {
            grid-template-columns: 48px 72px 72px 92px 28px 48px 116px 36px auto 56px 136px 36px 136px 24px;
            
            @screen sm {
                grid-template-columns: 48px auto 32px;
            }
        
            @screen md {
                grid-template-columns: 48px 72px 72px 92px 38px 36px auto 58px 58px 58px 58px 58px;
            }
        
            @screen lg {
                grid-template-columns: 48px 72px 72px 92px 28px 48px 116px 36px auto 56px 136px 36px 136px 24px;
            }
        }
    `],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class OrdemServicoListaComponent implements AfterViewInit {
    @ViewChild('sidenav') sidenav: MatSidenav;
    userSession: UserSession;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) private sort: MatSort;
    dataSourceData: OrdemServicoData;
    selectedItem: OrdemServico | null = null;
    filtro: any;
    isLoading: boolean = false;
    protected _onDestroy = new Subject<void>();

    constructor(
        private _cdr: ChangeDetectorRef,
        private _ordemServicoService: OrdemServicoService,
        private _userService: UserService
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngAfterViewInit(): void {
        this.carregarFiltro();

        interval(5 * 60 * 1000)
            .pipe(
                startWith(0),
                takeUntil(this._onDestroy)
            )
            .subscribe(() => {
                this.obterOrdensServico();
            });

        this.registrarEmitters();

        if (this.sort && this.paginator) {
            this.sort.disableClear = true;
            this._cdr.markForCheck();

            this.sort.sortChange.subscribe(() => {
                this.paginator.pageIndex = 0;
                this.obterOrdensServico();
            });
        }

        this._cdr.detectChanges();
    }

    async obterOrdensServico() {
        this.isLoading = true;

        const params: OrdemServicoParameters = {
            pageNumber: this.paginator.pageIndex + 1,
            sortActive: this.sort.active || 'codOS',
            sortDirection: this.sort.direction || 'desc',
            pageSize: this.paginator?.pageSize,
        };

        const data: OrdemServicoData = await this._ordemServicoService
            .obterPorParametros({
                ...params,
                ...this.filtro?.parametros
            })
            .toPromise();

        this.dataSourceData = data;
        this.isLoading = false;
    }

    private registrarEmitters(): void {
        // Quando o sidebar fecha
        this.sidenav.closedStart.subscribe(() => {
            this.paginator.pageIndex = 0;
            this.carregarFiltro();
            this.obterOrdensServico();
        })
    }

    private carregarFiltro(): void {
        this.filtro = this._userService.obterFiltro('ordem-servico');

        if (!this.filtro) {
            return;
        }

        // Filtro obrigatorio de filial quando o usuario esta vinculado a uma filial
        if (this.userSession?.usuario?.codFilial) {
            this.filtro.parametros.codFiliais = [this.userSession.usuario.codFilial]
        }

        Object.keys(this.filtro?.parametros).forEach((key) => {
            if (this.filtro.parametros[key] instanceof Array) {
                this.filtro.parametros[key] = this.filtro.parametros[key].join()
            };
        });
    }

    paginar() {
        this.obterOrdensServico();
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
