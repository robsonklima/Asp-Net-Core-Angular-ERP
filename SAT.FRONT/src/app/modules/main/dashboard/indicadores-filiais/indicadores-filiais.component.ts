import { Component, OnInit } from '@angular/core';
import Enumerable from 'linq';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardIndicadoresFiliais } from 'app/core/types/dashboard.types';
import * as L from "leaflet";
import { latLng, tileLayer, Map } from 'leaflet';
import 'leaflet.markercluster';
import { HttpClient } from '@angular/common/http';
import { FilialService } from 'app/core/services/filial.service';

@Component({
  selector: 'app-indicadores-filiais',
  templateUrl: './indicadores-filiais.component.html',
  styles: [`tr:nth-child(odd) { background-color: rgb(239,245,254); }`]
})

export class IndicadoresFiliaisComponent implements OnInit {
  public indicadoresFiliais: ViewDashboardIndicadoresFiliais[] = [];
  public indicadoresFiliaisTotal: ViewDashboardIndicadoresFiliais;
  private map: Map;
  public markerClusterGroup: L.MarkerClusterGroup;
  public markerClusterData = [];
  public loading: boolean = true;

  public options = {
    layers: [
      tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap'
      })
    ],
    zoom: 4,
    center: latLng([-15.7801, -47.9292])
  };

  constructor(
    private _dashboardService: DashboardService,
    private _filialService: FilialService,
    private _http: HttpClient
  ) { }

  ngOnInit(): void {
    this.loading = true;
    this.montaDashboard();
  }

  onMapReady(map: Map): void {
    this.map = map;
    this.markerClusterGroup = L.markerClusterGroup({ removeOutsideVisibleBounds: true });

    this._http.get('assets/geojson/uf.json').subscribe((json: any) => {
      L.geoJSON(json, {
        style: {
          weight: 1,
          color: '#254441',
          fillColor: '#43AA8B'
        }
      }).addTo(map);
    });
  }

  private async montaDashboard(): Promise<void> {
    this.indicadoresFiliais = (await this._dashboardService
      .obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.INDICADORES_FILIAL }).toPromise())
      .viewDashboardIndicadoresFiliais;
    this.indicadoresFiliaisTotal = Enumerable.from(this.indicadoresFiliais).firstOrDefault(q => q.filial == "TOTAL");
    this.indicadoresFiliais = Enumerable.from(this.indicadoresFiliais).where(f => f.filial != "TOTAL").distinct().toArray();
    this.indicadoresFiliais.sort((a, b) => (a.sla > b.sla ? -1 : 1));

    let filiaisData = await this._filialService.obterPorParametros({ indAtivo: 1 }).toPromise();
    const filiais = filiaisData.items.filter((f) => f.codFilial != 7 && f.codFilial != 21 && f.codFilial != 33);

    filiais.forEach(async (filial) => {
      const mark = {
        lat: +filial.cidade.latitude,
        lng: +filial.cidade.longitude,
        toolTip: filial.nomeFilial,
        count: 1
      };

      const valorIndicador = this.indicadoresFiliais?.find(f => f.filial == filial.nomeFilial)?.sla || 0;
      var icon = new L.Icon({
        iconUrl: this.obterIconeUrl(valorIndicador),
        iconSize: [32, 32],
        iconAnchor: [15, 32],
        popupAnchor: [1, -32]
      });

      const marker = new L.Marker([+filial.cidade.latitude, +filial.cidade.longitude], { icon: icon }).bindPopup(filial.nomeFilial);
      marker.addTo(this.map);
      this.map.invalidateSize();
    });
    

    this.loading = false;
  }

  private obterIconeUrl(valor: number): string {
    if (valor >= 95)
      return 'assets/icons/marker-green-32.svg';
    else if (valor >= 92)
      return 'assets/icons/marker-yellow-32.svg';
    else 
      return 'assets/icons/marker-red-32.svg';
  }
}