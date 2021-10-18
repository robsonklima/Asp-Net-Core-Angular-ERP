import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import * as L from 'leaflet';
import 'leaflet-routing-machine';
import moment from 'moment';

@Component({
  selector: 'app-roteiro-mapa',
  templateUrl: './roteiro-mapa.component.html',
  styles: ['div{ height: 100%; width: 100%; z-index: 1 }']
})
export class RoteiroMapaComponent implements OnInit {
  codTecnico: number;

  constructor(
    private _route: ActivatedRoute,
    private _osSvc: OrdemServicoService
  ) { }

  async ngOnInit() {
    this.codTecnico = +this._route.snapshot.paramMap.get('codTecnico');

    const chamados = await this._osSvc.obterPorParametros({
      codTecnico: this.codTecnico,
      dataTransfInicio: moment().add(-1, 'days').toISOString(),
      dataTransfFim: moment().add(1, 'days').toISOString(),
      sortActive: 'dataHoraTransf',
      sortDirection: 'asc',
    }).toPromise();

    const waypoints = chamados.items
      .filter(os => os.localAtendimento !== null)
      .filter(os => os.localAtendimento.latitude !== null && os.localAtendimento.longitude !== null)
      .map(os => {
        return L.latLng(+os.localAtendimento.latitude, +os.localAtendimento.longitude
      )});

    setTimeout(() => {
      let map = L.map('map').setView([+waypoints[0].lat, +waypoints[0].lng], 9);

      L.Routing.control({ 
        waypoints: waypoints,
      }).addTo(map);

      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
      }).addTo(map);

      map.invalidateSize();
    }, 0);
  }
}
