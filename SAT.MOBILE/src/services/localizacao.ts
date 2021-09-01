import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from "rxjs/Observable";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Localizacao } from '../models/localizacao';


@Injectable()
export class LocalizacaoService {
  constructor(
    private http: Http
  ) { }

  buscarLocalizacoesApi(codUsuario: string): Observable<Localizacao[]> {
    return this.http.get(Config.API_URL + 'LocalizacaoTecnico/' + codUsuario + '/')
      .timeout(15000)
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json())
    );
  }
}