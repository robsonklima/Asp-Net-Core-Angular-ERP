import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Importacao } from '../types/importacao.types';

@Injectable({
	providedIn: 'root'
})
export class ImportacaoService {
	constructor(
		private http: HttpClient
	) { }

	importar(importacao: Importacao): Observable<Importacao> {
		return this.http.post<Importacao>(`${c.api}/Importacao`, importacao).pipe(
			map((obj) => obj)
		);
	}
}
