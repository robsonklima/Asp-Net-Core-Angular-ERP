import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { StatusServico } from '../models/status-servico';


@Injectable()
export class StatusServicoService {
  private statusServicos: StatusServico[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarStatusServicosApi(): Observable<StatusServico[]> {
    return this.http.get(Config.API_URL + 'StatusServico')
      .map((res: Response) => { this.storage.set('StatusServicos', res.json()).catch() })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarStatusServicosStorage(): Promise<StatusServico[]> {
    return new Promise((resolve, reject) => {
      this.storage.get('StatusServicos').then((s: StatusServico[]) => {
        this.statusServicos = s != null ? s : [];
        resolve(this.statusServicos.slice());
      }).catch(() => {
        reject();
      });
    });
  }
}