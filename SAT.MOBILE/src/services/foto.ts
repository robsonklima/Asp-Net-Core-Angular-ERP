import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from "rxjs/Observable";

import { NgxImageCompressService } from 'ngx-image-compress';

import 'rxjs/Rx';
import 'rxjs/add/operator/retry';
import 'rxjs/add/operator/timeout';
import 'rxjs/add/operator/delay';
import 'rxjs/add/operator/map';

import { Config } from '../models/config';

import { Foto } from '../models/foto';

@Injectable()
export class FotoService {
  foto: Foto;

  constructor(
    private http: Http,
    private imageCompress: NgxImageCompressService
  ) { }
  
  enviarFotoApi(foto: Foto): Observable<any> {
    return this.http.post(Config.API_URL + 'RatImagemUpload', foto)
      .delay(Math.floor(Math.random() * (800 - 250 + 1) + 250))
      .timeout(120000)
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json()));
  }

  comprimirFoto(foto: Foto): Promise<Foto> {
    return new Promise((resolve, reject) => {
      this.imageCompress.compressFile(foto.str, '', 50, 50).then(res => {
        foto.str = res;
        resolve(foto);
      }).catch(() => {
        reject();
      });
    });
  }
}