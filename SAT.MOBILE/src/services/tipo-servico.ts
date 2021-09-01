import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';
import 'rxjs/add/operator/retry';
import 'rxjs/add/operator/timeout';
import 'rxjs/add/operator/delay';
import 'rxjs/add/operator/map';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { TipoServico } from "../models/tipo-servico";

@Injectable()
export class TipoServicoService {
  private tipoServicos: TipoServico[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarTipoServicosApi(): Observable<TipoServico[]> {
    return this.http.get(Config.API_URL + 'TipoServico')
      .timeout(15000)
      .map((res: Response) => {
        this.inseretipoServicosStorage(res.json());
      })
      .catch((error: any) => Observable.throw(error.json()));
  }

  inseretipoServicosStorage(tipoServicos: TipoServico[]) {
    tipoServicos.forEach(tipoServico => {
      if (!this.tipoServicoEstaNoStorage(tipoServico.codTipoServico)) {
        this.tipoServicos.push(tipoServico);
      }
    });

    this.storage.set('TipoServicos', this.tipoServicos)
      .then()
      .catch();
  }

  buscarTipoServicosStorage() {
    return this.storage.get('TipoServicos').then((causas: TipoServico[]) => {
      this.tipoServicos = causas != null ? causas : [];
      return this.tipoServicos.slice();
    }).catch();
  }

  tipoServicoEstaNoStorage(codTipoServico: number): boolean {
    let causaEncontrada: boolean = false;

    this.tipoServicos.forEach(tipoServico => {
      if (tipoServico.codTipoServico === codTipoServico) {
        causaEncontrada = true;
      }
    });

    return causaEncontrada;
  }

  apagarTipoServicosStorage(): Promise<any> {
    return new Promise((resolve, reject) => {
      for (var i = 0; i < this.tipoServicos.length; i++) {
        this.tipoServicos.splice(i, 100);
      }

      this.storage.set('TipoServicos', this.tipoServicos)
        .then((res) => {
          resolve(res);
        })
        .catch();
    });
  }

  carregarTipoServicos(): Promise<TipoServico[]> {
    return new Promise((resolve, reject) => {
      resolve(this.tipoServicos.slice())
    });
  }

  atualizarTipoServico(causa: TipoServico) {
    this.storage.get('TipoServicos')
      .then((causas: TipoServico[]) => {
        causas.forEach((ts, i) => {
          if (causa.codTipoServico == ts.codTipoServico) {
            this.tipoServicos.splice(i, 1);
            this.tipoServicos.push(causa);
            this.storage.set('TipoServicos', this.tipoServicos)
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