import { EventEmitter } from "@angular/core";
import { AbstractControl, FormGroup } from "@angular/forms";
import { MatPaginator } from "@angular/material/paginator";
import { MatSidenav } from "@angular/material/sidenav";
import { MatSort } from "@angular/material/sort";
import { FilterBase } from "../filters/filter-base";
import { UserSession } from "../user/user.types";

export interface Filtro {
    parametros: Parameters;
    nome: string
}

export class Parameters {
    codFiliais?: string;
    codAutorizadas?: string;
    codTiposIntervencao?: number[];
    codClientes?: number[];
    codStatusServicos?: number[];
    codOS?: number;
    numOSCliente?: string;
    numOSQuarteirizada?: string;
    dataAberturaInicio?: string;
    dataAberturaFim?: string;
    dataFechamentoInicio?: string;
    dataFechamentoFim?: string;
    pa?: number;
    pontosEstrategicos?: number[];
    qtdPaginacaoLista?: number;
    sortActive?: string;
    sortDirection?: string;
    codTipoEquip?: string;
    codGrupoEquip?: string;
    codEquipamentos?: string;
    indAtivo?: number;
    indTecnicoLiberado?: number;
    codDespesaPeriodoStatus?: string;
    inicioPeriodo?: string;
    fimPeriodo?: string;
}

export interface IFilterBaseCore {
    form: FormGroup;
    filter: any;
    filterName: string;
    userSession: UserSession;
    sidenav: MatSidenav;
    meusFiltros: FiltroUsuarioData[];
    onRefreshFilter: EventEmitter<any>;

    aplicar(): void;
    limpar(): void;
    salvar(): void;
    remover(codFiltroUsuario: number): void;
    refreshFilter(): void;
    selectAll(select: AbstractControl, values, propertyName);
    onSelectFiltroUsuario(codFiltroUsuario: number);
}

export interface IFilterableCore {
    filter: Filtro;
    filterName: string;
    userSession: UserSession;

    sidenav: MatSidenav;
    paginator: MatPaginator;
    sort: MatSort;

    loadFilter(): void;
    onSortChanged(): void;
    onPaginationChanged(): void;
    onSidenavClosed(): void;
}

export interface IFilterBase {
    sidenav: MatSidenav;

    createForm(): void;
    loadData(): void;
}

export interface IFilterable {
    sidenav: MatSidenav;
    paginator: MatPaginator;
    sort: MatSort;

    registerEmitters(): void;
}

export class FiltroUsuarioData {
    codFiltroUsuario?: number;
    dadosJson: string;
    componenteFiltro: string;
    nomeFiltro?: string;
    codUsuario: string;
}