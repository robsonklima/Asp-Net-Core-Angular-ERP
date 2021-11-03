import { MatSidenav } from "@angular/material/sidenav";
import { UserSession } from "app/core/user/user.types";
import { Filtro } from "../types/filtro.types";

export interface IFilterableCore
{
    filter: Filtro;
    filterName: string;
    userSession: UserSession;
    sidenav: MatSidenav;

    carregaFiltro(): void;
}