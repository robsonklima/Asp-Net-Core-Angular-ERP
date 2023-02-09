import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { InstalacaoAnexo, InstalacaoAnexoData, InstalacaoAnexoParameters } from '../types/instalacao-anexo.types';
import { ImagemPerfilModel } from 'app/modules/main/dialog/dialog-alterar-foto-perfil/dialog-alterar-foto-perfil.component';

@Injectable({
  providedIn: 'root'
})
export class InstalacaoAnexoService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: InstalacaoAnexoParameters): Observable<InstalacaoAnexoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/InstalacaoAnexo`, { params: params }).pipe(
      map((data: InstalacaoAnexoData) => data)
    )
  }

  obterPorCodigo(codInstalAnexo: number): Observable<InstalacaoAnexo> {
    const url = `${c.api}/InstalacaoAnexo/${codInstalAnexo}`;
    return this.http.get<InstalacaoAnexo>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(instalacaoAnexo: InstalacaoAnexo): Observable<InstalacaoAnexo> {
    return this.http.post<InstalacaoAnexo>(`${c.api}/InstalacaoAnexo`, instalacaoAnexo).pipe(
      map((obj) => obj)
    );
  }

  deletar(codInstalAnexo: number): Observable<InstalacaoAnexo> {
    const url = `${c.api}/InstalacaoAnexo/${codInstalAnexo}`;

    return this.http.delete<InstalacaoAnexo>(url).pipe(
      map((obj) => obj)
    );
  }
}
