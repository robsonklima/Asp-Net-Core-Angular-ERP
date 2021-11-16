import { AbstractControl, FormGroup } from "@angular/forms";
import { MatPaginator } from "@angular/material/paginator";
import { MatSidenav } from "@angular/material/sidenav";
import { MatSort } from "@angular/material/sort";
import { UserSession } from "../user/user.types";

export interface Filtro
{
    parametros: Parameters;
    nome: string
}

export class Parameters 
{
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

export interface IFilterBaseCore
{
    form: FormGroup;
    filter: any;
    filterName: string;
    userSession: UserSession;
    sidenav: MatSidenav;

    apply(): void;
    clean(): void;
    selectAll(select: AbstractControl, values, propertyName);
}

export interface IFilterableCore
{
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

export interface IFilterBase
{
    sidenav: MatSidenav;

    createForm(): void;
    loadData(): void;
}

export interface IFilterable
{
    sidenav: MatSidenav;
    paginator: MatPaginator;
    sort: MatSort;

    registerEmitters(): void;
}