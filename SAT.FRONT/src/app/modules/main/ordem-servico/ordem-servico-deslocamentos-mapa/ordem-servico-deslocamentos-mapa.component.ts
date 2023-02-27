import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { ViewTecnicoDeslocamento } from 'app/core/types/tecnico.types';
import 'leaflet';
import { LatLng } from 'leaflet';
import 'leaflet-routing-machine';
import { OrdemServicoDeslocamentosComponent } from '../ordem-servico-deslocamentos/ordem-servico-deslocamentos.component';
declare var L: any;

export interface DialogData
{
  codUsuario: string;
  chamados: OrdemServico[]
}

@Component({
  selector: 'app-ordem-servico-deslocamentos-mapa',
  templateUrl: './ordem-servico-deslocamentos-mapa.component.html',
  styles: [`
    div { height: 100%; width: 100%; margin-left: 0px; position: relative; }
    .leaflet-right { display: none !important; }
  `]
})
export class OrdemServicoDeslocamentosMapaComponent implements OnInit {
  deslocamento: ViewTecnicoDeslocamento;
  isLoading: boolean = false;

  constructor (
    public dialogRef: MatDialogRef<OrdemServicoDeslocamentosComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.deslocamento = data.deslocamento;
  }

  async ngOnInit() {
    var map = new L.Map('map');

    L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    L.Routing.control({
      waypoints: this.obterWaypoints(),
      createMarker: (i: number, wp: any, n: number) => {
        return L.marker(wp.latLng, {
          draggable: false,
          bounceOnAdd: false,
          bounceOnAddOptions: {
            duration: 1000,
            height: 800
          }
        })
        .bindPopup(this.obterTooltip(wp))
      },
      lineOptions: {
        addWaypoints: false,
        styles: [{ color: 'green', opacity: 1, weight: 3 }]
      },
      show: true
    }).addTo(map);
  }

  private obterTooltip(wp: any): string {
    if (wp.latLng.lat == this.deslocamento.localLat)
      return 'Local de Atendimento';

    if (wp.latLng.lat == this.deslocamento.intencaoLat)
      return 'Intenção';
    
    if (wp.latLng.lat == this.deslocamento.checkinLat)
      return 'Checkin';

    if (wp.latLng.lat == this.deslocamento.checkoutLat)
      return 'Checkout';

    return 'Posição';
  }

  private obterWaypoints(): any[] {
    let wps = [];

    if (this.deslocamento.intencaoLat && this.deslocamento.intencaoLng)
      wps.push(new LatLng(+this.deslocamento.intencaoLat, +this.deslocamento.intencaoLng));

    if (this.deslocamento.checkinLat && this.deslocamento.checkinLng)
      wps.push(new LatLng(+this.deslocamento.checkinLat, +this.deslocamento.checkinLng));

    if (this.deslocamento.checkoutLat && this.deslocamento.checkoutLng)
      wps.push(new LatLng(+this.deslocamento.checkoutLat, +this.deslocamento.checkoutLng));

    if (this.deslocamento.localLat && this.deslocamento.localLng)
      wps.push(new LatLng(+this.deslocamento.localLat, +this.deslocamento.localLng));

    return wps;
  }

  fecharModal(): void {
    this.dialogRef.close();
  }
}
