import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrdemServico, OrdemServicoData, OrdemServicoParameters } from '../types/ordem-servico.types';

@Injectable({
  providedIn: 'root'
})
export class ImportacaoService
{
  constructor (
    private http: HttpClient
  ) { }

  aberturaChamadosEmMassa(aberturaChamadosEmMassa: any): Observable<any>
  {
    return this.http.post<any>(`${c.api}/Importacao/AberturaOs`, aberturaChamadosEmMassa).pipe(
      map((obj) => obj)
    );
  }
}
