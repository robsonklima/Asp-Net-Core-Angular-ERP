import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { Email } from '../types/email.types';

@Injectable({
    providedIn: 'root'
})
export class EmailService
{
    constructor (private http: HttpClient) { }

    enviarEmail(email: Email): Observable<Email>
    {
        debugger;
        const url = `${c.api}/Email`;
        return this.http.post<Email>(url, email).pipe(
            map((obj) => obj)
        );
    }
}