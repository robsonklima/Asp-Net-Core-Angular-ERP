import { Injectable } from '@angular/core';
import { HttpClient,  } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config';
import { IISLog } from '../types/iislog.types';

@Injectable({
  providedIn: 'root'
})
export class IISLogService {
  constructor(private http: HttpClient) {}

  obter(): Observable<IISLog[]> {
    return this.http.get(`${c.api}/IISLog`).pipe(
      map((data: IISLog[]) => data)
    )
  }
}
