import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { UserService } from 'app/core/user/user.service';
declare var L:any;
import 'leaflet';
import 'leaflet-routing-machine';
import moment from 'moment';

@Component({
  selector: 'app-roteiro-mapa',
  templateUrl: './roteiro-mapa.component.html',
  styles: ['div{ height: 100%; width: 100%; z-index: 1 }']
})
export class RoteiroMapaComponent implements OnInit {
  codTecnico: number;
  loading: boolean;

  constructor(
    private _route: ActivatedRoute,
    private _osSvc: OrdemServicoService,
    private _usuarioSvc: UserService
  ) { }

  async ngOnInit() {
    this.loading = true;
    this.codTecnico = +this._route.snapshot.paramMap.get('codTecnico');

    const chamados = await this._osSvc.obterPorParametros({
      codTecnico: this.codTecnico,
      dataTransfInicio: moment().add(-1, 'days').toISOString(),
      dataTransfFim: moment().add(1, 'days').toISOString(),
      sortActive: 'dataHoraTransf',
      sortDirection: 'asc',
    }).toPromise();

    const paradas = chamados.items
      .filter(os => os.localAtendimento !== null)
      .filter(os => os.localAtendimento.latitude !== null && os.localAtendimento.longitude !== null)
      .map(os => { return L.latLng(+os.localAtendimento.latitude, +os.localAtendimento.longitude) });

    const usuarios = await this._usuarioSvc.obterPorParametros({
      codTecnico: this.codTecnico,
      indAtivo: 1
    }).toPromise();

    if (usuarios.items[0].localizacoes.length > 0) {
      paradas.unshift(
        L.latLng(usuarios.items[0].localizacoes[0].latitude, usuarios.items[0].localizacoes[0].longitude)
      );
    }

    let map = L.map('map').setView([+paradas[0].lat, +paradas[0].lng], 9);

    L.Routing.control({
      waypoints: paradas,
      createMarker: (i: number, waypoint: any, n: number) => {
        const marker = L.marker(waypoint.latLng, {
          draggable: false,
          bounceOnAdd: false,
          bounceOnAddOptions: {
            duration: 1000,
            height: 800
          },
          icon: (i == 0) ? L.icon({
            iconUrl: './assets/icons/sport-car-64.png',
            iconSize: [32, 32],
            iconAnchor: [15, 32],
            popupAnchor: [0, -32],
            shadowUrl: null,
          }) : L.icon({
            iconUrl: './assets/icons/atm-64.png',
            iconSize: [32, 32],
            iconAnchor: [15, 32],
            popupAnchor: [0, -32],
            shadowUrl: null,
          })
        }).bindPopup((usuarios.items[0].localizacoes.length > 0 && i == 0) ? 'Localização do Técnico' : `${i+1}° Atendimento`);

        return marker;
      },
      lineOptions : {
        addWaypoints: false
      }
    }).addTo(map);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    map.invalidateSize();
    this.loading = false;
  }
}
