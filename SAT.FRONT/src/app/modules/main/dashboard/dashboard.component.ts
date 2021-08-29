import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements AfterViewInit {
  @ViewChild('sidenav') sidenav: MatSidenav;

  constructor(
    
  ) {}

  async ngAfterViewInit() {
   
  }
}
