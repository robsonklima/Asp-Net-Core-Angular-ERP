import { Component } from '@angular/core';
import * as L from "leaflet";
import 'leaflet.markercluster';
import { latLng, tileLayer, Map } from 'leaflet';
import 'leaflet.heat/dist/leaflet-heat.js'
import { Filial } from 'app/core/types/filial.types';
import { Regiao } from 'app/core/types/regiao.types';
import { Autorizada } from 'app/core/types/autorizada.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum } from 'app/core/types/dashboard.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-densidade',
  templateUrl: './densidade.component.html'
})
export class DensidadeComponent {
  usuarioSessao: UsuarioSessao;
  filiais: Filial[] = [];
  map: Map;
  markerClusterGroup: L.MarkerClusterGroup;
  markerClusterData = [];
  codFilial: number;
  regioes: Regiao[] = [];
  autorizadas: Autorizada[] = [];
  loading: boolean = true;

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
    private _dashboardService: DashboardService,
    private _userService: UserService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  onMapReady(map: Map): void {
    this.map = map;
    this.obterEquipamentosContrato().then(async () => {
      this.obterTecnicos();
    });
  }

  private async obterEquipamentosContrato() {
    const data = await this._dashboardService.obterViewPorParametros({
      dashboardViewEnum: DashboardViewEnum.DENSIDADE_EQUIPAMENTOS,
      codFilial: this.usuarioSessao.usuario.codFilial
    }).toPromise();
    const densidade = data.viewDashboardDensidadeEquipamentos;

    let markers: any[] = densidade.filter(e => this.isFloat(+e.latitude) && this.isFloat(+e.longitude)).map((equip) => {
      return {
        lat: +equip.latitude,
        lng: +equip.longitude,
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
    this.loading = false;
  }

  private async obterTecnicos() {
    const data = await this._dashboardService.obterViewPorParametros({
      dashboardViewEnum: DashboardViewEnum.DENSIDADE_TECNICOS,
      codFilial: this.usuarioSessao.usuario.codFilial
    }).toPromise();
      
    const tecnicos = data.viewDashboardDensidadeTecnicos;

    let markers: any[] = tecnicos.filter(t => this.isFloat(+t.latitude) && this.isFloat(+t.longitude)).map((tecnico: any) => {
      return {
        lat: +tecnico.latitude,
        lng: +tecnico.longitude,
        toolTip: tecnico.tecnico
      }
    });

    var icon = new L.Icon({
      iconUrl: 'assets/icons/home-32.png',
      iconSize: [32, 32],
      iconAnchor: [15, 32],
      popupAnchor: [1, -32]
    });

    this.addMarkersOnMap(markers, icon);
    this.loading = false;
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
