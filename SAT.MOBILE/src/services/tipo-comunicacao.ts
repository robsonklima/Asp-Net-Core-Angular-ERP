import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { TipoComunicacao } from '../models/tipo-comunicacao';


@Injectable()
export class TipoComunicacaoService {
  private tipos: TipoComunicacao[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarTiposComunicacaoApi(): Observable<TipoComunicacao[]> {
    return this.http.get(Config.API_URL + 'TipoComunicacao')
      .map((res: Response) => { this.storage.set('TiposComunicacao', res.json()).catch() })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarTiposComunicacaoStorage(): Promise<TipoComunicacao[]> {
    return new Promise((resolve, reject) => {
      this.storage.get('TiposComunicacao').then((tipos: TipoComunicacao[]) => {
        this.tipos = tipos != null ? tipos : [];
        resolve(this.tipos.slice());
      }).catch(() => {
        reject();
      });
    });
  }
}