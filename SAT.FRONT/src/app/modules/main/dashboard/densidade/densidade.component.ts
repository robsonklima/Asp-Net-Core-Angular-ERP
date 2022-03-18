import { Component } from '@angular/core';
import * as L from "leaflet";
import 'leaflet.markercluster';
import { latLng, tileLayer, Map } from 'leaflet';
import 'leaflet.heat/dist/leaflet-heat.js'
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { Regiao } from 'app/core/types/regiao.types';
import { Autorizada } from 'app/core/types/autorizada.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardDensidadeEquipamentos } from 'app/core/types/dashboard.types';
import { UserService } from 'app/core/user/user.service';
import { FilialService } from 'app/core/services/filial.service';

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
    private _userService: UserService,
    private _filialService: FilialService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async onMapReady(map: Map) {
    this.loading = true;
    this.map = map;

    await this.obterFiliais();
    await this.obterEquipamentosContrato()
    await this.obterTecnicos();
    this.loading = false;
  }

  private async obterFiliais() {
    const params: FilialParameters = {
      sortActive: 'nomeFilial',
      sortDirection: 'asc',
      indAtivo: 1
    }
    const data = await this._filialService.obterPorParametros(params).toPromise();
    this.filiais = data.items;
  }

  public async selecionarFilial(codFilial: number) {
    this.loading = true;

    this.limparMapa();
    await this.obterEquipamentosContrato(codFilial)
    await this.obterTecnicos(codFilial);

    this.loading = false;
  }

  private limparMapa() {
    this.map.eachLayer((layer) => {
      if (layer instanceof L.MarkerClusterGroup)
      {
        this.map.removeLayer(layer);
      }
    })
  }

  private async obterTecnicos(codFilial: number=null) {
    const data = await this._dashboardService.obterViewPorParametros({
      dashboardViewEnum: DashboardViewEnum.DENSIDADE_TECNICOS,
      codFilial: codFilial || this.usuarioSessao.usuario.codFilial
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

    this.markerClusterGroup = L.markerClusterGroup({ removeOutsideVisibleBounds: true });

    markers.forEach((m, i) => {
      let layer = L.marker(L.latLng([m.lat, m.lng]), { icon: icon }).bindPopup(m.toolTip);
      
      this.markerClusterGroup.addLayer(layer).addTo(this.map);
    });
  
    this.map.fitBounds(markers);
    this.map.invalidateSize();
  }

  private async obterEquipamentosContrato(codFilial: number=null) {
    const data = await this._dashboardService.obterViewPorParametros({
      dashboardViewEnum: DashboardViewEnum.DENSIDADE_EQUIPAMENTOS,
      codFilial: codFilial || this.usuarioSessao.usuario.codFilial
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
