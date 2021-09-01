import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { Peca } from "../models/peca";

@Injectable()
export class PecaService {
  private pecas: Peca[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarPecasApi(): Observable<Peca[]> {
    return this.http.get(Config.API_URL + 'Peca')
      .timeout(30000)
      .map((res: Response) => {
        this.inserePecasStorage(res.json());
      })
      .catch((error: any) => Observable.throw(error.json()));
  }

  inserePecasStorage(pecas: Peca[]) {
    pecas.forEach(peca => {
      if (!this.pecaEstaNoStorage(peca.codPeca)) {
        this.pecas.push(peca);
      }
    });

    this.storage.set('Pecas', this.pecas)
      .then()
      .catch();
  }

  buscarPecasStorage() {
    return this.storage.get('Pecas').then((pecas: Peca[]) => {
      this.pecas = pecas != null ? pecas : [];
      return this.pecas.slice();
    }).catch();
  }

  pecaEstaNoStorage(codPeca: number): boolean {
    let pecaEncontrada: boolean = false;

    this.pecas.forEach(peca => {
      if (peca.codPeca === codPeca) {
        pecaEncontrada = true;
      }
    });

    return pecaEncontrada;
  }

  apagarPecasStorage(): Promise<any> {
    return new Promise((resolve, reject) => {
      for (var i = 0; i < this.pecas.length; i++) {
        this.pecas.splice(i, 100);
      }

      this.storage.set('Pecas', this.pecas)
        .then((res) => {
          resolve(res);
        })
        .catch(() => {
          reject();
        });
    });
  }

  carregarPecas(): Promise<Peca[]> {
    return new Promise((resolve, reject) => {
      resolve(this.pecas.slice());
    });
  }

  atualizarPeca(peca: Peca) {
    this.storage.get('Pecas')
      .then((pecas: Peca[]) => {
        pecas.forEach((p, i) => {
          if (peca.codPeca == p.codPeca) {
            this.pecas.splice(i, 1);
            this.pecas.push(peca);
            this.storage.set('Pecas', this.pecas)
              .then(
                () => {}
              )
              .catch(

              );

          }
        });
      })
      .catch();
  }
}