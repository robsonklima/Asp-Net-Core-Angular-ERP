import { AfterViewInit, Component } from '@angular/core';
import { FilialService } from 'app/core/services/filial.service';
import { Filial, FilialData } from 'app/core/types/filial.types';
import { SharedService } from 'app/shared.service';
import * as L from "leaflet";
import 'leaflet.markercluster';
import { latLng, tileLayer, Map } from 'leaflet';
import { HttpClient } from '@angular/common/http';
import Enumerable from 'linq';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum } from 'app/core/types/dashboard.types';

@Component({
  selector: 'app-indicadores-filiais-mapa',
  templateUrl: './indicadores-filiais-mapa.component.html',
  styles: [`
    #landmarks-brazil path { fill: #b28afc; }
    #landmarks-brazil path:hover { fill: #7f419d }
    #landmarks-brazil { width: 100%; margin-top: -20px; }
    #landmarks-brazil .spArea:hover~.spArea, .spArea:hover { fill: #7f419d; }
    #landmarks-brazil .noHover { pointer-events: none; }
  `]
})

export class IndicadoresFiliaisMapaComponent implements AfterViewInit {
  private map: Map;
  private filiais: Filial[] = [];

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
    private _sharedService: SharedService,
    private _filialService: FilialService,
    private _dashboardService: DashboardService,
    private _http: HttpClient) { }

  ngAfterViewInit(): void
  {
    this.obterDados();
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

  private async obterDados() {

    // Filiais
    this._filialService.obterPorParametros({ indAtivo: 1 }).subscribe((data: FilialData) => {
      this.filiais.push(...data.items.filter((f) => f.codFilial != 7 && f.codFilial != 21 && f.codFilial != 33)); // Remover EXP,OUT,IND
    });

    let indicadoresFiliais = Enumerable.from((await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.INDICADORES_FILIAL }).toPromise())
      .viewDashboardIndicadoresFiliais).where(f => f.filial != "TOTAL").toArray();

    this.filiais.forEach(async (filial) => {
      const mark = {
        lat: +filial.cidade.latitude,
        lng: +filial.cidade.longitude,
        toolTip: filial.nomeFilial,
        count: 1
      };

      const valorIndicador = indicadoresFiliais?.find(f => f.filial == filial.nomeFilial)?.sla || 0;
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