import { AfterViewInit, ChangeDetectorRef, Component } from '@angular/core';
import L, { circle, divIcon, Icon, latLng, marker, polygon, tileLayer, Map } from 'leaflet';

@Component({
  selector: 'app-densidade',
  templateUrl: './densidade.component.html'
})
export class DensidadeComponent implements AfterViewInit {

  options = {
    layers: [
      tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap'
      })
    ],
    zoom: 8,
    center: latLng([46.879966, -121.726909])
  };

  constructor(
    private _cdr: ChangeDetectorRef
  ) { }

  ngAfterViewInit(): void {
    this._cdr.markForCheck();
  }

  onMapReady(map: Map): void {
    var icon = new L.Icon.Default();
    icon.options.shadowSize = [0,0];
    
    var marker = new L.Marker([ 46.879966, -121.726909 ], {icon : icon}).addTo(map);
  }
}
