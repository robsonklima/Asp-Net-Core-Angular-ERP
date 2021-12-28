import { Component, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-ajuda-faq',
  templateUrl: './ajuda-faq.component.html',
  styleUrls: ['./ajuda-faq.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class AjudaFaqComponent implements OnInit
{
  constructor ()
  {
  }

  ngOnInit(): void
  {

  }

  ngOnDestroy(): void
  {
  }
}