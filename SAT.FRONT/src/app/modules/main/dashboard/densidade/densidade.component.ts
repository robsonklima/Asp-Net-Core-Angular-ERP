import { AutorizadaService } from 'app/core/services/autorizada.service';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import * as L from "leaflet";
import 'leaflet.markercluster';
import { latLng, tileLayer, Map } from 'leaflet';
import 'leaflet.heat/dist/leaflet-heat.js'
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { Regiao } from 'app/core/types/regiao.types';
import { Autorizada, AutorizadaParameters } from 'app/core/types/autorizada.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardDensidadeEquipamentos } from 'app/core/types/dashboard.types';
import { UserService } from 'app/core/user/user.service';
import { FilialService } from 'app/core/services/filial.service';
import { MatSidenav } from '@angular/material/sidenav';
import { Filterable } from 'app/core/filters/filterable';
import { IFilterable } from 'app/core/types/filtro.types';
import moment from 'moment';
import { statusConst } from 'app/core/types/status-types';
import Enumerable from 'linq';

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
		private _autorizadaService: AutorizadaService,
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

		if (params.exibirEquipamentos)
			await this.obterEquipamentosContrato(params)
		if (params.exibirAutorizadas)
			this.obterAutorizadas(params);
		if (params.exibirTecnicos)
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
		const data = await this._dashboardService.obterViewPorParametros({
			dashboardViewEnum: DashboardViewEnum.DENSIDADE_TECNICOS,
			codFiliais: params.codFiliais,
			codRegioes: params.codRegioes,
			codAutorizadas: params.codAutorizadas,
		}).toPromise();

		let markers: any[] = data.viewDashboardDensidadeTecnicos.filter(t => this.isFloat(+t.latitude) && this.isFloat(+t.longitude)).map((tecnico: any) => {
			return {
				lat: +tecnico.latitude,
				lng: +tecnico.longitude,
				toolTip: `
						<table>
							<tbody>				
							<tr>
								<td>${tecnico.tecnico}</td>
							</tr>
							<tr>
								<td>${tecnico.fonePerto}</td>
							</tr>
							<tr>
								<td>${tecnico.cidadeTecnico} - ${tecnico.ufTecnico}</td>
							</tr>
							<tr>
								<td>Adimissão: ${moment(tecnico.dataAdmissao).format('DD/MM/yyyy')}</td>
							</tr>
							</tbody>
						</table>
						`
			}
		});

		var icon = new L.Icon({
			iconUrl: 'assets/icons/home-32.png',
			iconSize: [32, 32],
			iconAnchor: [15, 32],
			popupAnchor: [1, -32]
		});

		let group = L.layerGroup();

		markers.forEach(async (m, i) => {
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
			codFiliais: params.codFiliais,
			codRegioes: params.codRegioes,
			codAutorizadas: params.codAutorizadas,
			codClientes: params.codClientes,
			codEquips: params.codEquips
		}).toPromise();

		const densidade = data.viewDashboardDensidadeEquipamentos;


		this.markers = densidade.filter(e => this.isFloat(+e.latitude) && this.isFloat(+e.longitude)).map((equip) => {
			return {
				lat: +equip.latitude,
				lng: +equip.longitude,
				toolTip: `
						<table>
							<tbody>				
							<tr>
								<td>Modelo: </td>
								<td>${equip.equipamento}</td>
							</tr>
							<tr>
								<td>Série: </td>
								<td>${equip.numSerie}</td>
							</tr>
							<tr>
								<td>Cliente: </td>
								<td>${equip.cliente}</td>
							</tr>
							</tbody>
						</table>
						`
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

	private async obterAutorizadas(params: any = null) {
		let autorizadaParams: AutorizadaParameters = {
			indAtivo: statusConst.ATIVO,
			codFiliais: params.codFiliais,
			codAutorizadas: params.codAutorizadas,
			pageSize: 1000
		};
		const data = await this._autorizadaService
			.obterPorParametros(autorizadaParams)
			.toPromise();

		let markers: any[] = data.items.filter(t => t.latitude != null && t.longitude != null && this.isFloat(+t.latitude) && this.isFloat(+t.longitude)).map((autorizada: any) => {
			return {
				lat: +autorizada.latitude,
				lng: +autorizada.longitude,
				toolTip: `
							<table>
								<tbody>				
								<tr>
									<td>${autorizada.razaoSocial}</td>
								</tr>						
								<tr>
									<td>${autorizada.nomeFantasia}</td>
								</tr>						
								</tbody>
							</table>
							`
			}
		});

		var icon = new L.Icon({
			iconUrl: 'assets/icons/building.png',
			iconSize: [32, 32],
			iconAnchor: [15, 32],
			popupAnchor: [1, -32]
		});

		let group = L.layerGroup();

		markers.forEach(async (m, i) => {
			let layer = L.marker(L.latLng([m.lat, m.lng]), { icon: icon }).bindPopup(m.toolTip);
			group.addLayer(layer);
		});

		group.addTo(this.map);

		this.map.fitBounds(markers);
		this.map.invalidateSize();

	}

	private isFloat(n) {
		return n === +n && n !== (n | 0);
	}
}
