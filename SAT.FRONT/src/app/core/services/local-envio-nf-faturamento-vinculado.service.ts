import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { LocalEnvioNFFaturamentoVinculado, LocalEnvioNFFaturamentoVinculadoData, LocalEnvioNFFaturamentoVinculadoParameters } from '../types/local-envio-nf-faturamento-vinculado.types';

@Injectable({
	providedIn: 'root'
})
export class LocalEnvioNFFaturamentoVinculadoService {
	constructor(
		private http: HttpClient
	) { }


	criar(localEnvioNFFaturamentoVinculado: LocalEnvioNFFaturamentoVinculado): Observable<LocalEnvioNFFaturamentoVinculado> {
		return this.http.post<LocalEnvioNFFaturamentoVinculado>(`${c.api}/LocalEnvioNFFaturamentoVinculado`, localEnvioNFFaturamentoVinculado).pipe(
			map((obj) => obj)
		);
	}

	obterPorCodigo(codlocalEnvioNFFaturamentoVinculado: number): Observable<LocalEnvioNFFaturamentoVinculado> {
		const url = `${c.api}/LocalEnvioNFFaturamentoVinculado/${codlocalEnvioNFFaturamentoVinculado}`;
		return this.http.get<LocalEnvioNFFaturamentoVinculado>(url).pipe(
			map((obj) => obj)
		);
	}

	obterPorParametros(parameters: LocalEnvioNFFaturamentoVinculadoParameters): Observable<LocalEnvioNFFaturamentoVinculadoData> {
		let params = new HttpParams();

		Object.keys(parameters).forEach(key => {
			if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
		});

		return this.http.get(`${c.api}/LocalEnvioNFFaturamentoVinculado`, { params: params }).pipe(
			map((data: LocalEnvioNFFaturamentoVinculadoData) => data)
		)
	}

	atualizar(localEnvioNFFaturamentoVinculado: LocalEnvioNFFaturamentoVinculado): Observable<LocalEnvioNFFaturamentoVinculado> {
		const url = `${c.api}/LocalEnvioNFFaturamentoVinculado`;
		return this.http.put<LocalEnvioNFFaturamentoVinculado>(url, localEnvioNFFaturamentoVinculado).pipe(
			map((obj) => obj)
		);
	}

	deletar(codlocalEnvioNFFaturamento: number, codPosto: number, codContrato: number): Observable<LocalEnvioNFFaturamentoVinculado> {
		const url = `${c.api}/LocalEnvioNFFaturamentoVinculado/${codlocalEnvioNFFaturamento}/${codPosto}/${codContrato}`;

		return this.http.delete<LocalEnvioNFFaturamentoVinculado>(url).pipe(
			map((obj) => obj)
		);
	}
}
