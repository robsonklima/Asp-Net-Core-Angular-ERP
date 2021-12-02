import { Component, Input, OnInit } from '@angular/core';
import * as L from "leaflet";
import 'leaflet.markercluster';
import { latLng, tileLayer, Map } from 'leaflet';
import 'leaflet.heat/dist/leaflet-heat.js'
import { TecnicoService } from 'app/core/services/tecnico.service';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { Filial } from 'app/core/types/filial.types';
import { RegiaoAutorizadaService } from 'app/core/services/regiao-autorizada.service';
import { Regiao } from 'app/core/types/regiao.types';
import { Autorizada } from 'app/core/types/autorizada.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-densidade',
  templateUrl: './densidade.component.html'
})
export class DensidadeComponent extends Filterable implements OnInit, IFilterable {
  @Input() sidenav: MatSidenav;
  usuarioSessao: UsuarioSessao;
  filiais: Filial[] = [];
  map: Map;
  markerClusterGroup: L.MarkerClusterGroup;
  markerClusterData = [];
  codFilial: number = 4;
  regioes: Regiao[] = [];
  autorizadas: Autorizada[] = [];

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
    private _equipamentoContratoSvc: EquipamentoContratoService,
    private _tecnicoSvc: TecnicoService,
    private _regiaoAutorizadaSvc: RegiaoAutorizadaService,
    protected _userService: UserService
  ) {
    super(_userService, 'dashboard-filtro');
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.registerEmitters();
  }

  registerEmitters(): void {
    this.sidenav.closedStart.subscribe(() => {
      this.onSidenavClosed();
      this.obterRegioesAutorizadas();
      this.obterTecnicos();
      this.obterEquipamentosContrato();
    })
  }

  loadFilter(): void {
    super.loadFilter();
  }

  onMapReady(map: Map): void {
    this.map = map;
  }

  private async obterRegioesAutorizadas() {
    const data = await this._regiaoAutorizadaSvc.obterPorParametros({
      ...{ indAtivo: 1 }, ...this.filter?.parametros
    }).toPromise();

    this.regioes = data.items.map(ra => ra.regiao);
    this.autorizadas = data.items.map(ra => ra.autorizada);

    let markers: any[] = this.autorizadas.filter(a => this.isFloat(+a.latitude) && this.isFloat(+a.longitude)).map((autorizada) => {
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
      ...{ indAtivo: 1 }, ...this.filter?.parametros
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
      ...{ indAtivo: 1 }, ...this.filter?.parametros
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
      let marker = new L.Marker([+m.lat, +m.lng], { icon: icon }).bindPopup(m.toolTip);
      marker.addTo(this.map);
    });

    this.map.fitBounds(markers);
    this.map.invalidateSize();
  }

  private addLayer(markers: any[], icon: L.Icon): void {
    this.markerClusterGroup = L.markerClusterGroup({ removeOutsideVisibleBounds: true });

    markers.forEach((m, i) => {
      let layer = L.marker(L.latLng([m.lat, m.lng]), { icon: icon }).bindPopup(m.toolTip);
      this.markerClusterGroup.addLayer(layer).addTo(this.map);
    });
  }

  private isFloat(n) {
    return n === +n && n !== (n | 0);
  }
}
