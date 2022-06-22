import { FileMime } from '../types/file.types';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { appConfig as c } from 'app/core/config/app.config';
import moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class ExportacaoService {

  constructor(
    private http: HttpClient,
    ) { }

    private _params: HttpParams;
    
    async exportar(exportacaoTipo: string, tipoArquivo: FileMime, params: any = null){
      this._params = params;
      const data = await this.downloadExportacao(exportacaoTipo);
      const blob = new Blob([data], {type: tipoArquivo});

	  let excel = document.createElement("a");
	  excel.href = window.URL.createObjectURL(blob).toString();
	  excel.download = `${exportacaoTipo}_${moment().format('DD-MM-yyyy_HH-mm-ss')}.xlsx`;
	  excel.click()
    }

    downloadExportacao(exportacaoTipo : string): Promise<any> {
      let params = new HttpParams();

      Object.keys(this._params).forEach(key => {
        if (this._params[key] !== undefined && this._params[key] !== null) params = params.append(key, String(this._params[key]));
      });

      const requestOptions: Object = {
        responseType: 'blob',
        params: params 
      }

      return this.http.get<any>(`${c.api}/Exportacao/${exportacaoTipo}` ,requestOptions).toPromise()
    }
}
