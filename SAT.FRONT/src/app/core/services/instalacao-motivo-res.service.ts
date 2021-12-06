import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoMotivoRes, InstalacaoMotivoResData, InstalacaoMotivoResParameters } from '../types/instalacao-ressalva-motivo.types';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoMotivoResService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: InstalacaoMotivoResParameters): Observable<InstalacaoMotivoResData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoMotivoRes`, { params: params }).pipe(
      map((data: InstalacaoMotivoResData) => data)
    )
  }

  obterPorCodigo(codInstalacaoMotivoRes: number): Observable<InstalacaoMotivoRes> {
    const url = `${c.api}/InstalacaoMotivoRes/${codInstalacaoMotivoRes}`;
    return this.http.get<InstalacaoMotivoRes>(url).pipe(
      map((obj) => obj)
    );
}

  criar(instalLote: InstalacaoMotivoRes): Observable<InstalacaoMotivoRes> {
    return this.http.post<InstalacaoMotivoRes>(`${c.api}/InstalacaoMotivoRes`, instalLote).pipe(
      map((obj) => obj)
    );
  }

  atualizar(instalLote: InstalacaoMotivoRes): Observable<InstalacaoMotivoRes> {
    const url = `${c.api}/InstalacaoMotivoRes`;

    return this.http.put<InstalacaoMotivoRes>(url, instalLote).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalacaoMotivoRes: number): Observable<InstalacaoMotivoRes> {
    const url = `${c.api}/InstalacaoMotivoRes/${codInstalacaoMotivoRes}`;

    return this.http.delete<InstalacaoMotivoRes>(url).pipe(
      map((obj) => obj)
    );
  }
}
