import {
    AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
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
    styleUrls: ['./ordem-servico-lista.component.scss'],
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
    @ViewChild('searchInputControl', { static: true }) searchInputControl: ElementRef;

    constructor(
        private _cdr: ChangeDetectorRef,
        private _ordemServicoService: OrdemServicoService,
        private _userService: UserService
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngAfterViewInit(): void {
        this.carregarFiltro();
        this.obterOrdensServico();
        this.registrarEmitters();

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
                this.obterOrdensServico();
            });

            this.sort.disableClear = true;
            this._cdr.markForCheck();

            this.sort.sortChange.subscribe(() => {
                this.paginator.pageIndex = 0;
                this.obterOrdensServico();
                this.fecharDetalhes();
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
            filter: this.searchInputControl.nativeElement.val
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
            this.filtro.parametros.codFiliais = [ this.userSession.usuario.codFilial ]
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

    async alternarDetalhes(codigo: number) {
        this.isLoading = true;

        if (this.selectedItem && this.selectedItem.codOS === codigo) {
            this.isLoading = false;
            this.fecharDetalhes();
            return;
        }

        const os: OrdemServico = await this._ordemServicoService
            .obterPorCodigo(codigo)
            .toPromise();

        this.selectedItem = os;
        this.isLoading = false;
    }

    fecharDetalhes(): void {
        this.selectedItem = null;
    }
}
