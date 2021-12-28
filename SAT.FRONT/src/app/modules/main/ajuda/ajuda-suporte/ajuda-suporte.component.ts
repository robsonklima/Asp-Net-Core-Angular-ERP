import { Component, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-ajuda-suporte',
  templateUrl: './ajuda-suporte.component.html',
  styleUrls: ['./ajuda-suporte.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class AjudaSuporteComponent implements OnInit
{

  constructor () { }

  ngOnInit(): void
  {
  }

}
