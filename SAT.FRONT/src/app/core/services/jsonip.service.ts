import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class JsonIPService {
  constructor(private http: HttpClient) {}

  obterIP(): Observable<any> {
    const url = `https://jsonip.com`;
    return this.http.get<any>(url).pipe(
      map((obj) => obj)
    );
  }

  obterIPV4(): Observable<any> {
    const url = `https://ipv4.jsonip.com`;
    return this.http.get<any>(url).pipe(
      map((obj) => obj)
    );
  }
}
