import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { Defeito } from "../models/defeito";

@Injectable()
export class DefeitoService {
  private defeitos: Defeito[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarDefeitosApi(): Observable<Defeito[]> {
    return this.http.get(Config.API_URL + 'Defeito')
      .timeout(30000)
      .map((res: Response) => {
        this.insereDefeitosStorage(res.json());
      })
      .catch((error: any) => Observable.throw(error.json()));
  }

  insereDefeitosStorage(defeitos: Defeito[]) {
    defeitos.forEach(defeito => {
      if (!this.defeitoEstaNoStorage(defeito.codDefeito)) {
        this.defeitos.push(defeito);
      }
    });

    this.storage.set('Defeitos', this.defeitos)
      .then()
      .catch();
  }

  buscarDefeitosStorage() {
    return this.storage.get('Defeitos')
      .then(
      (defeitos: Defeito[]) => {
        this.defeitos = defeitos != null ? defeitos : [];
        return this.defeitos.slice();
      })
      .catch();
  }

  defeitoEstaNoStorage(codDefeito: number): boolean {
    let defeitoEncontrado: boolean = false;

    this.defeitos.forEach(defeito => {
      if (defeito.codDefeito === codDefeito) {
        defeitoEncontrado = true;
      }
    });

    return defeitoEncontrado;
  }

  apagarDefeitosStorage(): Promise<any> {
    return new Promise((resolve, reject) => {
      for (var i = 0; i < this.defeitos.length; i++) {
        this.defeitos.splice(i, 100);
      }

      this.storage.set('Defeitos', this.defeitos)
        .then((res) => {
          resolve(res);
        })
        .catch();
    });
  }

  carregarDefeitos(): Promise<Defeito[]> {
    return new Promise((resolve, reject) => {
      resolve(this.defeitos.slice())
    });
  }

  atualizarDefeito(defeito: Defeito) {
    this.storage.get('Defeitos')
      .then((defeitos: Defeito[]) => {
        defeitos.forEach((d, i) => {
          if (defeito.codDefeito == d.codDefeito) {
            this.defeitos.splice(i, 1);
            this.defeitos.push(defeito);
            this.storage.set('Defeitos', this.defeitos)
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