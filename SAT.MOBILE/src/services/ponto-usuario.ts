import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/Rx';

import { Config } from '../models/config';

import { Observable } from "rxjs/Observable";
import { PontoUsuario } from '../models/ponto-usuario';


@Injectable()
export class PontoUsuarioService {
  constructor(
    private http: Http
  ) {}

  buscarPontosPorUsuarioApi(codUsuario: string): Observable<PontoUsuario[]> {
    return this.http.get(Config.API_URL + 'PontoUsuario/' + codUsuario + '/')
      .timeout(10000)
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json()));
  }

  enviarPontoUsuarioApi(pontoUsuario: PontoUsuario): Observable<PontoUsuario> {
    return this.http.post(Config.API_URL + 'PontoUsuario', pontoUsuario)
      .map((res: Response) => {return res.json()})
      .catch((error: any) => {return Observable.throw(error)});
  }

  deletarPontoUsuarioApi(codPontoUsuario: number): Observable<string> {
    return this.http.delete(Config.API_URL + 'PontoUsuario/' + codPontoUsuario)
      .map((res: Response) => {return res.json()})
      .catch((error: any) => {return Observable.throw(error)});
  }
}