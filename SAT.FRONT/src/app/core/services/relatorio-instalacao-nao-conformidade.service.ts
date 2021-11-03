import { OrdemServicoRelatorioInstalacao, OrdemServicoRelatorioInstalacaoNaoConformidade } from './../types/ordem-servico.types';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class RelatorioInstalacaoNaoConformidadeService {
  constructor(
    private http: HttpClient
  ) {}

  criar(relatorioInstalacao: OrdemServicoRelatorioInstalacaoNaoConformidade): Observable<OrdemServicoRelatorioInstalacaoNaoConformidade> {
    return this.http.post<OrdemServicoRelatorioInstalacaoNaoConformidade>(`${c.api}/OrdemServicoRelatorioInstalacaoNaoConformidade`, relatorioInstalacao).pipe(
      map((obj) => obj)
    );
  }
}
