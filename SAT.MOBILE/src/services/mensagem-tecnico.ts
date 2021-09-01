import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/catch';
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { MensagemTecnico } from '../models/mensagem-tecnico';


@Injectable()
export class MensagemTecnicoService {
  constructor(
    private http: Http
  ) { }

  buscarMensagensTecnicoApi(codUsuario: string): Observable<MensagemTecnico[]> {
    return this.http.get(Config.API_URL + 'MensagemTecnico/' + codUsuario + '/')
      .map((res: Response) => {
        return res.json()
      }).catch((error: any) => {
        return Observable.throw(error);
     });
  }

  enviarMensagemTecnicoApi(mensagemTecnico: MensagemTecnico): Observable<any> {
    return this.http.post(Config.API_URL + 'MensagemTecnico', mensagemTecnico)
    .map((res: Response) => {
      return res.json()
    })
    .catch((error: Error) => {
      return Observable.throw(error);
    });
  }
}