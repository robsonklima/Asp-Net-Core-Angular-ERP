import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Setor, SetorData, SetorParameters } from '../types/setor.types';

@Injectable({
  providedIn: 'root'
})
export class SetorService {
  constructor(private http: HttpClient) { }

 
  obterPorParametros(parameters: SetorParameters): Observable<SetorData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Setor`, { params: params }).pipe(
      map((data: SetorData) => data)
    )
  }

  obterPorCodigo(codSetor: number): Observable<Setor> {
    const url = `${c.api}/Setor/${codSetor}`;
    return this.http.get<Setor>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(setor: Setor): Observable<Setor> {
    return this.http.post<Setor>(`${c.api}/Setor`, setor).pipe(
      map((obj) => obj)
    );
  }

  atualizar(setor: Setor): Observable<Setor> {
    const url = `${c.api}/Setor`;
    return this.http.put<Setor>(url, setor).pipe(
      map((obj) => obj)
    );
  }

  deletar(codSetor: number): Observable<Setor> {
    const url = `${c.api}/Setor/${codSetor}`;

    return this.http.delete<Setor>(url).pipe(
      map((obj) => obj)
    );
  }
}
