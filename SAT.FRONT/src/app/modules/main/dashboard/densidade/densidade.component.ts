import { Component, OnInit } from '@angular/core';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import { LocalAtendimentoService } from 'app/core/services/local-atendimento.service';
import * as L from "leaflet";
import { latLng, tileLayer, Map, MarkerClusterGroup } from 'leaflet';

import 'leaflet.heat/dist/leaflet-heat.js'
import { TecnicoService } from 'app/core/services/tecnico.service';

@Component({
  selector: 'app-densidade',
  templateUrl: './densidade.component.html'
})
export class DensidadeComponent implements OnInit {
  map: Map;
  markerClusterGroup: MarkerClusterGroup;
  markerClusterData = [];
  
  options = {
    layers: [
      tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap'
      })
    ],
    zoom: 8,
    center: latLng([-15.7801, -47.9292])
  };

  constructor(
    private _autorizadaSvc: AutorizadaService,
    private _localAtendimentoSvc: LocalAtendimentoService,
    private _tecnicoSvc: TecnicoService
  ) { }

  async ngOnInit() {
    
  }

  onMapReady(map: Map): void {
    this.map = map;

    this.obterAutorizadas();
    this.obterTecnicos();
    this.obterLocaisAtendimento();
  }

  private async obterAutorizadas() {
    const data = await this._autorizadaSvc.obterPorParametros({ 
      codFilial: 4,
      indAtivo: 1
    }).toPromise();

    let markers: any[] = data.items.filter(a => this.isFloat(+a.latitude) && this.isFloat(+a.longitude)).map((autorizada) => {
      return {
        lat: +autorizada.latitude,
        lng: +autorizada.longitude,
        toolTip: autorizada.nomeFantasia,
        count: 1
      }
    });
    
    this.addMarkersOnMap(markers, 'blue');
  }

  private async obterTecnicos() {
    const data = await this._tecnicoSvc.obterPorParametros({ 
      codFilial: 4,
      indAtivo: 1
    }).toPromise();

    let markers: any[] = data.items.filter(t => this.isFloat(+t.latitude) && this.isFloat(+t.longitude)).map((tecnico) => {
      return {
        lat: +tecnico.latitude,
        lng: +tecnico.longitude,
        toolTip: tecnico.nome
      }
    });
    
    this.addMarkersOnMap(markers, 'green');
  }

  private async obterLocaisAtendimento() {
    const data = await this._localAtendimentoSvc.obterPorParametros({indAtivo: 1, codFilial: 4}).toPromise();

    let markers: any[] = data.items.filter(l => this.isFloat(+l.latitude) && this.isFloat(+l.longitude)).map((local) => {
      return {
        lat: +local.latitude,
        lng: +local.longitude,
        toolTip: local.nomeLocal
      }
    });

    this.addLayer(markers);
  }

  private addMarkersOnMap(markers: any[], color:string='red'): void {
    var greenIcon = new L.Icon({
      iconUrl: `https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-${color}.png`,
      shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
      iconSize: [25, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      shadowSize: [41, 41]
    });

    markers.forEach((m, i) => {
      let marker = new L.Marker([ +m.lat, +m.lng ], {icon : greenIcon}).bindPopup(m.toolTip);
      marker.addTo(this.map);
    });

    this.map.fitBounds(markers);
    this.map.invalidateSize();
  }

  private addHeatMap(markers: any[]): void {
    var cfg = {
      radius: 10,
      maxOpacity: .5,
      minOpacity: 0,
      blur: .9,
      gradient: {
        // enter n keys between 0 and 1 here
        // for gradient color customization
        '.5': 'blue',
        '.8': 'red',
        '.95': 'white'
      }
    };

    let newAddressPoints = markers.map(function (m) { return [m.lat, m.lng]; });
    const heat = (L as any).heatLayer(newAddressPoints).addTo(this.map);

  }

  private addLayer(markers: any[]): void {
    var icon = new L.Icon({
      iconUrl: `https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png`,
      shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
      iconSize: [25, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      shadowSize: [41, 41]
    });

    console.log(markers);
    

    markers.forEach((m, i) => {
      // let marker = new L.Marker([ +m.lat, +m.lng ], {icon : greenIcon}).bindPopup(m.toolTip);
      // marker.addTo(this.map);
      
      let layer = L.marker(L.latLng([m.lat, m.lng])).bindPopup(m.toolTip).setIcon(icon);
      this.markerClusterGroup.addLayer(layer).addTo(this.map);
    });

    
  }

  private isFloat(n) {
    return n === +n && n !== (n|0);
  }
}
