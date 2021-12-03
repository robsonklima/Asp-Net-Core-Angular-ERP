import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoRessalva, InstalacaoRessalvaData, InstalacaoRessalvaParameters } from '../types/instalacao-ressalva.types';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoRessalvaService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoRessalvaParameters): Observable<InstalacaoRessalvaData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoRessalva`, { params: params }).pipe(
      map((data: InstalacaoRessalvaData) => data)
    )
  }

  obterPorCodigo(codInstalacaoRessalva: number): Observable<InstalacaoRessalva> {
    const url = `${c.api}/InstalacaoRessalva/${codInstalacaoRessalva}`;
    return this.http.get<InstalacaoRessalva>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalLote: InstalacaoRessalva): Observable<InstalacaoRessalva> {
    return this.http.post<InstalacaoRessalva>(`${c.api}/InstalacaoRessalva`, instalLote).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalLote: InstalacaoRessalva): Observable<InstalacaoRessalva> {
    const url = `${c.api}/InstalacaoRessalva`;

    return this.http.put<InstalacaoRessalva>(url, instalLote).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalacaoRessalva: number): Observable<InstalacaoRessalva> {
    const url = `${c.api}/InstalacaoRessalva/${codInstalacaoRessalva}`;

    return this.http.delete<InstalacaoRessalva>(url).pipe(
      map((obj) => obj)
    );
  }
}
