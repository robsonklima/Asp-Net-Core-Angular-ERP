import { Component, OnInit } from '@angular/core';
import { latLng, tileLayer } from 'leaflet';

@Component({
  selector: 'app-densidade',
  templateUrl: './densidade.component.html',
  styles: [`.map {
    height: 100%;
    padding: 0;
  }`]
})
export class DensidadeComponent implements OnInit {

  options = {
    layers: [
      tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
      })
    ],
    zoom: 7,
    center: latLng([46.879966, -121.726909])
  };

  constructor() { }

  ngOnInit(): void {
  }
}
