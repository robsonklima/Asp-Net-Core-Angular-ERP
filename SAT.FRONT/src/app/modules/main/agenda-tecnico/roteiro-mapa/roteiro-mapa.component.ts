import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
declare var L: any;
import 'leaflet';
import 'leaflet-routing-machine';
import Enumerable from 'linq';

export interface DialogData
{
  codUsuario: string;
  chamados: OrdemServico[]
}

@Component({
  selector: 'app-roteiro-mapa',
  templateUrl: './roteiro-mapa.component.html',
  styles: [`
    div { height: 100%; width: 100%; margin-left: 0px; position: relative; }
    .leaflet-right { display: none !important; }
  `]
})
export class RoteiroMapaComponent implements OnInit
{
  chamados: OrdemServico[] = [];
  codUsuario: any;
  isLoading: boolean = false;
  paradas: any[] = [];

  constructor (
    private _usuarioSvc: UserService,
    public dialogRef: MatDialogRef<RoteiroMapaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  )
  {
    this.chamados = data.chamados;
    this.codUsuario = data.codUsuario;
  }

  async ngOnInit()
  {
    this.isLoading = true;

    this.paradas = this.chamados
      .filter(os => os.localAtendimento !== null)
      .filter(os => os.localAtendimento.latitude != null && os.localAtendimento.latitude != '' && os.localAtendimento.longitude !== null && os.localAtendimento.latitude != '')
      .map(os => { return L.latLng(+os.localAtendimento?.latitude, +os.localAtendimento.longitude) });

    const usuario = await this._usuarioSvc.obterPorCodigo(this.codUsuario).toPromise();

    const ultimaLocalizacao = Enumerable.from(usuario.localizacoes)
      .orderByDescending(i => i.dataHoraCad)
      .firstOrDefault();

    // id 1
    var residencia: any = null;
    if (usuario?.tecnico?.latitude != null && usuario?.tecnico.latitude != '' && usuario?.tecnico?.longitude != null && usuario?.tecnico?.longitude != '')
      residencia = L.latLng(usuario?.tecnico?.latitude, usuario?.tecnico?.longitude)

    // id 0
    if (ultimaLocalizacao != null)
      this.paradas.unshift(L.latLng(ultimaLocalizacao.latitude, ultimaLocalizacao.longitude));

    // renderiza mapa
    if (this.paradas[0]?.lat && this.paradas[0]?.lng)
    {
      var map = L.map('map').setView([+this.paradas[0]?.lat, +this.paradas[0]?.lng], 9);

      L.Routing.control({
        waypoints: this.paradas,
        createMarker: (i: number, waypoint: any, n: number) =>
        {
          return L.marker(waypoint.latLng, {
            draggable: false,
            bounceOnAdd: false,
            bounceOnAddOptions: {
              duration: 1000,
              height: 800
            },
            icon: (ultimaLocalizacao != null && i == 0) ? L.icon({
              iconUrl: './assets/icons/sport-car-64.png',
              iconSize: [32, 32],
              iconAnchor: [15, 32],
              popupAnchor: [0, -32],
              shadowUrl: null,
            }) :
              (residencia != null && i == 1) ?
                L.icon({
                  iconUrl: './assets/icons/home-64.png',
                  iconSize: [32, 32],
                  iconAnchor: [15, 32],
                  popupAnchor: [0, -32],
                  shadowUrl: null,
                })
                :
                L.icon({
                  iconUrl: './assets/icons/atm-64.png',
                  iconSize: [32, 32],
                  iconAnchor: [15, 32],
                  popupAnchor: [0, -32],
                  shadowUrl: null,
                })
          })
            .bindPopup((ultimaLocalizacao != null && i == 0) ? 'Localização do Técnico' : (residencia != null && i == 1) ? 'Residência do Técnico' : `${i + 1}° Atendimento`);
        },
        lineOptions: {
          addWaypoints: false,
          styles: [{ color: 'green', opacity: 1, weight: 3 }]
        },
        show: false
      }).addTo(map);

      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
      }).addTo(map);

      map.invalidateSize();
    }
    this.isLoading = false;
  }

  fecharModal(): void
  {
    this.dialogRef.close();
  }
}
