import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServicoData } from 'app/core/types/ordem-servico.types';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class OrdensServicoResolver implements Resolve<any>
{
    constructor(private _ordemServicoService: OrdemServicoService) {}

    resolve(
        route: ActivatedRouteSnapshot, 
        state: RouterStateSnapshot
    ): Observable<OrdemServicoData> {
        return null;
    }
}
