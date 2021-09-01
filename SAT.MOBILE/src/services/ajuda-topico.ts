import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/Rx';

import { Config } from '../models/config';

import { Observable } from "rxjs/Observable";
import { AjudaTopico } from '../models/ajuda-topico';

@Injectable()
export class AjudaTopicoService {
  constructor(
    private http: Http
  ) { }

  buscarTopicosApi(): Observable<AjudaTopico[]> {
    return this.http.get(Config.API_URL + 'AjudaTopico')
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json()));
  }
}