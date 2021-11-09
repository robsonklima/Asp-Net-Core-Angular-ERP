import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Monitoramento } from '../types/monitoramento.type';

@Injectable({
    providedIn: 'root'
})
export class MonitoramentoService {
    constructor(private http: HttpClient) { }
    obterListaMonitoramento(): Observable<Monitoramento> {
        const url = `${c.api}/Monitoramento`;
        return this.http.get<Monitoramento>(url).pipe(
            map((obj) => obj)
        );
    }
}
