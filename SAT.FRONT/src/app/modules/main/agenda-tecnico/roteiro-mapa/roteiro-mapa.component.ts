import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
declare var L:any;
import 'leaflet';
import 'leaflet-routing-machine';

export interface DialogData {
  resource: any[];
  chamados: OrdemServico[]
}

@Component({
  selector: 'app-roteiro-mapa',
  templateUrl: './roteiro-mapa.component.html',
  styles: [`
    div { height: 100%; width: 100%; z-index: 1; margin-left: 0px  }
  `]
})
export class RoteiroMapaComponent implements OnInit {
  loading: boolean;
  chamados: OrdemServico[] = [];
  resource: any;

  constructor(
    private _usuarioSvc: UserService,
    public dialogRef: MatDialogRef<RoteiroMapaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {
    this.chamados = data.chamados;
    this.resource = data.resource;
  }

  async ngOnInit() {
    this.loading = true;

    const paradas = this.chamados
      .filter(os => os.localAtendimento !== null)
      .filter(os => os.localAtendimento.latitude !== null && os.localAtendimento.longitude !== null)
      .map(os => { return L.latLng(+os.localAtendimento.latitude, +os.localAtendimento.longitude) });

    const usuarios = await this._usuarioSvc.obterPorParametros({
      nomeUsuario: this.resource.name,
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
        return L.marker(waypoint.latLng, {
          draggable: false,
          bounceOnAdd: false,
          bounceOnAddOptions: {
            duration: 1000,
            height: 800
          },
          icon: (usuarios.items[0].localizacoes.length > 0 && i == 0) ? L.icon({
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
        })
        .bindPopup((usuarios.items[0].localizacoes.length > 0 && i == 0) ? 'Localização do Técnico' : `${i+1}° Atendimento`);
      },
      lineOptions : {
        addWaypoints: false,
        styles:[{color: 'green', opacity: 1, weight: 3}]
      }
    }).addTo(map);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    map.invalidateSize();
    this.loading = false;
  }

  fecharModal(): void {
    this.dialogRef.close();
  }
}
