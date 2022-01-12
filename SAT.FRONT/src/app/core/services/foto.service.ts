import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Foto, FotoData, FotoParameters } from '../types/foto.types';
import { ImagemPerfilModel } from 'app/modules/main/dialog/dialog-alterar-foto-perfil/dialog-alterar-foto-perfil.component';

@Injectable({
  providedIn: 'root'
})
export class FotoService {
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: FotoParameters): Observable<FotoData> {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key => {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Foto`, { params: params }).pipe(
      map((data: FotoData) => data)
    )
  }

  buscarFotoUsuario(codUsuario: string): Observable<ImagemPerfilModel> {
    const url = `${c.api}/Foto/BuscaFotoUsuario/${codUsuario}`;
    return this.http.get<ImagemPerfilModel>(url).pipe(
      map((obj) => obj)
    );
  }

  obterPorCodigo(codFoto: number): Observable<Foto> {
    const url = `${c.api}/Foto/${codFoto}`;
    return this.http.get<Foto>(url).pipe(
      map((obj) => obj)
    );
  }

  criar(foto: Foto): Observable<Foto> {
    return this.http.post<Foto>(`${c.api}/Foto`, foto).pipe(
      map((obj) => obj)
    );
  }

  deletar(codFoto: number): Observable<Foto> {
    const url = `${c.api}/Foto/${codFoto}`;

    return this.http.delete<Foto>(url).pipe(
      map((obj) => obj)
    );
  }

  alterarFotoPerfil(imagemModel: ImagemPerfilModel): Observable<ImagemPerfilModel> {
    return this.http.post<ImagemPerfilModel>(`${c.api}/Foto/AlterarFotoPerfil/`, imagemModel).pipe(
      map((obj) => obj)
    );
  }

}
