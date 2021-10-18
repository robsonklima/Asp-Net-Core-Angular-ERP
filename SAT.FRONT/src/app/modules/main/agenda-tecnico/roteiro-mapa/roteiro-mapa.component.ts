import { AfterViewInit, Component } from '@angular/core';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import * as L from 'leaflet';
import 'leaflet-routing-machine';
import moment from 'moment';

@Component({
  selector: 'app-roteiro-mapa',
  templateUrl: './roteiro-mapa.component.html',
  styles: ['div{ height: 100%; width: 100%; z-index: 1 }']
})
export class RoteiroMapaComponent implements AfterViewInit {
  
  constructor(
    private _osSvc: OrdemServicoService
  ) {}

  async ngAfterViewInit() {
    const chamados = await this._osSvc.obterPorParametros({
      codTecnico: 1548,
      dataTransfInicio: moment().add(-1, 'days').toISOString(),
      dataTransfFim: moment().add(1, 'days').toISOString(),
      sortActive: 'dataHoraTransf',
      sortDirection: 'asc'
    }).toPromise();

    console.log(chamados);
    

    let map = L.map('map').setView([-29.95046971811084, -51.09702372913963], 13);

    const waypoints: L.LatLng[] = [
      L.latLng(-29.95046971811084, -51.09702372913963),
      L.latLng(-29.87175859921068, -50.978990839760506),
      L.latLng(-29.380996881372884, -50.87513156107664),
      L.latLng(-29.154686525737386, -51.18852220057508),
      L.latLng(-30.013567377672995, -50.14855772662447)
    ];

    const icon = new L.Icon({
      iconUrl: 'assets/icons/destination-32.png',
      iconSize: [32, 32],
      iconAnchor: [15, 32],
      popupAnchor: [1, -32]
    });

    L.Routing.control({
			waypoints: waypoints
		}).addTo(map);

    //map.fitBounds([]);

		L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
			attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
		}).addTo(map);
  }
}
