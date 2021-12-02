import { AfterViewInit, Component, Input } from '@angular/core';
import { FilialService } from 'app/core/services/filial.service';
import { IndicadorService } from 'app/core/services/indicador.service';
import { Filial, FilialData } from 'app/core/types/filial.types';
import { IndicadorAgrupadorEnum, IndicadorParameters, IndicadorTipoEnum } from 'app/core/types/indicador.types';
import moment from 'moment';
import { SharedService } from 'app/shared.service';
import * as L from "leaflet";
import 'leaflet.markercluster';
import { latLng, tileLayer, Map } from 'leaflet';
import { HttpClient } from '@angular/common/http';
import { GoogleGeolocationService } from 'app/core/services/google-geolocation.service';
import { NominatimService } from 'app/core/services/nominatim.service';
import { OrdemServicoFilterEnum, OrdemServicoIncludeEnum } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import { Filterable } from 'app/core/filters/filterable';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-mapa',
  templateUrl: './mapa.component.html',
  styleUrls: ['./mapa.component.css'
  ]
})

export class MapaComponent extends Filterable implements AfterViewInit {
  private map: Map;
  private filiais: Filial[] = [];

  public markerClusterGroup: L.MarkerClusterGroup;
  public markerClusterData = [];
  public loading: boolean = true;

  @Input() sidenav: MatSidenav;

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
    protected _userService: UserService,
    private _sharedService: SharedService,
    private _filialService: FilialService,
    private _indicadorService: IndicadorService,
    private _googleGeolocationService: GoogleGeolocationService,
    private _nominatimService: NominatimService,
    private _http: HttpClient) {
    super(_userService, 'dashboard-filtro')
  }


  ngAfterViewInit(): void {
    this._sharedService.clearListEvents();
    this.obterFiliais();

    this.registerEmitters();
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.filiais = [];
      this.loading = true;
      this.obterFiliais();
    })
  }

  loadFilter(): void {
    super.loadFilter();
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
      _sharedService.sendClickEvent(MapaComponent, [{ estado: feature.properties.UF_05, seleciona: true }]);
    });
    layer.on('mouseout', function () {
      layer.setStyle({
        weight: 1,
        color: '#254441',
        fillColor: '#43AA8B'
      });
      _sharedService.sendClickEvent(MapaComponent, [{ estado: null, seleciona: false }]);
    });
  }

  private async obterFiliais() {

    // Filiais
    this._filialService.obterPorParametros({ indAtivo: 1 }).subscribe((data: FilialData) => {
      this.filiais.push(...data.items.filter((f) => f.codFilial != 7 && f.codFilial != 21 && f.codFilial != 33)); // Remover EXP,OUT,IND
    });

    // Indicadores
    let params: IndicadorParameters =
    {
      tipo: IndicadorTipoEnum.SLA,
      codFiliais: this.filiais.map((f) => f.codFilial).join(','),
      agrupador: IndicadorAgrupadorEnum.FILIAL,
      include: OrdemServicoIncludeEnum.OS_RAT_FILIAL_PRAZOS_ATENDIMENTO,
      filterType: OrdemServicoFilterEnum.FILTER_INDICADOR,
      dataInicio: this.filter?.parametros.dataInicio || moment().startOf('month').format('YYYY-MM-DD hh:mm'),
      dataFim: this.filter?.parametros.dataFim || moment().endOf('month').format('YYYY-MM-DD hh:mm')
    };

    let indicadores = await this._indicadorService.obterPorParametros(params).toPromise();

    let markers: any[] = [];

    this.filiais.forEach(async (filial) => {

      // Google
      // Tenta pelo cep (nem sempre os endereços são corretos)
      let mapService = (await this._googleGeolocationService.obterPorParametros
        ({ enderecoCep: filial.cep }).toPromise()).results.shift();

      // Se não encontra pelo cep, tenta pelo endereço
      if (!mapService) {
        let endereco = filial.endereco + " " + filial.bairro + " " + filial.cidade.nomeCidade;
        mapService = (await this._googleGeolocationService.obterPorParametros
          ({ enderecoCep: endereco }).toPromise()).results.shift();
      }

      // Nominatim
      // // Tenta pelo cep (nem sempre os endereços são corretos)
      // let mapService = (await this._nominatimService.buscarEndereco(filial.cep).toPromise()).results.shift();
      // // Se não encontra pelo cep, tenta pelo endereço
      // if (!mapService) {
      //    let endereco = filial.endereco + " " + filial.bairro + " " + filial.cidade.nomeCidade;
      //   mapService = (await this._nominatimService.buscarEndereco(endereco).toPromise()).results.shift();
      // }

      if (mapService) {

        let mark = {
          lat: +mapService.geometry.location.lat,
          lng: +mapService.geometry.location.lng,
          toolTip: filial.nomeFilial,
          count: 1
        };

        let valorIndicador = indicadores?.find(f => f.label == filial.nomeFilial)?.valor || 0;

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

        let marker = new L.Marker([+mapService.geometry.location.lat, +mapService.geometry.location.lng],
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