import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { PlantaoTecnicoCliente, PlantaoTecnicoClienteData, PlantaoTecnicoClienteParameters } from '../types/plantao-tecnico.types';

@Injectable({
  providedIn: 'root'
})
export class PlantaoTecnicoClienteService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: PlantaoTecnicoClienteParameters): Observable<PlantaoTecnicoClienteData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/PlantaoTecnicoCliente`, { params: params }).pipe(
      map((data: PlantaoTecnicoClienteData) => data)
    )
  }

  obterPorCodigo(codPlantaoTecnicoCliente: number): Observable<PlantaoTecnicoCliente> {
    const url = `${c.api}/PlantaoTecnicoCliente/${codPlantaoTecnicoCliente}`;
    return this.http.get<PlantaoTecnicoCliente>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(plantao: PlantaoTecnicoCliente): Observable<PlantaoTecnicoCliente> {
    return this.http.post<PlantaoTecnicoCliente>(`${c.api}/PlantaoTecnicoCliente`, plantao).pipe(
      map((obj) => obj)
    );
  }

  atualizar(plantao: PlantaoTecnicoCliente): Observable<PlantaoTecnicoCliente> {
    const url = `${c.api}/PlantaoTecnicoCliente`;
    return this.http.put<PlantaoTecnicoCliente>(url, plantao).pipe(
      map((obj) => obj)
    );
  }

  deletar(codPlantaoTecnicoCliente: number): Observable<PlantaoTecnicoCliente> {
    const url = `${c.api}/PlantaoTecnicoCliente/${codPlantaoTecnicoCliente}`;

    return this.http.delete<PlantaoTecnicoCliente>(url).pipe(
      map((obj) => obj)
    );
  }

  async obterPerfis(filtro: string = ''): Promise<PlantaoTecnicoCliente[]> {

    const params: PlantaoTecnicoClienteParameters = {
      sortActive: 'nomePlantaoTecnicoCliente',
      sortDirection: 'asc',
      pageSize: 1000,
      filter: filtro
    }
    return (await this.obterPorParametros(params).toPromise()).items;
  }
}