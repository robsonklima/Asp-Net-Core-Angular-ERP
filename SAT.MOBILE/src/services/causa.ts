import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { Causa } from "../models/causa";

@Injectable()
export class CausaService {
  private causas: Causa[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarCausasApi(): Observable<Causa[]> {
    return this.http.get(Config.API_URL + 'Causa')
      .timeout(30000)
      .map((res: Response) => {
        this.storage.set('Causas', res.json())
          .then()
          .catch()
      })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarCausasStorage() {
    return this.storage.get('Causas')
      .then((causas: Causa[]) => {
        this.causas = causas != null ? causas : [];
        return this.causas.slice();
      })
      .catch();
  }

  causaEstaNoStorage(codCausa: number): boolean {
    let causaEncontrada: boolean = false;

    this.causas.forEach(causa => {
      if (causa.codCausa === codCausa) {
        causaEncontrada = true;
      }
    });

    return causaEncontrada;
  }

  apagarCausasStorage(): Promise<any> {
    return new Promise((resolve, reject) => {
      for (var i = 0; i < this.causas.length; i++) {
        this.causas.splice(i, 100);
      }

      this.storage.set('Causas', this.causas)
        .then((res) => {
          resolve(res);
        })
        .catch();
    });
  }

  carregarCausas(): Promise<Causa[]> {
    return new Promise((resolve, reject) => {
      resolve(this.causas.slice())
    });
  }

  atualizarCausa(causa: Causa) {
    this.storage.get('Causas')
      .then((causas: Causa[]) => {
        causas.forEach((c, i) => {
          if (causa.codCausa == c.codCausa) {
            this.causas.splice(i, 1);
            this.causas.push(causa);
            this.storage.set('Causas', this.causas)
              .then(() => {})
              .catch();
          }
        });
      })
      .catch();
  }
}