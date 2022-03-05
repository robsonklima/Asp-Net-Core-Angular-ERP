import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { PlantaoTecnicoRegiao, PlantaoTecnicoRegiaoData, PlantaoTecnicoRegiaoParameters } from '../types/plantao-tecnico.types';

@Injectable({
  providedIn: 'root'
})
export class PlantaoTecnicoRegiaoService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: PlantaoTecnicoRegiaoParameters): Observable<PlantaoTecnicoRegiaoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PlantaoTecnicoRegiao`, { params: params }).pipe(
      map((data: PlantaoTecnicoRegiaoData) => data)
    )
  }

  obterPorCodigo(codPlantaoTecnicoRegiao: number): Observable<PlantaoTecnicoRegiao> {
    const url = `${c.api}/PlantaoTecnicoRegiao/${codPlantaoTecnicoRegiao}`;
    return this.http.get<PlantaoTecnicoRegiao>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(plantao: PlantaoTecnicoRegiao): Observable<PlantaoTecnicoRegiao> {
    return this.http.post<PlantaoTecnicoRegiao>(`${c.api}/PlantaoTecnicoRegiao`, plantao).pipe(
      map((obj) => obj)
    );
  }

  atualizar(plantao: PlantaoTecnicoRegiao): Observable<PlantaoTecnicoRegiao> {
    const url = `${c.api}/PlantaoTecnicoRegiao`;
    return this.http.put<PlantaoTecnicoRegiao>(url, plantao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPlantaoTecnicoRegiao: number): Observable<PlantaoTecnicoRegiao> {
    const url = `${c.api}/PlantaoTecnicoRegiao/${codPlantaoTecnicoRegiao}`;

    return this.http.delete<PlantaoTecnicoRegiao>(url).pipe(
      map((obj) => obj)
    );
  }

  async obterPerfis(filtro: string = ''): Promise<PlantaoTecnicoRegiao[]> {

    const params: PlantaoTecnicoRegiaoParameters = {
      sortActive: 'nomePlantaoTecnicoRegiao',
      sortDirection: 'asc',
      pageSize: 1000,
      filter: filtro
    }
    return (await this.obterPorParametros(params).toPromise()).items;
  }
}