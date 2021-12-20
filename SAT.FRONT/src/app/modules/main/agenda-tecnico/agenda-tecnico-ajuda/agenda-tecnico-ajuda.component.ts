import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-agenda-tecnico-ajuda',
  templateUrl: './agenda-tecnico-ajuda.component.html',
  styleUrls: ['./agenda-tecnico-ajuda.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class AgendaTecnicoAjudaDialogComponent implements OnInit
{

  @Input() sidenav: MatSidenav;

  constructor ()
  { }

  async ngOnInit()
  {
  }
}