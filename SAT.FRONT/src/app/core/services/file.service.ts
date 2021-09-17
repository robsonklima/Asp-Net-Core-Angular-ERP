import { FileMime } from './../types/file.types';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { appConfig as c } from 'app/core/config/app.config';


@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(
    private http: HttpClient,
    ) { }

    private objClass: string
    private _params: HttpParams;
    
    async downloadLink(rota: string, tipoArquivo: FileMime, params: any = null){
      this._params = params;
      this.objClass = rota;
      const data = await this.exportarExcel();
      const blob = new Blob([data], {type: tipoArquivo}, );
      return window.URL.createObjectURL(blob).toString();
    }

    exportarExcel(): Promise<any> {

      let params = new HttpParams();

      Object.keys(this._params).forEach(key => {
        if (this._params[key] !== undefined && this._params[key] !== null) params = params.append(key, String(this._params[key]));
      });

      const requestOptions: Object = {
        responseType: 'blob',
        params: params 
      }
      return this.http.get<any>(`${c.api}/${this.objClass}/export`,requestOptions).toPromise()
    }
}
