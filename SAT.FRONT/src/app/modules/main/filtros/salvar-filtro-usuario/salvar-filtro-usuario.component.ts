import { AfterViewInit, Component, Injectable, Input, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSelect } from '@angular/material/select';
import { MatSidenav } from '@angular/material/sidenav';
import { FilterBase } from 'app/core/filters/filter-base';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { FiltroService } from 'app/core/services/filtro.service';
import Enumerable from 'linq';

@Component({
    selector: 'salvar-filtro-usuario',
    templateUrl: './salvar-filtro-usuario.component.html'
})

@Injectable()
export class SalvarFiltroUsuarioComponent implements AfterViewInit
{

    @Input() filterName: string;
    @Input() filterBase: FilterBase;

    @ViewChild("selectFiltro") selectFiltroElement: MatSelect;

    private _sidenav: MatSidenav;
    public disableSelection: boolean = false;

    constructor (
        protected _dialog: MatDialog,
        protected _filtroService: FiltroService,
        protected _snack: CustomSnackbarService
    ) { }

    ngAfterViewInit(): void
    {

        this.filterBase.configurarFiltrosUsuario(this.filterName, this._dialog, this._filtroService, this._snack, this.selectFiltroElement);

        this._sidenav = this.filterBase.sidenav;

        this.filterBase.onAplicar.subscribe(this.aplicar);

        this.filterBase.onLimpar.subscribe(this.limpar);

        this._sidenav.closedStart.subscribe(() =>
        {
            this.disableSelection = false;
        })

        this._sidenav.openedStart.subscribe(() =>
        {
            this.filterBase.refreshFilter();
        });

        this.filterBase.onRefreshFilter.subscribe(() =>
        {
            this.filterBase.meusFiltros = this.filterBase.getFiltros();
            if (this.filterBase.meusFiltros.length == 0) this.filterBase.registerUsuarioFiltro(0);
            if (this.selectFiltroElement) this.selectFiltroElement.value = this.filterBase.obterUsuarioFiltro();
        });
    }

    aplicar(base: FilterBase)
    {
        base.salvar(base.selectFiltroElement.value,
            Enumerable.from(base.getFiltros())
                .firstOrDefault(f => f.codFiltroUsuario == (base.selectFiltroElement.value || 0))?.nomeFiltro || '');
    }

    limpar(base: FilterBase)
    {
        if (base.selectFiltroElement)
        {
            base.selectFiltroElement.value = 0;
            base.registerUsuarioFiltro(base.selectFiltroElement.value);
        }
    }

    remover(codFiltroUsuario: number)
    {
        this.filterBase.remover(codFiltroUsuario);
        this.selectFiltroElement.close();
    }
}