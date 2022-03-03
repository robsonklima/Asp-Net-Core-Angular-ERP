import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FilialService } from 'app/core/services/filial.service';
import { Filial, FilialData } from 'app/core/types/filial.types';
import { SharedService } from 'app/shared.service';
import * as L from "leaflet";
import 'leaflet.markercluster';
import { latLng, tileLayer, Map } from 'leaflet';
import { HttpClient } from '@angular/common/http';
import { UserService } from 'app/core/user/user.service';
import { GeolocalizacaoService } from 'app/core/services/geolocalizacao.service';
import { GeolocalizacaoServiceEnum } from 'app/core/types/geolocalizacao.types';
import Enumerable from 'linq';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum } from 'app/core/types/dashboard.types';

@Component({
  selector: 'app-indicadores-filiais-mapa',
  templateUrl: './indicadores-filiais-mapa.component.html',
  styleUrls: ['./indicadores-filiais-mapa.component.css'
  ]
})

export class IndicadoresFiliaisMapaComponent implements OnInit {
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
    private _geolocacationService: GeolocalizacaoService,
    private _http: HttpClient) { }

  ngOnInit(): void {
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
        },
        onEachFeature: (feature, layer) => {
          this.colorlayer(feature, layer, this._sharedService);
        }

      }).addTo(map);
    });
  }

  private colorlayer(feature, layer, _sharedService: SharedService): void {
    layer.on('mouseover', function () {
      layer.setStyle({
        color: "blue",
        fillColor: 'blue',
        weight: 1
      });
      _sharedService.sendClickEvent(IndicadoresFiliaisMapaComponent, [{ estado: feature.properties.UF_05, seleciona: true }]);
    });
    layer.on('mouseout', function () {
      layer.setStyle({
        weight: 1,
        color: '#254441',
        fillColor: '#43AA8B'
      });
      _sharedService.sendClickEvent(IndicadoresFiliaisMapaComponent, [{ estado: null, seleciona: false }]);
    });
  }

  private async obterDados() {

    // Filiais
    this._filialService.obterPorParametros({ indAtivo: 1 }).subscribe((data: FilialData) => {
      this.filiais.push(...data.items.filter((f) => f.codFilial != 7 && f.codFilial != 21 && f.codFilial != 33)); // Remover EXP,OUT,IND
    });

    let indicadoresFiliais = Enumerable.from((await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.INDICADORES_FILIAL }).toPromise())
      .viewDashboardIndicadoresFiliais).where(f => f.filial != "TOTAL").toArray();

    let markers: any[] = [];

    this.filiais.forEach(async (filial) => {

      // Google
      // Tenta pelo cep (nem sempre os endereços são corretos)
      let mapService = (await this._geolocacationService.obterPorParametros
        ({ enderecoCep: filial.cep, geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.GOOGLE }).toPromise());

      // Se não encontra pelo cep, tenta pelo endereço
      if (!mapService) {
        let endereco = filial.endereco + " " + filial.bairro + " " + filial.cidade.nomeCidade;
        mapService = (await this._geolocacationService.obterPorParametros
          ({ enderecoCep: filial.cep, geolocalizacaoServiceEnum: GeolocalizacaoServiceEnum.GOOGLE }).toPromise());
      }

      if (mapService) {
        let mark = {
          lat: +mapService.latitude,
          lng: +mapService.longitude,
          toolTip: filial.nomeFilial,
          count: 1
        };

        let valorIndicador = indicadoresFiliais?.find(f => f.filial == filial.nomeFilial)?.sla || 0;

        var icon = new L.Icon({
          iconUrl:
            valorIndicador >= 95 ?
              'assets/icons/marker-green-32.svg' :
              valorIndicador >= 92 ?
                'assets/icons/marker-yellow-32.svg' :
                'assets/icons/marker-red-32.svg'
          ,
          iconSize: [32, 32],
          iconAnchor: [15, 32],
          popupAnchor: [1, -32]
        });

        let marker = new L.Marker([+mapService.latitude.replace(',', '.'), +mapService.longitude.replace(',', '.')],
          { icon: icon }).bindPopup(filial.nomeFilial);

        marker.addTo(this.map);
        markers.push(mark);
        this.map.fitBounds(markers);
        this.map.invalidateSize();
      }
    });
    this.loading = false;
  }
}