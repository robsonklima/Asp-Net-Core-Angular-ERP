import { OrdemServicoRelatorioInstalacao } from './../types/ordem-servico.types';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { OrdemServico, OrdemServicoData, OrdemServicoParameters } from '../types/ordem-servico.types';

@Injectable({
  providedIn: 'root'
})
export class RelatorioInstalacaoService {
  constructor(
    private http: HttpClient
  ) {}

  criar(relatorioInstalacao: OrdemServicoRelatorioInstalacao): Observable<OrdemServicoRelatorioInstalacao> {
    return this.http.post<OrdemServicoRelatorioInstalacao>(`${c.api}/OrdemServicoRelatorioInstalacao`, relatorioInstalacao).pipe(
      map((obj) => obj)
    );
  }
}
