import { AfterViewInit, Component, Input } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
@Component({
  selector: 'app-suporte-stn-laudo-filtro',
  templateUrl: './suporte-stn-laudo-filtro.component.html'
})
export class SuporteStnLaudoFiltroComponent implements AfterViewInit {
    @Input() sidenav: MatSidenav;
  ngAfterViewInit() { }
}