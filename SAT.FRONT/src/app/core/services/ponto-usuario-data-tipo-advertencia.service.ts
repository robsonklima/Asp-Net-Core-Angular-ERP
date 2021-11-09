import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { PontoUsuarioDataTipoAdvertenciaData, PontoUsuarioDataTipoAdvertenciaParameters } from '../types/ponto-usuario-data-tipo-advertencia.types';
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class PontoUsuarioDataTipoAdvertenciaService {
  constructor(private http: HttpClient) {}

  obterPorParametros(parameters: PontoUsuarioDataTipoAdvertenciaParameters): Observable<PontoUsuarioDataTipoAdvertenciaData> {
    let params = new HttpParams();
    
    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PontoUsuarioDataTipoAdvertencia`, { params: params }).pipe(
      map((data: PontoUsuarioDataTipoAdvertenciaData) => data)
    )
  }
}
