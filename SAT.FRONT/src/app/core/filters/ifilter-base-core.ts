import { AbstractControl, FormGroup } from "@angular/forms";
import { MatSidenav } from "@angular/material/sidenav";
import { UserSession } from "app/core/user/user.types";

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