import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Cargo, CargoData, CargoParameters } from '../types/cargo.types';
import { statusConst } from '../types/status-types';

@Injectable({
    providedIn: 'root'
})
export class CargoService {
    constructor(private http: HttpClient) { }

    obterPorParametros(parameters: CargoParameters): Observable<CargoData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/Cargo`, { params: params }).pipe(
            map((data: CargoData) => data)
        )
    }

    obterPorCodigo(codCargo: number): Observable<Cargo> {
        const url = `${c.api}/Cargo/${codCargo}`;
        return this.http.get<Cargo>(url).pipe(
            map((obj) => obj)
        );
    }

    criar(cargo: Cargo): Observable<Cargo> {
        return this.http.post<Cargo>(`${c.api}/Cargo`, cargo).pipe(
            map((obj) => obj)
        );
    }

    atualizar(cargo: Cargo): Observable<Cargo> {
        const url = `${c.api}/Cargo`;

        return this.http.put<Cargo>(url, cargo).pipe(
            map((obj) => obj)
        );
    }

    deletar(codCargo: number): Observable<Cargo> {
        const url = `${c.api}/Cargo/${codCargo}`;

        return this.http.delete<Cargo>(url).pipe(
            map((obj) => obj)
        );
    }

    async obterCargos(filtro: string = ''): Promise<Cargo[]> {

        const params: CargoParameters = {
            sortActive: 'nomeCargo',
            sortDirection: 'asc',
            indAtivo: statusConst.ATIVO,
            pageSize: 1000,
            filter: filtro
        }
        return (await this.obterPorParametros(params).toPromise()).items;
    }
}