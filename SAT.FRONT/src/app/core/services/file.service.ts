import { FileMime } from './../types/file.types';
import { HttpClient } from '@angular/common/http';
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

    requestOptions: Object = {
      responseType: 'blob'
    }
    
    async downloadLink(rota: string, tipoArquivo: FileMime){
      this.objClass = rota;
      const data = await this.exportarExcel();
      const blob = new Blob([data], {type: tipoArquivo}, );
      return window.URL.createObjectURL(blob).toString();
    }

    exportarExcel(): Promise<any> {
      return this.http.get<any>(`${c.api}/${this.objClass}/export`, this.requestOptions).toPromise()
    }
}
