import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class NominatimService
{
  constructor (
    private http: HttpClient
  ) { }

  buscarEndereco(end: string): Observable<any> 
  {
    const url = `https://nominatim.openstreetmap.org/search?q=${end}&format=json&addressdetails=1`;

    return this.http.get<any>(url).pipe(
      map((data: any) => data)
    );
  }

  buscarRota(latOrigem: number, lngOrigem: number, latDestino: number, lngDestino: number) 
  {
    

    const head = new HttpHeaders().append('accept', 'application/json') .append('content-type', 'application/json').append('Access-Control-Allow-Origin', '*');
    const url = `https://www.mapquestapi.com/directions/v2/route?key=nCEqh4v9AjSGJreT75AAIaOx5vQZgVQ2&from=${latOrigem},${lngOrigem}&to=${latDestino},${lngDestino}`;

    return this.http.get<any>(url, {headers:head}).pipe(
      map((data: any) => data)
    );
  }
}

// 'Io2YoCuiLJ8SFAW14pXwozOSYgxPAOM1', 'nCEqh4v9AjSGJreT75AAIaOx5vQZgVQ2',
// 'KDVU5s6t3bOZkAksJfpuUiygIFPlXH9U', 'klrano7LC8Vk88QmjXvAt9jUrjzGReiz',
// 'bP1zqnkhSVsAj5gL8GucMipVqDRPNmID', 'A0bAhXKQNNEqjFWUUOvR2HhAStiElB0L',
// 'EDvdlS7xGN5U8WqHFiXMWXmXGwNSAhvh', 'XsjlWnkAo5fPMGhJ8l3RTwEpQfPINIGU',
// 'tbYhdvKIFCxkDFjoGATSHmVPL54ItdlC', 'l19CNtzjRZmwVCncGjBycgFV5WSUGYQ1',
