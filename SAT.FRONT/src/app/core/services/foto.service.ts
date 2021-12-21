import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Foto } from '../types/foto.types';

@Injectable({
  providedIn: 'root'
})
export class FotoService {
  constructor(private http: HttpClient) {}

  obterPorCodigo(codFoto: number): Observable<Foto> {
    const url = `${c.api}/Foto/${codFoto}`;
    return this.http.get<Foto>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(foto: Foto): Observable<Foto> {
    return this.http.post<Foto>(`${c.api}/Foto`, foto).pipe(
      map((obj) => obj)
    );
  }

  deletar(codFoto: number): Observable<Foto> {
    const url = `${c.api}/Foto/${codFoto}`;

    return this.http.delete<Foto>(url).pipe(
      map((obj) => obj)
    );
  }
}
