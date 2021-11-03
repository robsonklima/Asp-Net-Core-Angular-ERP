import { OrdemServicoRelatorioInstalacao, OrdemServicoRelatorioInstalacaoItem } from './../types/ordem-servico.types';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class RelatorioInstalacaoItemService {
  constructor(
    private http: HttpClient
  ) {}

  obter(): Observable<OrdemServicoRelatorioInstalacaoItem[]> {
    return this.http.get<OrdemServicoRelatorioInstalacaoItem[]>(`${c.api}/OrdemServicoRelatorioInstalacaoItem`).pipe(
      map((obj) => obj)
    );
  }
}
