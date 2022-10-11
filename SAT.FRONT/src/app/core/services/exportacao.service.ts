import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from './../types/exportacao.types';
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
	private _objParams: any;

	async exportar(mimeArquivo: FileMime, params: Exportacao = null) {
		this._params = params.entityParameters;
		this._objParams = params;
		const data = await this.downloadExportacao();
		if (!data) return;

		const blob = new Blob([data], { type: mimeArquivo });

		let exportacao = document.createElement("a");
		exportacao.href = window.URL.createObjectURL(blob).toString();
		exportacao.download = `${Object.keys(ExportacaoTipoEnum)[Object.values(ExportacaoTipoEnum).indexOf(params.tipoArquivo as unknown as ExportacaoTipoEnum)]}_${moment().format('DD-MM-yyyy_HH-mm-ss')}`;
		exportacao.click();
	}

	private downloadExportacao(): Promise<any> {
		debugger
		let params = new HttpParams();

		Object.keys(this._params).forEach(key => {
			if (this._params[key] !== undefined && this._params[key] !== null) params = params.append(key, String(this._params[key]));
		});

		const requestOptions: Object = {
			responseType: 'blob',
		}

		return this.http.post<any>(`${c.api}/Exportacao`, this._objParams, requestOptions).toPromise()
	}
}
