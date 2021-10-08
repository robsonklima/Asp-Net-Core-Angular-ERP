import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { appConfig as c } from 'app/core/config/app.config'
import { AgendaTecnico, AgendaTecnicoParameters } from 'app/core/types/agenda-tecnico.types';

@Injectable({
    providedIn: 'root'
})
export class AgendaTecnicoService
{
    constructor(
        private _httpClient: HttpClient
    ) {}

    obterAgendaTecnico(parameters: AgendaTecnicoParameters): Observable<AgendaTecnico[]> {
        let params = new HttpParams();
        
        Object.keys(parameters).forEach(key => {
          if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
        });

        return this._httpClient.get<AgendaTecnico[]>(`${c.api}/AgendaTecnico`, { params: params }).pipe(map((obj) => obj));
    }

    atualizarAgendaTecnico(agendaTecnico: AgendaTecnico): Observable<AgendaTecnico>
    {
       return null;
    }
    
    criarAgendaTecnico(event): Observable<AgendaTecnico>
    {
        return this._httpClient.post<AgendaTecnico>(`${c.api}/AgendaTecnico`, event).pipe(
            tap((data) => 
            {

            })
        );
    }
    
    deletarAgendaTecnico(id: string): Observable<AgendaTecnico>
    {
        return this._httpClient.delete<AgendaTecnico>(`${c.api}/AgendaTecnico/Evento/${id}`).pipe(
            tap((data) => 
            {
                
            })
        );
    }
}
