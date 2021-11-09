import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PontoUsuarioDataMotivoDivergenciaData, PontoUsuarioDataMotivoDivergenciaParameters } from '../types/ponto-usuario-data-motivo-divergencia.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class PontoUsuarioDataMotivoDivergenciaService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: PontoUsuarioDataMotivoDivergenciaParameters): Observable<PontoUsuarioDataMotivoDivergenciaData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PontoUsuarioDataMotivoDivergencia`, { params: params }).pipe(
      map((data: PontoUsuarioDataMotivoDivergenciaData) => data)
    )
  }
}
