import { AfterViewInit, Component, ViewChild } from '@angular/core';
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
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';

@Component({
	selector: 'app-densidade',
	templateUrl: './densidade.component.html'
})
export class DensidadeComponent extends Filterable implements AfterViewInit, IFilterable {

	@ViewChild('sidenav') sidenav: MatSidenav;

	usuarioSessao: UsuarioSessao;
	filiais: Filial[] = [];
	map: Map;
	markers: any[];
	markerClusterGroup: L.MarkerClusterGroup;
	markerClusterData = [];
	codFilial: number;
	regioes: Regiao[] = [];
	autorizadas: Autorizada[] = [];
	loading: boolean = true;

	options: L.MapOptions = {
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
		protected _userService: UserService,
	) {
		super(_userService, 'dashboard-densidade')
		this.usuarioSessao = JSON.parse(this._userService.userSession);
	}
	ngAfterViewInit(): void {
		this.selecionarFilial(this.filter?.parametros);
		this.registerEmitters();
	}

	registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
		});
	}

	onSidenavClosed(): void {
		super.loadFilter();
		this.selecionarFilial(this.filter?.parametros);
	}

	async onMapReady(map: Map) {
		this.loading = true;
		this.map = map;

		this.selecionarFilial(this.filter?.parametros);

		this.loading = false;
	}

	public async selecionarFilial(params = null) {
		this.loading = true;
		
		this.limparMapa();
		await this.obterEquipamentosContrato(params)
		this.obterTecnicos(params);

		this.loading = false;
	}

	private limparMapa() {
		this.map.eachLayer((layer) => {
			if (layer instanceof L.MarkerClusterGroup || 
				layer instanceof L.LayerGroup) {
				this.map.removeLayer(layer);
			}
		})
	}

	private async obterTecnicos(params: any = null) {
		let data = await this._dashboardService.obterViewPorParametros({
			dashboardViewEnum: DashboardViewEnum.DENSIDADE_TECNICOS,
			codFilial: params.codFilial ?? this.usuarioSessao.usuario.codFilial,
			codRegiao: params.codRegiao ,
			codAutorizada: params.codAutorizada
		}).toPromise();

		let markers: any[] = data.viewDashboardDensidadeTecnicos.filter(t => this.isFloat(+t.latitude) && this.isFloat(+t.longitude)).map((tecnico: any) => {
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
		
		let group = L.layerGroup();

		markers.forEach( async (m, i) => {
			let layer = L.marker(L.latLng([m.lat, m.lng]), { icon: icon }).bindPopup(m.toolTip);
			group.addLayer(layer);
		});

		group.addTo(this.map);
		
		this.map.fitBounds(markers);
		this.map.invalidateSize();
	}

	private async obterEquipamentosContrato(params: any = null) {
		const data = await this._dashboardService.obterViewPorParametros({
			dashboardViewEnum: DashboardViewEnum.DENSIDADE_EQUIPAMENTOS,
			codFilial: params.codFilial ?? this.usuarioSessao.usuario.codFilial,
			codRegiao: params.codRegiao,
			codAutorizada: params.codAutorizada,
			codClientes: params.codClientes
		}).toPromise();

		const densidade = data.viewDashboardDensidadeEquipamentos;

		this.markers = densidade.filter(e => this.isFloat(+e.latitude) && this.isFloat(+e.longitude)).map((equip) => {
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

		this.markers.forEach((m, i) => {
			let layer = L.marker(L.latLng([m.lat, m.lng]), { icon: icon }).bindPopup(m.toolTip);
			this.markerClusterGroup.addLayer(layer).addTo(this.map);
		});
	}

	private isFloat(n) {
		return n === +n && n !== (n | 0);
	}
}
