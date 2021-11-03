import { MatSidenav } from "@angular/material/sidenav";
import { UserSession } from "app/core/user/user.types";

export interface IFilterable
{
    filter: any;
    filterName: string;
    userSession: UserSession;
    sidenav: MatSidenav;

    carregaFiltro(): void;
}