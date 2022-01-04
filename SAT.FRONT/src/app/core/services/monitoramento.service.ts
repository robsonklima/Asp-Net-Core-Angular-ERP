import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Monitoramento, MonitoramentoClienteViewModel as MonitoramentoClienteViewModel } from '../types/monitoramento.types';

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

    obterListaMonitoramentoClientes(): Observable<MonitoramentoClienteViewModel[]> {
        const url = `${c.api}/Monitoramento/GetMonitoramentoClientes`;
        return this.http.get<MonitoramentoClienteViewModel[]>(url).pipe(
            map((obj) => obj)
        );
    }
}
