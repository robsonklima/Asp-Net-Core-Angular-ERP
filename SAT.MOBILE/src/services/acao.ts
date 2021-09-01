import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { Acao } from "../models/acao";

@Injectable()
export class AcaoService {
  private acoes: Acao[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarAcoesApi(): Observable<Acao[]> {
    return this.http.get(Config.API_URL + 'Acao')
      .timeout(30000)
      .map((res: Response) => { this.storage.set('Acoes', res.json()).catch() })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarAcoesStorage() {
    return this.storage.get('Acoes').then((acoes: Acao[]) => {
      this.acoes = acoes != null ? acoes : [];
      return this.acoes.slice();
    })
    .catch();
  }

  apagarAcoesStorage(): Promise<any> {
    return new Promise((resolve, reject) => {
      for (var i = 0; i < this.acoes.length; i++) {
        this.acoes.splice(i, 100);
      }

      this.storage.set('Acoes', this.acoes)
        .then((res) => {
          resolve(res);
        })
        .catch();
    });
  }

  carregarAcoes(): Promise<Acao[]> {
    return new Promise((resolve, reject) => {
      resolve(this.acoes.slice())
    });
  }
}