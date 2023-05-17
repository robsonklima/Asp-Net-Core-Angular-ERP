import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { EquipamentoPOS, EquipamentoPOSData, EquipamentoPOSParameters } from 'app/core/types/equipamento-pos.types'

@Injectable({
    providedIn: 'root'
})
export class EquipamentoPOSService {
    constructor(private http: HttpClient) { }

    criar(op: EquipamentoPOS): Observable<EquipamentoPOS> {
        return this.http.post<EquipamentoPOS>(`${c.api}/EquipamentoPOS`, op).pipe(
            map((obj) => obj)
        );
    }

    obterPorCodigo(cod: number): Observable<EquipamentoPOS> {
        const url = `${c.api}/EquipamentoPOS/${cod}`;
        return this.http.get<EquipamentoPOS>(url).pipe(
            map((obj) => obj)
        );
    }

    obterPorParametros(parameters: EquipamentoPOSParameters): Observable<EquipamentoPOSData> {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key => {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(`${c.api}/EquipamentoPOS`, { params: params }).pipe(
            map((data: EquipamentoPOSData) => data)
        )
    }

    atualizar(op: EquipamentoPOS): Observable<EquipamentoPOS> {
        const url = `${c.api}/EquipamentoPOS`;
        return this.http.put<EquipamentoPOS>(url, op).pipe(
            map((obj) => obj)
        );
    }

    deletar(cod: number): Observable<EquipamentoPOS> {
        const url = `${c.api}/EquipamentoPOS/${cod}`;

        return this.http.delete<EquipamentoPOS>(url).pipe(
            map((obj) => obj)
        );
    }
}