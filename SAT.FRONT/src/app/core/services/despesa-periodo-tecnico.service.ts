import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { DespesaPeriodoTecnico, DespesaPeriodoTecnicoAtendimentoData, DespesaPeriodoTecnicoData, DespesaPeriodoTecnicoParameters } from '../types/despesa-periodo.types';
import { RoleEnum, UserSession } from '../user/user.types';
import { UserService } from '../user/user.service';

@Injectable({
    providedIn: 'root'
})
export class DespesaPeriodoTecnicoService
{
    userSession: UserSession;

    constructor (private http: HttpClient, private _userService: UserService) 
    {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    obterAtendimentos(parameters: DespesaPeriodoTecnicoParameters): Observable<DespesaPeriodoTecnicoAtendimentoData>
    {
        if (this.userSession.usuario.codPerfil == RoleEnum.FILIAL_TECNICO_DE_CAMPO)
            parameters.codTecnico = this.userSession.usuario.codTecnico;

        let params = new HttpParams();
        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null)
                params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaPeriodoTecnico/Atendimentos`, { params: params })
            .pipe(map((data: DespesaPeriodoTecnicoAtendimentoData) => data));
    }

    obterClassificacaoCreditoTecnico(despesaPeriodoTecnico: DespesaPeriodoTecnico): Observable<DespesaPeriodoTecnico>
    {
        return this.http.post<DespesaPeriodoTecnico>(
            `${c.api}/DespesaPeriodoTecnico/ClassificacaoCreditoTecnico`, despesaPeriodoTecnico)
            .pipe(map((obj) => obj));
    }

    obterPorParametros(parameters: DespesaPeriodoTecnicoParameters): Observable<DespesaPeriodoTecnicoData>
    {
        let params = new HttpParams();

        Object.keys(parameters).forEach(key =>
        {
            if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this.http.get(
            `${c.api}/DespesaPeriodoTecnico`, { params: params })
            .pipe(map((data: DespesaPeriodoTecnicoData) => data));
    }

    obterPorCodigo(codDespesaPeriodoTecnico: number): Observable<DespesaPeriodoTecnico>
    {
        return this.http.get<DespesaPeriodoTecnico>(
            `${c.api}/DespesaPeriodoTecnico/${codDespesaPeriodoTecnico}`)
            .pipe(map((obj) => obj));
    }

    criar(despesaPeriodoTecnico: DespesaPeriodoTecnico): Observable<DespesaPeriodoTecnico>
    {
        return this.http.post<DespesaPeriodoTecnico>(
            `${c.api}/DespesaPeriodoTecnico`, despesaPeriodoTecnico)
            .pipe(map((obj) => obj));
    }

    atualizar(despesaPeriodoTecnico: DespesaPeriodoTecnico): Observable<DespesaPeriodoTecnico>
    {
        return this.http.put<DespesaPeriodoTecnico>(
            `${c.api}/DespesaPeriodoTecnico`, despesaPeriodoTecnico)
            .pipe(map((obj) => obj));
    }

    deletar(codDespesaPeriodoTecnico: number): Observable<DespesaPeriodoTecnico>
    {
        return this.http.delete<DespesaPeriodoTecnico>(
            `${c.api}/DespesaPeriodoTecnico/${codDespesaPeriodoTecnico}`)
            .pipe(map((obj) => obj));
    }
}