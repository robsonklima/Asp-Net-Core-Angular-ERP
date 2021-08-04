import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Foto } from 'app/core/types/foto.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { first } from 'rxjs/operators';
import * as L from 'leaflet';

@Component({
  selector: 'app-ordem-servico-detalhe',
  templateUrl: './ordem-servico-detalhe.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class OrdemServicoDetalheComponent implements OnInit {
  tecnicos: string[] = ['João da Silva', 'Maria Vasconcelos', 'Adriano Nunes'];
  codOS: number;
  os: OrdemServico;
  fotos: Foto[] = [];
  map: L.Map;
  mapOptions: L.MapOptions;
  //markerClusterGroup: L.MarkerClusterGroup;
  markerClusterData = [];

  constructor(
    private _route: ActivatedRoute,
    private _ordemServicoService: OrdemServicoService,
  ) { }

  ngOnInit(): void {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.obterDadosOrdemServico();

    this.mapOptions = {
      center: L.latLng([
        +this.os.localAtendimento?.latitude,
        +this.os.localAtendimento?.longitude
      ]),
      zoom: 10,
      layers: [
        L.tileLayer(
          'http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
          {
            maxZoom: 18,
            attribution: 'SAT 2.0'
          })
      ],
    };

    //this.markerClusterGroup = L.markerClusterGroup({ removeOutsideVisibleBounds: true });
  }

  private obterDadosOrdemServico(): void {
    this._ordemServicoService.obterPorCodigo(this.codOS)
      .pipe(first())
      .subscribe(res => {
        this.os = res;
      });
  }
}