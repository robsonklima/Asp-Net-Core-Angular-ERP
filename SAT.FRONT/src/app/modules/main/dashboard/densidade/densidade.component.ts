import { Component, OnInit } from '@angular/core';
import { AutorizadaService } from 'app/core/services/autorizada.service';
import * as L from "leaflet";
import 'leaflet.markercluster';
import { latLng, tileLayer, Map } from 'leaflet';

import 'leaflet.heat/dist/leaflet-heat.js'
import { TecnicoService } from 'app/core/services/tecnico.service';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';

@Component({
  selector: 'app-densidade',
  templateUrl: './densidade.component.html'
})
export class DensidadeComponent implements OnInit {
  map: Map;
  markerClusterGroup: L.MarkerClusterGroup;
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
    private _equipamentoContratoSvc: EquipamentoContratoService,
    private _tecnicoSvc: TecnicoService
  ) { }

  async ngOnInit() {
    
  }

  onMapReady(map: Map): void {
    this.map = map;
    this.markerClusterGroup = L.markerClusterGroup({ removeOutsideVisibleBounds: true });

    this.obterAutorizadas();
    this.obterTecnicos();
    this.obterEquipamentosContrato();
  }

  private async obterAutorizadas() {
    const data = await this._autorizadaSvc.obterPorParametros({ 
      codFilial: 2,
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

    var icon = new L.Icon({
      iconUrl: 'assets/icons/destination-32.png',
      iconSize: [32, 32],
      iconAnchor: [15, 32],
      popupAnchor: [1, -32]
    });
    
    this.addMarkersOnMap(markers, icon);
  }

  private async obterTecnicos() {
    const data = await this._tecnicoSvc.obterPorParametros({ 
      codFilial: 2,
      indAtivo: 1
    }).toPromise();

    let markers: any[] = data.items.filter(t => this.isFloat(+t.latitude) && this.isFloat(+t.longitude)).map((tecnico) => {
      return {
        lat: +tecnico.latitude,
        lng: +tecnico.longitude,
        toolTip: tecnico.nome
      }
    });

    var icon = new L.Icon({
      iconUrl: 'assets/icons/home-32.png',
      iconSize: [32, 32],
      iconAnchor: [15, 32],
      popupAnchor: [1, -32]
    });
    
    this.addMarkersOnMap(markers, icon);
  }

  private async obterEquipamentosContrato() {
    const data = await this._equipamentoContratoSvc.obterPorParametros({ 
      codFilial: 2,
      indAtivo: 1
    }).toPromise();

    let markers: any[] = data.items.filter(e => this.isFloat(+e.localAtendimento.latitude) && this.isFloat(+e.localAtendimento.longitude)).map((equip) => {
      return {
        lat: +equip.localAtendimento.latitude,
        lng: +equip.localAtendimento.longitude,
        toolTip: equip.numSerie
      }
    });

    var icon = new L.Icon({
      iconUrl: 'assets/icons/bank-64.png',
      iconSize: [32, 32],
      iconAnchor: [15, 32],
      popupAnchor: [1, -32]
    });
    
    this.addLayer(markers, icon);
  }

  private addMarkersOnMap(markers: any[], icon: L.Icon): void {
    markers.forEach((m, i) => {
      let marker = new L.Marker([ +m.lat, +m.lng ], {icon : icon}).bindPopup(m.toolTip);
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
        '.5': 'blue',
        '.8': 'red',
        '.95': 'white'
      }
    };

    let newAddressPoints = markers.map(function (m) { return [m.lat, m.lng]; });
    const heat = (L as any).heatLayer(newAddressPoints).addTo(this.map);
  }

  private addLayer(markers: any[], icon: L.Icon): void {
    markers.forEach((m, i) => {
      let layer = L.marker(L.latLng([m.lat, m.lng]), {icon : icon}).bindPopup(m.toolTip);
      this.markerClusterGroup.addLayer(layer).addTo(this.map);
    });
  }

  private isFloat(n) {
    return n === +n && n !== (n|0);
  }
}
