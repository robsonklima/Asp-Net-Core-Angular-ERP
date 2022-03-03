import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Cliente, ClienteData, ClienteParameters } from '../types/cliente.types';
import { appConfig as c } from 'app/core/config/app.config'
import { Contrato, ContratoData } from '../types/contrato.types';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  constructor(private http: HttpClient) { }

  obterTodos(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(`${c.api}/Cliente`).pipe(
      map((obj) => obj)
    );
  }

  obterPorParametros(parameters: ClienteParameters): Observable<ClienteData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Cliente`, { params: params }).pipe(
      map((data: ClienteData) => data)
    )
  }

  obterPorCodigo(codCliente: number): Observable<Cliente> {
    const url = `${c.api}/Cliente/${codCliente}`;
    return this.http.get<Cliente>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(cliente: Cliente): Observable<Cliente> {
    return this.http.post<Cliente>(`${c.api}/Cliente`, cliente).pipe(
      map((obj) => obj)
    );
  }

  atualizar(cliente: Cliente): Observable<Cliente> {
    const url = `${c.api}/Cliente`;

    return this.http.put<Cliente>(url, cliente).pipe(
      map((obj) => obj)
    );
  }

  deletar(codCliente: number): Observable<Cliente> {
    const url = `${c.api}/Cliente/${codCliente}`;

    return this.http.delete<Cliente>(url).pipe(
      map((obj) => obj)
    );
  }
}
