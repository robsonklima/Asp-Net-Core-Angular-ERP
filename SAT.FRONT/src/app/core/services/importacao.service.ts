import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { ImportacaoAberturaOrdemServico } from '../types/importacao.types';

@Injectable({
  providedIn: 'root'
})
export class ImportacaoService
{
  constructor (
    private http: HttpClient
  ) { }

  aberturaChamadosEmMassa(aberturaChamadosEmMassa: object): Observable<object[]>
  {
    return this.http.post<object[]>(`${c.api}/Importacao/AberturaOs`, aberturaChamadosEmMassa).pipe(
      map((obj) => obj)
    );
  }
}
