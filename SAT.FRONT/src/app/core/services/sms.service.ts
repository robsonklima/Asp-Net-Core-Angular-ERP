import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Sms } from '../types/sms.types';

@Injectable({
    providedIn: 'root'
})
export class SmsService
{
    constructor (private http: HttpClient) { }

    enviarSms(sms: Sms): Observable<Sms> {
        const url = `${c.api}/Sms`;
        return this.http.post<Sms>(url, sms).pipe(
            map((obj) => obj)
        );
    }
}