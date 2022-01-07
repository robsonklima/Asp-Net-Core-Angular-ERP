import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Laudo } from '../types/laudo.types';

@Injectable({
  providedIn: 'root'
})
export class LaudoService
{
  constructor (
    private http: HttpClient
  ) { }

  obterPorCodigo(codLaudo: number): Observable<Laudo>
  {
    const url = `${c.api}/Laudo/${codLaudo}`;

    return this.http.get<Laudo>(url).pipe(
      map((obj) => obj)
    );
  }
}
