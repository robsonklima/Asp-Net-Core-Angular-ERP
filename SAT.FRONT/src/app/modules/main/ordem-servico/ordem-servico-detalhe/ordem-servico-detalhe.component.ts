import { AfterViewInit, ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Foto } from 'app/core/types/foto.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { first } from 'rxjs/operators';
import * as L from 'leaflet';

@Component({
  selector: 'app-ordem-servico-detalhe',
  templateUrl: './ordem-servico-detalhe.component.html',
  styleUrls: ['./ordem-servico-detalhe.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class OrdemServicoDetalheComponent implements AfterViewInit {
  codOS: number;
  os: OrdemServico;
  fotos: Foto[] = [];
  map: L.Map;

  constructor(
    private _route: ActivatedRoute,
    private _ordemServicoService: OrdemServicoService,
    private _cdr: ChangeDetectorRef
  ) { }

  ngAfterViewInit(): void {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.obterDadosOrdemServico();
    this._cdr.detectChanges();
  }

  trocarTab(tab: any) {
    if (tab.index !== 3 || !this.os) {
      return;
    }

    this.map = L.map('map', {
      scrollWheelZoom: false,
    }).setView([
      +this.os.localAtendimento.latitude, 
      +this.os.localAtendimento.longitude
    ], 14);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: 'SAT 2.0'
    }).addTo(this.map);

    L.marker([
      +this.os.localAtendimento.latitude, 
      +this.os.localAtendimento.longitude
    ]).bindPopup(this.os.localAtendimento.nomeLocal)
      .addTo(this.map);

    this.map.invalidateSize();
  }

  private obterDadosOrdemServico(): void {
    this._ordemServicoService.obterPorCodigo(this.codOS)
      .pipe(first())
      .subscribe(res => {
        this.os = res;
      });
  }
}