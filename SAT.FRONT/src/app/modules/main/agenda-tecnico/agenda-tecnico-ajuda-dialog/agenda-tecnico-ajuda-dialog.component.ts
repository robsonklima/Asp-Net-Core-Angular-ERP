import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-agenda-tecnico-ajuda-dialog',
  templateUrl: './agenda-tecnico-ajuda-dialog.component.html',
  styleUrls: ['./agenda-tecnico-ajuda-dialog.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class AgendaTecnicoAjudaDialogComponent implements OnInit
{

  constructor ()
  { }

  async ngOnInit()
  {
  }
}