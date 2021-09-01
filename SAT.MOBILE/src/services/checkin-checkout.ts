import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { Checkin } from '../models/checkin';

@Injectable()
export class CheckinCheckoutService {
  constructor(
    private http: Http
  ) { }

  buscarCheckinApi(codOs: number): Observable<any> {
    return this.http.get(Config.API_URL + 'Checkin/' + codOs)
    .map((res: Response) => {
      return res.json()
    })
    .catch((error: Error) => {
      return Observable.throw(error);
    });
  }

  enviarCheckinApi(checkin: Checkin): Observable<any> {
    return this.http.post(Config.API_URL + 'Checkin', checkin)
    .map((res: Response) => {
      return res.json()
    })
    .catch((error: Error) => {
      return Observable.throw(error);
    });
  }

  buscarCheckoutApi(codOs: number): Observable<any> {
    return this.http.get(Config.API_URL + 'Checkout/' + codOs)
    .map((res: Response) => {
      return res.json()
    })
    .catch((error: Error) => {
      return Observable.throw(error);
    });
  }
}