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
		protected _userService: UserService,
		private _filialService: FilialService
	) {
		super(_userService, 'dashboard-densidade')
		this.usuarioSessao = JSON.parse(this._userService.userSession);
	}
	ngAfterViewInit(): void {
		this.selecionarFilial();
		this.registerEmitters();
	}

	registerEmitters(): void {
		this.sidenav.closedStart.subscribe(() => {
			this.onSidenavClosed();
		});
	}

	onSidenavClosed(): void {
		super.loadFilter();
		this.selecionarFilial(this.filter?.parametros.codFilial);
	}

	async onMapReady(map: Map) {
		this.loading = true;
		this.map = map;

		await this.obterEquipamentosContrato()
		await this.obterTecnicos();
		this.loading = false;
	}

	public async selecionarFilial(codFilial: number = null) {
		this.loading = true;

		this.limparMapa();
		await this.obterEquipamentosContrato(codFilial)
		await this.obterTecnicos(codFilial);

		this.loading = false;
	}

	private limparMapa() {
		this.map.eachLayer((layer) => {
			if (layer instanceof L.MarkerClusterGroup) {
				this.map.removeLayer(layer);
			}
		})
	}

	private async obterTecnicos(codFilial: number = null) {
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

	private async obterEquipamentosContrato(codFilial: number = null) {
		const data = await this._dashboardService.obterViewPorParametros({
			dashboardViewEnum: DashboardViewEnum.DENSIDADE_EQUIPAMENTOS,
			codFilial: codFilial ?? this.usuarioSessao.usuario.codFilial,
			codRegiao: this.filter?.parametros.codRegiao ,
			codAutorizada: this.filter?.parametros.codAutorizada
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
