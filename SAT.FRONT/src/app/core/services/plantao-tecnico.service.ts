import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { PlantaoTecnico, PlantaoTecnicoData, PlantaoTecnicoParameters } from '../types/plantao-tecnico.types';

@Injectable({
  providedIn: 'root'
})
export class PlantaoTecnicoService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: PlantaoTecnicoParameters): Observable<PlantaoTecnicoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PlantaoTecnico`, { params: params }).pipe(
      map((data: PlantaoTecnicoData) => data)
    )
  }

  obterPorCodigo(codPlantaoTecnico: number): Observable<PlantaoTecnico> {
    const url = `${c.api}/PlantaoTecnico/${codPlantaoTecnico}`;
    return this.http.get<PlantaoTecnico>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(plantao: PlantaoTecnico): Observable<PlantaoTecnico> {
    return this.http.post<PlantaoTecnico>(`${c.api}/PlantaoTecnico`, plantao).pipe(
      map((obj) => obj)
    );
  }

  atualizar(plantao: PlantaoTecnico): Observable<PlantaoTecnico> {
    const url = `${c.api}/PlantaoTecnico`;
    return this.http.put<PlantaoTecnico>(url, plantao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPlantaoTecnico: number): Observable<PlantaoTecnico> {
    const url = `${c.api}/PlantaoTecnico/${codPlantaoTecnico}`;

    return this.http.delete<PlantaoTecnico>(url).pipe(
      map((obj) => obj)
    );
  }

  async obterPerfis(filtro: string = ''): Promise<PlantaoTecnico[]> {

    const params: PlantaoTecnicoParameters = {
      sortActive: 'nomePlantaoTecnico',
      sortDirection: 'asc',
      pageSize: 1000,
      filter: filtro
    }
    return (await this.obterPorParametros(params).toPromise()).items;
  }
}